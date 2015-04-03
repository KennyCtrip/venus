using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Venus;

namespace Venus.Tests
{
    [TestClass]
    public class LifetimeTest
    {
        [TestMethod()]
        public void DefaultLifetime()
        {
            var c = new IocContainer();
            c.Register<IFoo, Foo1>();

            var instance1 = c.Resolve<IFoo>();
            var instance2 = c.Resolve<IFoo>();
            var instance3 = c.Resolve<IFoo>();

            Assert.IsInstanceOfType(instance1, typeof(Foo1));
            Assert.IsInstanceOfType(instance2, typeof(Foo1));
            Assert.IsInstanceOfType(instance3, typeof(Foo1));

            Assert.AreNotSame(instance1, instance2);
            Assert.AreNotSame(instance2, instance3);
            Assert.AreNotSame(instance1, instance3);
        }

        [TestMethod()]
        public void TransientLifetime()
        {
            var c = new IocContainer();
            c.Register<IFoo, Foo1>(new TransientLifetime());

            var instance1 = c.Resolve<IFoo>();
            var instance2 = c.Resolve<IFoo>();
            var instance3 = c.Resolve<IFoo>();

            Assert.IsInstanceOfType(instance1, typeof(Foo1));
            Assert.IsInstanceOfType(instance2, typeof(Foo1));
            Assert.IsInstanceOfType(instance3, typeof(Foo1));

            Assert.AreNotSame(instance1, instance2);
            Assert.AreNotSame(instance2, instance3);
            Assert.AreNotSame(instance1, instance3);
        }

        [TestMethod()]
        public void ContainerLifetime()
        {
            var c = new IocContainer();
            c.Register<IFoo, Foo1>(new ContainerLifetime());

            var instance1 = c.Resolve<IFoo>();
            var instance2 = c.Resolve<IFoo>();
            var instance3 = c.Resolve<IFoo>();

            Assert.IsInstanceOfType(instance1, typeof(Foo1));
            Assert.IsInstanceOfType(instance2, typeof(Foo1));
            Assert.IsInstanceOfType(instance3, typeof(Foo1));

            Assert.AreSame(instance1, instance2);
            Assert.AreSame(instance2, instance3);
        }
    }
}
