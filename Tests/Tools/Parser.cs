using System;
using System.Collections.Generic;
using System.Globalization;

namespace Tests.Tools
{
    public sealed class Parser : IParser
    {
        private readonly Dictionary<char, Action> _tokens = new Dictionary<char, Action>();

        public void DefineToken(char token, Action action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            _tokens[token] = action;
        }

        public void Parse(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            for (int i = 0; i < input.Length; ++i)
            {
                char token = input[i];
                if (!_tokens.ContainsKey(token))
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture,
                        "'{0}' is not recognized command", token));
                }

                _tokens[token]();
            }
        }
    }
}