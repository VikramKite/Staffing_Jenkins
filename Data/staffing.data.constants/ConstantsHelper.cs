using System;

namespace staffing.data.constants
{
    public static class ConstantsHelper
    {
        public const int MaxTextCharacterLength = 10;

        public static string TextLimit(this string input)
        {
            if (!String.IsNullOrEmpty(input) && input.Length > MaxTextCharacterLength)
            {
                return input.Substring(0, MaxTextCharacterLength) + " ... ";
            }
            return input;
        }
    }
}
