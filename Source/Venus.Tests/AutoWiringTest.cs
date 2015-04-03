using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Venus;

namespace Venus.Tests
{
    [TestClass]
    public class AutoWiringTest
    {
        [TestMethod]
        public void DefaultWiring()
        {
            var c = new IocContainer();
            c.Register<IFoo, Foo1>();
            c.Register<IBar, Bar1>();
            c.Register<IFooBar, FooBar1>();

            var instance = c.Resolve<IFooBar>();

            Assert.AreEqual("Foo1", instance.Foo.Name);
            Assert.AreEqual("Bar1", instance.Bar.Name);
        }

        [TestMethod]
        public void AnnotatedNamelessWiring()
        {
            var c = new IocContainer();
            c.Register<IFoo, Foo1>();
            c.Register<IBar, Bar1>();
            c.Register<IFooBar, FooBar2>();

            var instance = c.Resolve<IFooBar>();

            Assert.AreEqual("Foo1", instance.Foo.Name);
            Assert.AreEqual("Bar1", instance.Bar.Name);
        }

        [TestMethod]
        public void AnnotatedNamedWiring()
        {
            var c = new IocContainer();
            c.Register<IFoo, Foo1>("foo1");
            c.Register<IFoo, Foo2>("foo2");
            c.Register<IBar, Bar1>("bar1");
            c.Register<IBar, Bar2>("bar2");
            c.Register<IFooBar, FooBar3>();

            var instance = c.Resolve<IFooBar>();

            Assert.AreEqual("Foo2", instance.Foo.Name);
            Assert.AreEqual("Bar2", instance.Bar.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DependencyCanNotResolved()
        {
            var c = new IocContainer();
            c.Register<IFoo, Foo1>();
            c.Register<IFooBar, FooBar1>();

            c.Resolve<IFooBar>();
        }
    }
}
