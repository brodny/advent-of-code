using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day1
{
    public interface ITest
    {
        string Hello(string name);
    }

    public class Test : ITest
    {
        public string Hello(string name) => "Hello, " + name;
    }
}