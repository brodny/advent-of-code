using System;

namespace Tests.Tools
{
    public interface IParser
    {
        void DefineToken(char token, Action action);
        void Parse(string input);
    }
}