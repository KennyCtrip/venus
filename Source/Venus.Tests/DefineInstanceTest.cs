using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Venus;

namespace Venus.Tests
{
    [TestClass]
    public class DefineInstanceTest
    {
        [TestMethod]
        public void GenericNameless()
        {
            var repository = new SqlUserRepository();
            var logger = new FileLogger();

            var c = new VenusContainer();
            c.DefineInstance<IUserRepository>(repository);
            c.DefineInstance<ILogger>(logger);

            Assert.AreSame(c.Lookup<IUserRepository>(), repository);
            Assert.AreSame(c.Lookup<ILogger>(), logger);
        }

        [TestMethod()]
        public void GenericNamed()
        {
            var sqlRepository = new SqlUserRepository();
            var oracleRepository = new OracleUserRepository();
            var fileLogger = new FileLogger();
            var mailLogger = new MailLogger();

            var c = new VenusContainer();
            c.DefineInstance<IUserRepository>(sqlRepository, "sql");
            c.DefineInstance<IUserRepository>(oracleRepository, "oracle");
            c.DefineInstance<ILogger>(fileLogger, "file");
            c.DefineInstance<ILogger>(mailLogger, "mail");

            Assert.AreSame(c.Lookup<IUserRepository>("sql"), sqlRepository);
            Assert.AreSame(c.Lookup<IUserRepository>("oracle"), oracleRepository);
            Assert.AreSame(c.Lookup<ILogger>("file"), fileLogger);
            Assert.AreSame(c.Lookup<ILogger>("mail"), mailLogger);
        }

        [TestMethod]
        public void NongenericNameless()
        {
            var repository = new SqlUserRepository();
            var logger = new FileLogger();

            var c = new VenusContainer();
            c.DefineInstance(typeof(IUserRepository), repository);
            c.DefineInstance(typeof(ILogger), logger);

            Assert.AreSame(c.Lookup(typeof(IUserRepository)), repository);
            Assert.AreSame(c.Lookup(typeof(ILogger)), logger);
        }

        [TestMethod]
        public void NongenericNamed()
        {
            var sqlRepository = new SqlUserRepository();
            var oracleRepository = new OracleUserRepository();
            var fileLogger = new FileLogger();
            var mailLogger = new MailLogger();

            var c = new VenusContainer();
            c.DefineInstance(typeof(IUserRepository), sqlRepository, "sql");
            c.DefineInstance(typeof(IUserRepository), oracleRepository, "oracle");
            c.DefineInstance(typeof(ILogger), fileLogger, "file");
            c.DefineInstance(typeof(ILogger), mailLogger, "mail");

            Assert.AreSame(c.Lookup<IUserRepository>("sql"), sqlRepository);
            Assert.AreSame(c.Lookup<IUserRepository>("oracle"), oracleRepository);
            Assert.AreSame(c.Lookup<ILogger>("file"), fileLogger);
            Assert.AreSame(c.Lookup<ILogger>("mail"), mailLogger);
        }
    }
}
