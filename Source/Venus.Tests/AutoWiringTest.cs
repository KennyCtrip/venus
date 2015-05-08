using System;
using System.Reflection;
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
            var c = new VenusContainer();
            c.Define<IUserRepository, SqlUserRepository>();
            c.Define<ILogger, FileLogger>();
            c.Define<IUserService, UserServiceWithConstructorDependency>();
            c.Define<FullInitialization>();

            var service = c.Lookup<IUserService>();

            Assert.AreEqual("sql", service.UserRepository.Name);
            Assert.AreEqual("file", service.Logger.Name);
        }

        [TestMethod]
        public void AnnotatedNamelessWiring()
        {
            var c = new VenusContainer();
            c.Define<IUserRepository, SqlUserRepository>();
            c.Define<ILogger, FileLogger>();
            c.Define<IUserService, UserServiceWithConstructorAndPropertyDependencies>();

            var service = c.Lookup<IUserService>();

            Assert.AreEqual("sql", service.UserRepository.Name);
            Assert.AreEqual("file", service.Logger.Name);
        }

        [TestMethod]
        public void AnnotatedNamedWiring()
        {
            var c = new VenusContainer();
            c.Define<IUserRepository, SqlUserRepository>("sql");
            c.Define<IUserRepository, OracleUserRepository>("oracle");
            c.Define<ILogger, FileLogger>("file");
            c.Define<ILogger, MailLogger>("mail");
            c.Define<IUserService, UserServiceWithNamedDependencies>();

            var service = c.Lookup<IUserService>();

            Assert.AreEqual("oracle", service.UserRepository.Name);
            Assert.AreEqual("mail", service.Logger.Name);
        }
    }
}
