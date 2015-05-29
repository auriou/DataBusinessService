using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataBusinessService.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            DataCache.Test test = new DataCache.Test();

            test.Start();
        }
    }
}
