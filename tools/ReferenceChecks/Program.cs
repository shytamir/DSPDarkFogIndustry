using System.Security.Cryptography;
using System.Text.Json;
using Mono.Cecil;

// Metadata only: never load a game, Unity, BepInEx, or plugin assembly for execution.
internal static class Program
{
    private static readonly string[] Libraries = ["Assembly-CSharp", "BepInEx", "0Harmony", "UnityEngine", "UnityEngine.CoreModule"];
    private static readonly HashSet<string> Framework = ["mscorlib", "System", "System.Core", "System.Runtime", "netstandard"];
    private static readonly JsonSerializerOptions Json = new() { WriteIndented = true };

    private static string Visibility(MethodDefinition m) => m.IsPublic ? "public" : m.IsFamily ? "protected" : m.IsFamilyOrAssembly ? "protected internal" : "internal";
    private static string Visibility(FieldDefinition f) => f.IsPublic ? "public" : f.IsFamily ? "protected" : f.IsFamilyOrAssembly ? "protected internal" : "internal";
    private static bool Exposed(MethodDefinition m) => m.IsPublic || m.IsFamily || m.IsFamilyOrAssembly;
    private static bool Exposed(FieldDefinition f) => f.IsPublic || f.IsFamily || f.IsFamilyOrAssembly;
    private static string Signature(MethodDefinition m) => $"{Visibility(m)} {(m.IsStatic ? "static" : "instance")} {m.FullName}";
    private static string Signature(FieldDefinition f) => $"{Visibility(f)} {(f.IsStatic ? "static" : "instance")} {f.FullName}";
    private static string Hash(string path) => Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(path))).ToLowerInvariant();
    private static string Scope(TypeReference t) => t.Scope is AssemblyNameReference a ? a.Name : t.Module.Assembly.Name.Name;
    private static void Require(bool ok, string message) { if (!ok) throw new InvalidOperationException(message); }

    private static object[] Inventory(string directory) => Libraries.Select(name =>
    {
        using var assembly = AssemblyDefinition.ReadAssembly(Path.Combine(directory, name + ".dll"));
        var module = assembly.MainModule;
        return (object)new
        {
            assembly = assembly.Name.FullName,
            types = module.Types.Where(t => t.IsPublic).OrderBy(t => t.FullName).Select(t => new
            {
                type = t.FullName,
                kind = t.IsInterface ? "interface" : t.IsEnum ? "enum" : t.IsValueType ? "struct" : "class",
                abstract_type = t.IsAbstract,
                sealed_type = t.IsSealed,
                base_type = t.BaseType?.FullName,
                generic_parameters = t.GenericParameters.Select(g => new { name = g.Name, attributes = g.Attributes.ToString(), constraints = g.Constraints.Select(c => c.ConstraintType.FullName).Order().ToArray() }).ToArray(),
                interfaces = t.Interfaces.Select(i => i.InterfaceType.FullName).Order().ToArray(),
                fields = t.Fields.Where(Exposed).OrderBy(Signature).Select(f => new { signature = Signature(f), read_only = f.IsInitOnly, literal = f.IsLiteral, constant = f.HasConstant ? f.Constant : null }).ToArray(),
                methods = t.Methods.Where(Exposed).OrderBy(Signature).Select(m => new { signature = Signature(m), virtual_method = m.IsVirtual, abstract_method = m.IsAbstract }).ToArray()
            }).ToArray(),
            forwards = module.ExportedTypes.Where(t => t.IsForwarder).Select(t => new { type = t.FullName, target = t.Scope.Name }).OrderBy(t => t.type).ToArray()
        };
    }).ToArray();

    public static int Main(string[] args)
    {
        try
        {
            if (args.Length < 3) throw new ArgumentException("inventory <shims> <ledger> [--write], or validate <shims> <ledger> <plugin> <managed> <bepcore> <report>");
            string inventory = JsonSerializer.Serialize(Inventory(args[1]), Json).Replace("\r\n", "\n") + "\n";
            if (args[0] == "inventory" && args.Length == 4 && args[3] == "--write")
            {
                File.WriteAllText(args[2], inventory);
                Console.WriteLine("External type/member ledger written from compile-only declarations.");
                return 0;
            }
            Require(File.ReadAllText(args[2]).Replace("\r\n", "\n") == inventory, "Shim declarations differ from the reviewed external reference ledger.");
            if (args[0] == "inventory") { Console.WriteLine("Shim declaration ledger matches."); return 0; }
            Require(args[0] == "validate" && args.Length == 7, "Invalid validation arguments.");
            string plugin = args[3], managed = args[4], core = args[5];
            var resolver = new DefaultAssemblyResolver();
            resolver.AddSearchDirectory(managed);
            resolver.AddSearchDirectory(core);
            var parameters = new ReaderParameters { AssemblyResolver = resolver };
            using var baseline = JsonDocument.Parse(File.ReadAllText(Path.Combine(Path.GetDirectoryName(args[2])!, "reference-baseline.json")));
            var identities = new List<object>();
            int declarations = 0;
            foreach (string name in Libraries)
            {
                string actualPath = Path.Combine(name is "BepInEx" or "0Harmony" ? core : managed, name + ".dll");
                using var actual = AssemblyDefinition.ReadAssembly(actualPath, parameters);
                using var shim = AssemblyDefinition.ReadAssembly(Path.Combine(args[1], name + ".dll"));
                Require(actual.Name.FullName == shim.Name.FullName, $"Assembly identity mismatch: {name}");
                Require(baseline.RootElement.GetProperty(name).GetProperty("sha256").GetString() == Hash(actualPath), $"Reference baseline drift: {name}");
                identities.Add(new { assembly = actual.Name.FullName, sha256 = Hash(actualPath) });
                foreach (var type in shim.MainModule.Types.Where(t => t.IsPublic))
                {
                    var real = actual.MainModule.GetType(type.FullName);
                    Require(real != null, $"Missing real type: {name}:{type.FullName}");
                    Require(real!.BaseType?.FullName == type.BaseType?.FullName && real.IsValueType == type.IsValueType && real.IsInterface == type.IsInterface && real.IsAbstract == type.IsAbstract && real.IsSealed == type.IsSealed,
                        $"Type shape mismatch: {type.FullName}");
                    Require(real.GenericParameters.Count == type.GenericParameters.Count, $"Generic arity mismatch: {type.FullName}");
                    for (int i = 0; i < type.GenericParameters.Count; i++)
                        Require(real.GenericParameters[i].Attributes == type.GenericParameters[i].Attributes && real.GenericParameters[i].Constraints.Select(c => c.ConstraintType.FullName).Order().SequenceEqual(type.GenericParameters[i].Constraints.Select(c => c.ConstraintType.FullName).Order()), $"Generic constraint mismatch: {type.FullName}");
                    foreach (var iface in type.Interfaces) Require(real.Interfaces.Any(i => i.InterfaceType.FullName == iface.InterfaceType.FullName), $"Missing interface: {type.FullName}:{iface.InterfaceType}");
                    foreach (var field in type.Fields.Where(Exposed))
                    {
                        Require(real.Fields.Any(f => Signature(f) == Signature(field) && f.IsInitOnly == field.IsInitOnly && f.IsLiteral == field.IsLiteral && Equals(f.Constant, field.Constant)), $"Field mismatch: {Signature(field)}");
                        declarations++;
                    }
                    foreach (var method in type.Methods.Where(Exposed))
                    {
                        Require(real.Methods.Any(m => Signature(m) == Signature(method) && m.IsVirtual == method.IsVirtual && m.IsAbstract == method.IsAbstract), $"Method mismatch: {Signature(method)}");
                        declarations++;
                    }
                }
                foreach (var forward in shim.MainModule.ExportedTypes.Where(t => t.IsForwarder))
                    Require(actual.MainModule.ExportedTypes.Any(t => t.IsForwarder && t.FullName == forward.FullName && t.Scope.Name == forward.Scope.Name), $"Type forward mismatch: {forward.FullName}");
            }
            using var product = AssemblyDefinition.ReadAssembly(plugin, parameters);
            var external = new List<string>();
            foreach (var reference in product.MainModule.AssemblyReferences)
                Require(Libraries.Contains(reference.Name) || Framework.Contains(reference.Name), $"Unaccounted external assembly: {reference.FullName}");
            foreach (var type in product.MainModule.GetTypeReferences())
            {
                if (Framework.Contains(Scope(type)) || Scope(type) == product.Name.Name) continue;
                Require(Libraries.Contains(Scope(type)) && type.Resolve() != null, $"Unresolved external type: {type.FullName}");
                external.Add($"{Scope(type)}:type:{type.FullName}");
            }
            foreach (var member in product.MainModule.GetMemberReferences())
            {
                if (Framework.Contains(Scope(member.DeclaringType)) || Scope(member.DeclaringType) == product.Name.Name) continue;
                Require(member switch { MethodReference m => m.Resolve() != null, FieldReference f => f.Resolve() != null, _ => false }, $"Unresolved member: {member.FullName}");
                external.Add($"{Scope(member.DeclaringType)}:member:{member.FullName}");
            }
            var entry = product.MainModule.Types.Single(t => t.FullName == "DSPDarkFogIndustry.Plugin");
            var metadata = entry.CustomAttributes.Single(a => a.AttributeType.FullName == "BepInEx.BepInPlugin");
            Require((string)metadata.ConstructorArguments[0].Value == "dark-fog-industry", "Wrong plugin GUID.");
            Require((string)metadata.ConstructorArguments[1].Value == "DSP Dark Fog Industry", "Wrong plugin display name.");
            Require((string)entry.CustomAttributes.Single(a => a.AttributeType.FullName == "BepInEx.BepInProcess").ConstructorArguments[0].Value == "DSPGAME.exe", "Wrong game process filter.");
            Require((string)metadata.ConstructorArguments[2].Value == product.Name.Version.ToString(3), "Plugin/version mismatch.");
            var targets = new List<string>();
            using var patchLedger = JsonDocument.Parse(File.ReadAllText(Path.Combine(Path.GetDirectoryName(args[2])!, "patch-targets.json")));
            foreach (var attribute in product.MainModule.Types.SelectMany(t => t.CustomAttributes).Where(a => a.AttributeType.FullName == "HarmonyLib.HarmonyPatch"))
            {
                Require(attribute.ConstructorArguments.Count == 2, "Unreviewed Harmony target declaration.");
                var targetType = (TypeReference)attribute.ConstructorArguments[0].Value;
                var targetName = (string)attribute.ConstructorArguments[1].Value;
                var method = targetType.Resolve().Methods.Single(m => m.Name == targetName);
                var expected = patchLedger.RootElement.EnumerateArray().Single(t => t.GetProperty("type").GetString() == targetType.FullName && t.GetProperty("method").GetString() == targetName);
                Require(expected.GetProperty("assembly").GetString() == Scope(targetType) && expected.GetProperty("signature").GetString() == method.FullName
                    && expected.GetProperty("visibility").GetString() == (method.IsPrivate ? "private" : Visibility(method)) && !method.IsStatic
                    && expected.GetProperty("parameter_names").EnumerateArray().Select(p => p.GetString()).SequenceEqual(method.Parameters.Select(p => p.Name)), $"Unverified Harmony target: {method.FullName}");
                targets.Add(method.FullName);
            }
            Require(targets.Count == patchLedger.RootElement.GetArrayLength(), "Missing or duplicated Harmony target.");
            File.WriteAllText(args[6], JsonSerializer.Serialize(new { kind = "static-real-reference-check", plugin_sha256 = Hash(plugin), references = identities, shim_member_count = declarations,
                external_references = external.Distinct().Order().ToArray(), harmony_targets = targets.Order().ToArray(), limits = "Metadata only; no game or plugin execution, gameplay or owner acceptance." }, Json) + "\n");
            Console.WriteLine($"Verified {declarations} shim members, {external.Distinct().Count()} external references, {targets.Count} Harmony targets against real metadata.");
            return 0;
        }
        catch (Exception error) { Console.Error.WriteLine(error.Message); return 1; }
    }
}
