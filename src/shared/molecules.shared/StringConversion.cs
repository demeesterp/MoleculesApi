﻿using System.Globalization;

namespace molecules.shared
{
    public static class StringConversion
    {
        public static decimal ToDecimal(string input)
        {
            if (!decimal.TryParse(input.Replace(",", "."),
                                    NumberStyles.Float,
                                    CultureInfo.CreateSpecificCulture("en-US"),
                                    out decimal retval))
            {
                throw new ArgumentException($"Invalid input {input}");
            }
            return retval;
        }

        public static double ToDouble(string input)
        {
            if (!double.TryParse(input.Replace(",", "."),
                                    NumberStyles.Float,
                                    CultureInfo.CreateSpecificCulture("en-US"),
                                    out double retval))
            {
                throw new ArgumentException($"Invalid input {input}");
            }
            return retval;
        }

        public static int ToInt(string input)
        {
            if (!int.TryParse(input, out int retval))
            {
                throw new ArgumentException($"Invalid input {input}");
            }
            return retval;
        }
    }
}
