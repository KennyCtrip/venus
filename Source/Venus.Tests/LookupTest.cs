using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Venus;

namespace Venus.Tests
{
    [TestClass]
    public class LookupTest
    {
        [TestMethod]
        public void TryLookupGenericNameless()
        {
            var c = new VenusContainer();
            c.Define<SqlUserRepository>();
            c.Define<IUserRepository, OracleUserRepository>();
            
            Assert.IsNotNull(c.TryLookup<SqlUserRepository>());
            Assert.IsNotNull(c.TryLookup<IUserRepository>());
            Assert.IsNull(c.TryLookup<FileLogger>());
            Assert.IsNull(c.TryLookup<ILogger>());
        }

        [TestMethod()]
        public void TryLookupGenericNamed()
        {
            var c = new VenusContainer();
            c.Define<IUserRepository, SqlUserRepository>("sql");
            c.Define<ILogger, FileLogger>("file");

            Assert.IsNotNull(c.TryLookup<IUserRepository>("sql"));
            Assert.IsNotNull(c.TryLookup<ILogger>("file"));
            Assert.IsNull(c.TryLookup<IUserRepository>("oracle"));
            Assert.IsNull(c.TryLookup<ILogger>("mail"));
        }

        [TestMethod()]
        public void TryLookupNongenericNameless()
        {
            var c = new VenusContainer();
            c.Define(typeof(SqlUserRepository));
            c.Define(typeof(IUserRepository), typeof(OracleUserRepository));

            Assert.IsNotNull(c.TryLookup(typeof(SqlUserRepository)));
            Assert.IsNotNull(c.TryLookup(typeof(IUserRepository)));
            Assert.IsNull(c.TryLookup(typeof(FileLogger)));
            Assert.IsNull(c.TryLookup(typeof(ILogger)));
        }

        [TestMethod()]
        public void TryLookupNongenericNamed()
        {
            var c = new VenusContainer();
            c.Define(typeof(IUserRepository), typeof(SqlUserRepository), "sql");
            c.Define(typeof(ILogger), typeof(FileLogger), "file");

            Assert.IsNotNull(c.TryLookup(typeof(IUserRepository), "sql"));
            Assert.IsNotNull(c.TryLookup(typeof(ILogger), "file"));
            Assert.IsNull(c.TryLookup(typeof(IUserRepository), "oracle"));
            Assert.IsNull(c.TryLookup(typeof(ILogger), "mail"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LookupGenericNamelessThrowsExceptionIfRegistrationNotExists()
        {
            var c = new VenusContainer();
            c.Define<IUserRepository, SqlUserRepository>();

            c.Lookup<ILogger>();
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LookupGenericNamedThrowsExceptionIfRegistrationNotExists()
        {
            var c = new VenusContainer();
            c.Define<IUserRepository, SqlUserRepository>("sql");

            c.Lookup<IUserRepository>("oracle");
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LookupNongenericNamelessThrowsExceptionIfRegistrationNotExists()
        {
            var c = new VenusContainer();
            c.Define(typeof(IUserRepository), typeof(SqlUserRepository));

            c.Lookup(typeof(ILogger));
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LookupNongenericNamedThrowsExceptionIfRegistrationNotExists()
        {
            var c = new VenusContainer();
            c.Define(typeof(IUserRepository), typeof(SqlUserRepository), "sql");

            c.Lookup(typeof(IUserRepository), "oracle");
        }

        [TestMethod()]
        public void LookupListGeneric()
        {
            var c = new VenusContainer();
            c.Define<IUserRepository, SqlUserRepository>();
            c.Define<IUserRepository, OracleUserRepository>("oracle");

            Assert.AreEqual(2, c.LookupList<IUserRepository>().Count());
            Assert.AreEqual(0, c.LookupList<ILogger>().Count());
        }

        [TestMethod()]
        public void LookupListNongeneric()
        {
            var c = new VenusContainer();
            c.Define(typeof(IUserRepository), typeof(SqlUserRepository));
            c.Define(typeof(IUserRepository), typeof(OracleUserRepository), "oracle");

            Assert.AreEqual(2, c.LookupList(typeof(IUserRepository)).Count());
            Assert.AreEqual(0, c.LookupList(typeof(ILogger)).Count());
        }

        [TestMethod]
        public void LookupMapGeneric()
        {
            var c = new VenusContainer();
            c.Define<IUserRepository, SqlUserRepository>();
            c.Define<IUserRepository, OracleUserRepository>("oracle");

            var map = c.LookupMap<IUserRepository>();
            Assert.AreEqual(2, map.Count);
            Assert.IsInstanceOfType(map["default"], typeof(SqlUserRepository));
            Assert.IsInstanceOfType(map["oracle"], typeof(OracleUserRepository));

            Assert.AreEqual(0, c.LookupMap<ILogger>().Count);
        }

        [TestMethod]
        public void LookupMapNongeneric()
        {
            var c = new VenusContainer();
            c.Define(typeof(IUserRepository), typeof(SqlUserRepository));
            c.Define(typeof(IUserRepository), typeof(OracleUserRepository), "oracle");

            var map = c.LookupMap(typeof(IUserRepository));
            Assert.AreEqual(2, map.Count);
            Assert.IsInstanceOfType(map["default"], typeof(SqlUserRepository));
            Assert.IsInstanceOfType(map["oracle"], typeof(OracleUserRepository));

            Assert.AreEqual(0, c.LookupMap<ILogger>().Count);
        }
    }
}
