using System;

namespace Tests.Day7
{
    public static class Day7Extensions
    {
        public static string[] SplitAndRemoveEmptyEntries(this string @string, string separator)
        {
            if (@string == null)
                throw new ArgumentNullException(nameof(@string));
            if (separator == null)
                throw new ArgumentNullException(nameof(separator));

            string[] splitted = @string.Split(new string[] { separator, }, StringSplitOptions.RemoveEmptyEntries);
            return splitted;
        }
    }
}