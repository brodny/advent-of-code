using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public static class CollectionExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            foreach (T item in enumerable)
            {
                action(item);
            }
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> enumerable, T element) => enumerable.Except(element.AsEnumerable());

        public static IEnumerable<T> AsEnumerable<T>(this T element)
        {
            yield return element;
        }
    }
}