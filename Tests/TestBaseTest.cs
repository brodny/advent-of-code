using Day1;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public sealed class TestBaseTest : TestBase
    {
        [Test]
        public void TestMethod()
        {
            ITest test = Kernel.Get<ITest>();
            string answer = test.Hello("Łukasz");
            Assert.AreEqual("Hello, Łukasz", answer);
        }
    }
}