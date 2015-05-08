using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Venus;

namespace Venus.Tests
{
    [TestClass]
    public class InitializeTest
    {
        [TestInitialize]
        public void Init()
        {
            System.Diagnostics.Debug.AutoFlush = false;
        }

        [TestMethod]
        public void SetLogger()
        {
            var c = new VenusContainer();
            c.Define<Loggable>(new PerLookupLifetime());

            var instance = c.Lookup<Loggable>();
            Assert.IsNull(instance.Logger);

            c.Define<Venus.ILogger, CommonLogger>();

            var instance1 = c.Lookup<Loggable>();
            var instance2 = c.Lookup<Loggable>();
            var instance3 = c.Lookup<Loggable>();

            Assert.IsInstanceOfType(instance1.Logger, typeof(CommonLogger));
            Assert.IsInstanceOfType(instance2.Logger, typeof(CommonLogger));
            Assert.IsInstanceOfType(instance3.Logger, typeof(CommonLogger));

            Assert.AreSame(instance1.Logger, instance2.Logger);
            Assert.AreSame(instance2.Logger, instance3.Logger);
        }

        [TestMethod]
        public void SetContainer()
        {
            var c = new VenusContainer();
            c.Define<Containable>(new PerLookupLifetime());

            var instance1 = c.Lookup<Containable>();
            var instance2 = c.Lookup<Containable>();
            var instance3 = c.Lookup<Containable>();

            Assert.AreSame(c, instance1.Container);
            Assert.AreSame(c, instance2.Container);
            Assert.AreSame(c, instance3.Container);
        }

        [TestMethod]
        public void Initialize()
        {
            var c = new VenusContainer();
            c.Define<Initializable>(new PerLookupLifetime());

            var instance1 = c.Lookup<Initializable>();
            var instance2 = c.Lookup<Initializable>();
            var instance3 = c.Lookup<Initializable>();

            Assert.IsTrue(instance1.Initialized);
            Assert.IsTrue(instance2.Initialized);
            Assert.IsTrue(instance3.Initialized);
        }

        [TestMethod]
        public void Dispose()
        {
            var c = new VenusContainer();
            c.Define<Disposable>();

            var instance = c.Lookup<Disposable>();
            Assert.IsFalse(instance.Disposabled);

            c.Dispose();
            Assert.IsTrue(instance.Disposabled);
        }

        [TestMethod]
        public void FullInitialize()
        {
            var c = new VenusContainer();
            c.Define<Venus.ILogger, CommonLogger>();
            c.Define<FullInitialization>();

            var instance = c.Lookup<FullInitialization>();
            Assert.IsInstanceOfType(instance.Logger, typeof(CommonLogger));
            Assert.AreSame(c, instance.Container);
            Assert.IsTrue(instance.Initialized);

            Assert.IsFalse(instance.Disposabled);

            c.Dispose();
            Assert.IsTrue(instance.Disposabled);
        }
    }
}
