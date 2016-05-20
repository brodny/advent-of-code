using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Day8
{
    public sealed class StringEncoder : IStringEncoder
    {
        public string Encode(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            StringBuilder builder = new StringBuilder().Append(STRING_DELIMETER);

            for (int i = 0; i < input.Length; ++i)
            {
                EncodeCharacter(input[i], builder);
            }

            builder.Append(STRING_DELIMETER);

            return builder.ToString();
        }

        private void EncodeCharacter(char character, StringBuilder buffer)
        {
            Debug.Assert(buffer != null);

            if (CharacterNeedsEscaping(character))
            {
                buffer.Append(ESCAPE_CHARACTER);
            }

            buffer.Append(character);
        }

        private bool CharacterNeedsEscaping(char character) => _charactersThatNeedEscaping.Contains(character);

        private const char ESCAPE_CHARACTER = '\\';
        private const char STRING_DELIMETER = '\"';
        private static readonly HashSet<char> _charactersThatNeedEscaping = new HashSet<char>()
            { '\\', '\"', '\'', };
    }
}