using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Venus;

namespace Venus.Tests
{
    [TestClass]
    public class DefineTypeTest
    {
        [TestMethod]
        public void GenericNameless()
        {
            var c = new VenusContainer();
            c.Define<SqlUserRepository>();
            c.Define<FileLogger>();
            c.Define<IUserRepository, OracleUserRepository>();
            c.Define<ILogger, MailLogger>();

            Assert.IsInstanceOfType(c.Lookup<SqlUserRepository>(), typeof(SqlUserRepository));
            Assert.IsInstanceOfType(c.Lookup<FileLogger>(), typeof(FileLogger));
            Assert.IsInstanceOfType(c.Lookup<IUserRepository>(), typeof(OracleUserRepository));
            Assert.IsInstanceOfType(c.Lookup<ILogger>(), typeof(MailLogger));
        }

        [TestMethod]
        public void GenericNamed()
        {
            var c = new VenusContainer();
            c.Define<IUserRepository, SqlUserRepository>("sql");
            c.Define<IUserRepository, OracleUserRepository>("oracle");
            c.Define<ILogger, FileLogger>("file");
            c.Define<ILogger, MailLogger>("mail");

            Assert.IsInstanceOfType(c.Lookup<IUserRepository>("sql"), typeof(SqlUserRepository));
            Assert.IsInstanceOfType(c.Lookup<IUserRepository>("oracle"), typeof(OracleUserRepository));
            Assert.IsInstanceOfType(c.Lookup<ILogger>("file"), typeof(FileLogger));
            Assert.IsInstanceOfType(c.Lookup<ILogger>("mail"), typeof(MailLogger));
        }

        [TestMethod]
        public void NongenericNameless()
        {
            var c = new VenusContainer();
            c.Define(typeof(SqlUserRepository));
            c.Define(typeof(FileLogger));
            c.Define(typeof(IUserRepository), typeof(OracleUserRepository));
            c.Define(typeof(ILogger), typeof(MailLogger));

            Assert.IsInstanceOfType(c.Lookup<SqlUserRepository>(), typeof(SqlUserRepository));
            Assert.IsInstanceOfType(c.Lookup<FileLogger>(), typeof(FileLogger));
            Assert.IsInstanceOfType(c.Lookup<IUserRepository>(), typeof(OracleUserRepository));
            Assert.IsInstanceOfType(c.Lookup<ILogger>(), typeof(MailLogger));
        }

        [TestMethod]
        public void NongenericNamed()
        {
            var c = new VenusContainer();
            c.Define(typeof(IUserRepository), typeof(SqlUserRepository), "sql");
            c.Define(typeof(IUserRepository), typeof(OracleUserRepository), "oracle");
            c.Define(typeof(ILogger), typeof(FileLogger), "file");
            c.Define(typeof(ILogger), typeof(MailLogger), "mail");

            Assert.IsInstanceOfType(c.Lookup(typeof(IUserRepository), "sql"), typeof(SqlUserRepository));
            Assert.IsInstanceOfType(c.Lookup(typeof(IUserRepository), "oracle"), typeof(OracleUserRepository));
            Assert.IsInstanceOfType(c.Lookup(typeof(ILogger), "file"), typeof(FileLogger));
            Assert.IsInstanceOfType(c.Lookup(typeof(ILogger), "mail"), typeof(MailLogger));
        }
    }
}
