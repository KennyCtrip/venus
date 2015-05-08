using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Venus.Tests
{
    [TestClass]
    public class ContainerLoaderTest
    {
        [TestMethod]
        public void Singleton()
        {
            var container1 = VenusContainerLoader.Container;
            var container2 = VenusContainerLoader.Container;
            var container3 = VenusContainerLoader.Container;

            Assert.AreSame(container1, container2);
            Assert.AreSame(container2, container3);
        }

        [TestMethod]
        public void DefineAndLookup()
        {
            VenusContainerLoader.Container.Define<IUserRepository, SqlUserRepository>();
            VenusContainerLoader.Container.Define<IUserRepository, OracleUserRepository>("oracle");
            VenusContainerLoader.Container.Define<ILogger, FileLogger>();
            VenusContainerLoader.Container.Define<ILogger, MailLogger>("mail");

            Assert.AreEqual("sql", VenusContainerLoader.Container.Lookup<IUserRepository>().Name);
            Assert.AreEqual("oracle", VenusContainerLoader.Container.Lookup<IUserRepository>("oracle").Name);
            Assert.AreEqual("file", VenusContainerLoader.Container.Lookup<ILogger>().Name);
            Assert.AreEqual("mail", VenusContainerLoader.Container.Lookup<ILogger>("mail").Name);
        }
    }
}
