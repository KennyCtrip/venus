using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Venus;

namespace Venus.Tests
{
    [TestClass]
    public class ResolveTest
    {
        [TestMethod]
        public void GenericNameless()
        {
            var c = new IocContainer();
            c.Register<Foo1>();
            c.Register<IFoo, Foo2>();

            Assert.IsNotNull(c.TryResolve<Foo1>());
            Assert.IsNotNull(c.TryResolve<IFoo>());
            Assert.IsNull(c.TryResolve<Bar1>());
            Assert.IsNull(c.TryResolve<IBar>());
        }

        [TestMethod()]
        public void GenericNamed()
        {
            var c = new IocContainer();
            c.Register<IFoo, Foo1>("foo1");
            c.Register<IBar, Bar1>("bar1");

            Assert.IsNotNull(c.TryResolve<IFoo>("foo1"));
            Assert.IsNotNull(c.TryResolve<IBar>("bar1"));
            Assert.IsNull(c.TryResolve<IFoo>("foo2"));
            Assert.IsNull(c.TryResolve<IBar>("bar2"));
        }

        [TestMethod()]
        public void NongenericNameless()
        {
            var c = new IocContainer();
            c.Register(typeof(Foo1));
            c.Register(typeof(IFoo), typeof(Foo2));

            Assert.IsNotNull(c.TryResolve(typeof(Foo1)));
            Assert.IsNotNull(c.TryResolve(typeof(IFoo)));
            Assert.IsNull(c.TryResolve(typeof(Bar1)));
            Assert.IsNull(c.TryResolve(typeof(IBar)));
        }

        [TestMethod()]
        public void NongenericNamed()
        {
            var c = new IocContainer();
            c.Register(typeof(IFoo), typeof(Foo1), "foo1");
            c.Register(typeof(IBar), typeof(Bar1), "bar1");

            Assert.IsNotNull(c.TryResolve(typeof(IFoo), "foo1"));
            Assert.IsNotNull(c.TryResolve(typeof(IBar), "bar1"));
            Assert.IsNull(c.TryResolve(typeof(IFoo), "foo2"));
            Assert.IsNull(c.TryResolve(typeof(IBar), "bar2"));
        }

        [TestMethod()]
        public void ResolveAllGeneric()
        {
            var c = new IocContainer();
            c.Register<IFoo, Foo1>("foo1");
            c.Register<IFoo, Foo2>("foo2");

            Assert.AreEqual(2, c.ResolveAll<IFoo>().Count());
            Assert.AreEqual(0, c.ResolveAll<IBar>().Count());
        }

        [TestMethod()]
        public void ResolveAllNongeneric()
        {
            var c = new IocContainer();
            c.Register(typeof(IFoo), typeof(Foo1), "foo1");
            c.Register(typeof(IFoo), typeof(Foo2), "foo2");

            Assert.AreEqual(2, c.ResolveAll(typeof(IFoo)).Count());
            Assert.AreEqual(0, c.ResolveAll(typeof(IBar)).Count());
        }
    }
}
