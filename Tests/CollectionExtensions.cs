using System;
using System.Collections.Generic;

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
    }
}