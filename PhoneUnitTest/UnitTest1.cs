using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace PhoneUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            WP8_SqliteDemo.SqlHelper helper = new WP8_SqliteDemo.SqlHelper();

            Assert.IsTrue(helper.OpenDb());

            Assert.IsTrue(helper.CreateTable());
        }
    }
}
