using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Venus;

namespace Venus.Tests
{
    [TestClass]
    public class RegisterTypeTest
    {
        [TestMethod]
        public void GenericNameless()
        {
            var c = new IocContainer();
            c.Register<Foo1>();
            c.Register<Bar1>();
            c.Register<IFoo, Foo2>();
            c.Register<IBar, Bar2>();

            Assert.IsInstanceOfType(c.Resolve<Foo1>(), typeof(Foo1));
            Assert.IsInstanceOfType(c.Resolve<Bar1>(), typeof(Bar1));
            Assert.IsInstanceOfType(c.Resolve<IFoo>(), typeof(Foo2));
            Assert.IsInstanceOfType(c.Resolve<IBar>(), typeof(Bar2));
        }

        [TestMethod]
        public void GenericNamed()
        {
            var c = new IocContainer();
            c.Register<IFoo, Foo1>("foo1");
            c.Register<IFoo, Foo2>("foo2");
            c.Register<IBar, Bar1>("bar1");
            c.Register<IBar, Bar2>("bar2");

            Assert.IsInstanceOfType(c.Resolve<IFoo>("foo1"), typeof(Foo1));
            Assert.IsInstanceOfType(c.Resolve<IFoo>("foo2"), typeof(Foo2));
            Assert.IsInstanceOfType(c.Resolve<IBar>("bar1"), typeof(Bar1));
            Assert.IsInstanceOfType(c.Resolve<IBar>("bar2"), typeof(Bar2));
        }

        [TestMethod]
        public void NongenericNameless()
        {
            var c = new IocContainer();
            c.Register(typeof(Foo1));
            c.Register(typeof(Bar1));
            c.Register(typeof(IFoo), typeof(Foo2));
            c.Register(typeof(IBar), typeof(Bar2));

            Assert.IsInstanceOfType(c.Resolve(typeof(Foo1)), typeof(Foo1));
            Assert.IsInstanceOfType(c.Resolve(typeof(Bar1)), typeof(Bar1));
            Assert.IsInstanceOfType(c.Resolve(typeof(IFoo)), typeof(Foo2));
            Assert.IsInstanceOfType(c.Resolve(typeof(IBar)), typeof(Bar2));
        }

        [TestMethod]
        public void NongenericNamed()
        {
            var c = new IocContainer();
            c.Register(typeof(IFoo), typeof(Foo1), "foo1");
            c.Register(typeof(IFoo), typeof(Foo2), "foo2");
            c.Register(typeof(IBar), typeof(Bar1), "bar1");
            c.Register(typeof(IBar), typeof(Bar2), "bar2");

            Assert.IsInstanceOfType(c.Resolve(typeof(IFoo), "foo1"), typeof(Foo1));
            Assert.IsInstanceOfType(c.Resolve(typeof(IFoo), "foo2"), typeof(Foo2));
            Assert.IsInstanceOfType(c.Resolve(typeof(IBar), "bar1"), typeof(Bar1));
            Assert.IsInstanceOfType(c.Resolve(typeof(IBar), "bar2"), typeof(Bar2));
        }
    }
}
