using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Venus;

namespace Venus.Tests.AutoRegistration
{
    [TestClass]
    public class AutoRegistrationTest
    {
        [TestMethod]
        public void DefaultService()
        {
            var container = new VenusContainer();
            var service = container.Lookup<IUserService>();

            Assert.IsInstanceOfType(service, typeof(UserServiceAutoRegistered));
            Assert.AreEqual("sql", service.UserRepository.Name);
            Assert.AreEqual("file", service.Logger.Name);
        }

        [TestMethod]
        public void NamedService()
        {
            var container = new VenusContainer();
            var service = container.Lookup<IUserService>("UserServiceWithName");

            Assert.IsInstanceOfType(service, typeof(UserServiceAutoRegisteredWithName));
            Assert.AreEqual("oracle", service.UserRepository.Name);
            Assert.AreEqual("mail", service.Logger.Name);
        }

        [TestMethod]
        public void NamedLifetimeService()
        {
            var container = new VenusContainer();
            var instance1 = container.Lookup<IUserService>("UserServiceWithLifetime");
            var instance2 = container.Lookup<IUserService>("UserServiceWithLifetime");
            var instance3 = container.Lookup<IUserService>("UserServiceWithLifetime");

            Assert.IsInstanceOfType(instance1, typeof(UserServiceAutoRegisteredWithLifetime));
            Assert.IsInstanceOfType(instance2, typeof(UserServiceAutoRegisteredWithLifetime));
            Assert.IsInstanceOfType(instance3, typeof(UserServiceAutoRegisteredWithLifetime));

            Assert.AreNotSame(instance1, instance2);
            Assert.AreNotSame(instance2, instance3);
            Assert.AreNotSame(instance1, instance3);
        }

        [TestMethod]
        public void OverrideAutoRegistration()
        {
            var container = new VenusContainer();
            container.Define<IUserRepository, OracleUserRepositoryAutoRegistered>();
            container.Define<ILogger, MailLoggerAutoRegisered>();

            Assert.IsInstanceOfType(container.Lookup<IUserRepository>(), typeof(OracleUserRepositoryAutoRegistered));
            Assert.IsInstanceOfType(container.Lookup<ILogger>(), typeof(MailLoggerAutoRegisered));
        }
    }
}
