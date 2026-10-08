internal static class Program
{
    internal static void Check(bool condition, string message)
    {
        if (!condition)
        {
            throw new InvalidOperationException(message);
        }
    }

    internal static void Run(string name, Action test)
    {
        try
        {
            test();
            Console.WriteLine($"PASS {name}");
        }
        catch (Exception error)
        {
            throw new InvalidOperationException($"FAIL {name}: {error.Message}", error);
        }
    }

    public static void Main()
    {
        RegistrationChecks.Run();
        PrototypeDetectionChecks.Run();
        ProductionChecks.Run();
        SaveChecks.Run();
        Console.WriteLine("Product checks passed using test data; no game code executed.");
    }
}
