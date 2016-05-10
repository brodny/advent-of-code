using System;
using System.IO;

namespace Tests
{
    internal static class Utils
    {
        public static string GetTextFromResource(string resourceName)
        {
            if (resourceName == null)
                throw new ArgumentNullException(nameof(resourceName));

            using (Stream resourceStream = typeof(Utils).Assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(resourceStream))
            {
                return reader.ReadToEnd();
            }
        }

        public static string[] SplitLines(string @string)
        {
            if (@string == null)
                throw new ArgumentNullException(nameof(@string));

            string[] splitted = @string.Split(new[] { '\r', '\n', }, StringSplitOptions.RemoveEmptyEntries);
            return splitted;
        }
    }
}