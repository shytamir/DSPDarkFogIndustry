using System;
using System.Globalization;
using System.Numerics;
using System.Text.RegularExpressions;

namespace DSPDarkFogIndustry
{
    // Exact decimal distances prevent rounding a fractional tie the wrong way.
    // Values outside the Int32 neighbourhood need only retain their direction:
    // all candidate settings are nonnegative Int32 values.
    internal sealed class NumericSetting
    {
        private readonly BigInteger numerator;
        private readonly BigInteger denominator;
        internal readonly bool IsNumeric;
        internal bool IsZero { get { return IsNumeric && numerator.IsZero; } }

        internal NumericSetting(IntegerSetting setting)
        {
            var match = Regex.Match(setting.NumericText.Trim(), @"^([+-]?)([0-9]*)(?:\.([0-9]*))?(?:[eE]([+-]?[0-9]+))?$");
            string digits = match.Groups[2].Value + match.Groups[3].Value;
            IsNumeric = match.Success && digits.Length > 0;
            denominator = BigInteger.One;
            if (!IsNumeric)
            {
                numerator = setting.Default;
                return;
            }

            digits = digits.TrimStart('0');
            if (digits.Length == 0) { numerator = BigInteger.Zero; return; }
            int sign = match.Groups[1].Value == "-" ? -1 : 1;
            BigInteger exponent = match.Groups[4].Success ? BigInteger.Parse(match.Groups[4].Value, CultureInfo.InvariantCulture) : BigInteger.Zero;
            BigInteger places = match.Groups[3].Value.Length - exponent;
            BigInteger order = digits.Length - places;
            if (order > 10) { numerator = sign * ((BigInteger)int.MaxValue + 1); return; }
            if (order < 0) { numerator = sign; denominator = 10; return; }
            numerator = sign * BigInteger.Parse(digits, CultureInfo.InvariantCulture);
            if (places > 0) { denominator = BigInteger.Pow(10, (int)places); }
            else { numerator *= BigInteger.Pow(10, (int)-places); }
        }

        internal bool Equals(int value) { return numerator == value * denominator; }
        internal BigInteger Distance(int value) { return BigInteger.Abs(numerator - value * denominator); }

        internal int Nearest(int min, int max, int step = 1)
        {
            // Candidates are positive multiples of step (or the explicit zero row).
            BigInteger units = numerator / (denominator * step);
            BigInteger low = units * step;
            int lower = (int)BigInteger.Min(max, BigInteger.Max(min, low));
            int upper = (int)BigInteger.Min(max, BigInteger.Max(min, low + step));
            return Distance(lower) <= Distance(upper) ? lower : upper;
        }
    }
}
