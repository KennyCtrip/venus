using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Venus;

namespace Venus.Tests
{
    [TestClass]
    public class RegisterInstanceTest
    {
        [TestMethod]
        public void GenericNameless()
        {
            var foo = new Foo1();
            var bar = new Bar1();

            var c = new IocContainer();
            c.RegisterInstance<IFoo>(foo);
            c.RegisterInstance<IBar>(bar);

            Assert.AreSame(c.Resolve<IFoo>(), foo);
            Assert.AreSame(c.Resolve<IBar>(), bar);
        }

        [TestMethod()]
        public void GenericNamed()
        {
            var foo1 = new Foo1();
            var foo2 = new Foo2();
            var bar1 = new Bar1();
            var bar2 = new Bar2();

            var c = new IocContainer();
            c.RegisterInstance<IFoo>(foo1, "foo1");
            c.RegisterInstance<IFoo>(foo2, "foo2");
            c.RegisterInstance<IBar>(bar1, "bar1");
            c.RegisterInstance<IBar>(bar2, "bar2");

            Assert.AreSame(c.Resolve<IFoo>("foo1"), foo1);
            Assert.AreSame(c.Resolve<IFoo>("foo2"), foo2);
            Assert.AreSame(c.Resolve<IBar>("bar1"), bar1);
            Assert.AreSame(c.Resolve<IBar>("bar2"), bar2);
        }

        [TestMethod]
        public void NongenericNameless()
        {
            var foo = new Foo1();
            var bar = new Bar1();

            var c = new IocContainer();
            c.RegisterInstance(typeof(IFoo), foo);
            c.RegisterInstance(typeof(IBar), bar);

            Assert.AreSame(c.Resolve(typeof(IFoo)), foo);
            Assert.AreSame(c.Resolve(typeof(IBar)), bar);
        }

        [TestMethod]
        public void NongenericNamed()
        {
            var foo1 = new Foo1();
            var foo2 = new Foo2();
            var bar1 = new Bar1();
            var bar2 = new Bar2();

            var c = new IocContainer();
            c.RegisterInstance(typeof(IFoo), foo1, "foo1");
            c.RegisterInstance(typeof(IFoo), foo2, "foo2");
            c.RegisterInstance(typeof(IBar), bar1, "bar1");
            c.RegisterInstance(typeof(IBar), bar2, "bar2");

            Assert.AreSame(c.Resolve(typeof(IFoo), "foo1"), foo1);
            Assert.AreSame(c.Resolve(typeof(IFoo), "foo2"), foo2);
            Assert.AreSame(c.Resolve(typeof(IBar), "bar1"), bar1);
            Assert.AreSame(c.Resolve(typeof(IBar), "bar2"), bar2);
        }
    }
}
