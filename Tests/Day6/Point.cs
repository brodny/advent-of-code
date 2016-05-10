using System;

namespace Tests.Day6
{
    public sealed class Point
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Point Parse(string @string)
        {
            if (@string == null)
                throw new ArgumentNullException(nameof(@string));

            string[] splitted = @string.Split(new char[] { ',', }, StringSplitOptions.RemoveEmptyEntries);
            if (splitted.Length != 2)
                throw new ArgumentException("Invalid format.");

            int x;
            if (!int.TryParse(splitted[0], out x))
                throw new ArgumentException("Invalid format.");

            int y;
            if (!int.TryParse(splitted[1], out y))
                throw new ArgumentException("Invalid format.");

            return new Point(x, y);
        }
    }
}