using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tests.Day2
{
    public sealed class WrappingPaperParser : IWrappingPaperParser
    {
        public IList<Present> Parse(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            string[] presents = input.Split(new string[] { "\n", }, StringSplitOptions.RemoveEmptyEntries);
            List<Present> result = new List<Present>(presents.Length);

            for (int i = 0; i < presents.Length; ++i)
            {
                Debug.Assert(!string.IsNullOrWhiteSpace(presents[i]));

                Present present = ParsePresent(presents[i]);
                result.Add(present);
            }

            return result;
        }

        private Present ParsePresent(string present)
        {
            Debug.Assert(!string.IsNullOrEmpty(present));

            string[] dimensions = present.Split(new string[] { "x", }, StringSplitOptions.RemoveEmptyEntries);
            if (dimensions.Length < 3)
                throw new ArgumentException($"Invalid present dimensions description: {present}", nameof(present));

            int length;
            int width;
            int height;
            if (!int.TryParse(dimensions[0], out length) || !int.TryParse(dimensions[1], out width) ||
                !int.TryParse(dimensions[2], out height))
                throw new ArgumentException($"Invalid present dimensions description: {present}", nameof(present));

            Present result = new Present(length, width, height);
            return result;
        }
    }
}