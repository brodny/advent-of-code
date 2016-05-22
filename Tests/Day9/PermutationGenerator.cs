using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Tests.Day9
{
    public sealed class PermutationGenerator : IPermutationGenerator
    {
        public IEnumerable<IEnumerable<string>> Generate(IEnumerable<string> elements)
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));
            if (elements.Any(element => string.IsNullOrWhiteSpace(element)))
                throw new ArgumentException("One of elements is null or whitespace", nameof(elements));

            IEnumerable<IEnumerable<string>> permutations = DoGenerate(elements);
            return permutations;
        }

        private IEnumerable<IEnumerable<string>> DoGenerate(IEnumerable<string> elements)
        {
            Debug.Assert(elements != null);
            Debug.Assert(elements.All(element => !string.IsNullOrWhiteSpace(element)));

            if (elements.Count() == 1)
                return elements.AsEnumerable();

            IEnumerable<IEnumerable<string>> permutations = Enumerable.Empty<IEnumerable<string>>();

            foreach (string element in elements)
            {
                IEnumerable<string> remainingElements = elements.Except(element);
                IEnumerable<IEnumerable<string>> subpermutations = DoGenerate(remainingElements);
                IEnumerable<IEnumerable<string>> allPermutations = InsertElement(subpermutations, element);
                permutations = permutations.Concat(allPermutations);
            }

            return permutations;
        }

        private IEnumerable<IEnumerable<string>> InsertElement(IEnumerable<IEnumerable<string>> subpermutations,
            string element)
        {
            Debug.Assert(subpermutations != null);
            Debug.Assert(subpermutations.All(subpermutation => subpermutation != null));
            Debug.Assert(subpermutations.All(subpermutation => subpermutation.All(elem => !string.IsNullOrWhiteSpace(elem))));

            IEnumerable<IEnumerable<string>> inserted = Enumerable.Empty<IEnumerable<string>>();

            foreach (IEnumerable<string> subpermutation in subpermutations)
            {
                int subpermutationSize = subpermutation.Count();
                for (int i = 0; i < subpermutationSize; ++i)
                {
                    IEnumerable<string> subpermutationWithInsertedElement = Concat(subpermutation.Take(i),
                        element, subpermutation.Skip(i));
                    inserted = inserted.Concat(subpermutationWithInsertedElement.AsEnumerable());
                }
            }

            return inserted;
        }

        private IEnumerable<string> Concat(IEnumerable<string> start, string element,
            IEnumerable<string> finish)
        {
            Debug.Assert(start != null);
            Debug.Assert(start.All(elem => !string.IsNullOrWhiteSpace(elem)));
            Debug.Assert(!string.IsNullOrWhiteSpace(element));
            Debug.Assert(finish != null);
            Debug.Assert(finish.All(elem => !string.IsNullOrWhiteSpace(elem)));

            return start.Concat(element.AsEnumerable()).Concat(finish);
        }
    }
}