using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Modules;
using NUnit.Framework;

namespace Tests
{
    public abstract class TestBase
    {
        private IKernel _kernel;
        protected IKernel Kernel => _kernel;

        [SetUp]
        public void SetUp()
        {
            _kernel = new StandardKernel(new TestModule());
        }

        private sealed class TestModule : NinjectModule
        {
            public override void Load()
            {
                this.Bind(x => x.From(typeof(TestBase).Assembly).SelectAllClasses().BindAllInterfaces());
            }
        }
    }
}