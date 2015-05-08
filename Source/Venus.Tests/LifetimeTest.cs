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
            var c = new VenusContainer();
            c.Define<IUserRepository, SqlUserRepository>();

            var instance1 = c.Lookup<IUserRepository>();
            var instance2 = c.Lookup<IUserRepository>();
            var instance3 = c.Lookup<IUserRepository>();

            Assert.IsInstanceOfType(instance1, typeof(IUserRepository));
            Assert.IsInstanceOfType(instance2, typeof(IUserRepository));
            Assert.IsInstanceOfType(instance3, typeof(IUserRepository));

            Assert.AreSame(instance1, instance2);
            Assert.AreSame(instance2, instance3);
        }

        [TestMethod()]
        public void PerLookupLifetime()
        {
            var c = new VenusContainer();
            c.Define<IUserRepository, SqlUserRepository>(new PerLookupLifetime());

            var instance1 = c.Lookup<IUserRepository>();
            var instance2 = c.Lookup<IUserRepository>();
            var instance3 = c.Lookup<IUserRepository>();

            Assert.IsInstanceOfType(instance1, typeof(IUserRepository));
            Assert.IsInstanceOfType(instance2, typeof(IUserRepository));
            Assert.IsInstanceOfType(instance3, typeof(IUserRepository));

            Assert.AreNotSame(instance1, instance2);
            Assert.AreNotSame(instance2, instance3);
            Assert.AreNotSame(instance1, instance3);
        }

        [TestMethod()]
        public void PerContainerLifetime()
        {
            var c = new VenusContainer();
            c.Define<IUserRepository, SqlUserRepository>(new PerContainerLifetime());

            var instance1 = c.Lookup<IUserRepository>();
            var instance2 = c.Lookup<IUserRepository>();
            var instance3 = c.Lookup<IUserRepository>();

            Assert.IsInstanceOfType(instance1, typeof(IUserRepository));
            Assert.IsInstanceOfType(instance2, typeof(IUserRepository));
            Assert.IsInstanceOfType(instance3, typeof(IUserRepository));

            Assert.AreSame(instance1, instance2);
            Assert.AreSame(instance2, instance3);
        }
    }
}
