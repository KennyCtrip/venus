using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Venus.Utility;

namespace Venus.Tests
{
    [TestClass]
    public class NamespaceListTest
    {
        [TestMethod]
        public void AddTest()
        {
            var list = NamespaceList.Create();
            list.Add(new string[] { "a", "b", "c" });
            list.Add(new string[] { "ab", "cd", "ef" });
            list.Add(new string[] { "abc", "def", "ghi" });

            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void IncludeTest()
        {
            var list = NamespaceList.Create();
            list.Add(new string[] { "a"});
            list.Add(new string[] { "ab", "cd"});
            list.Add(new string[] { "abc", "def", "ghi" });

            Assert.IsTrue(list.Include(new string[] { "a"}));
            Assert.IsTrue(list.Include(new string[] { "a", "b"}));
            Assert.IsTrue(list.Include(new string[] { "ab", "cd" }));
            Assert.IsTrue(list.Include(new string[] { "ab", "cd", "ef" }));
            Assert.IsTrue(list.Include(new string[] { "abc", "def", "ghi" }));
            Assert.IsTrue(list.Include(new string[] { "abc", "def", "ghi", "jkl" }));

            Assert.IsFalse(list.Include(new string[] { "b" }));
            Assert.IsFalse(list.Include(new string[] { "ab" }));
            Assert.IsFalse(list.Include(new string[] { "ab", "ef" }));
            Assert.IsFalse(list.Include(new string[] { "abc" }));
            Assert.IsFalse(list.Include(new string[] { "abc", "def" }));
            Assert.IsFalse(list.Include(new string[] { "abc", "def", "jkl" }));
        }
    }
}
