using System.Linq;
using DataBusinessService.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace DataBusinessService.Test
{
    [TestClass]
    public class ServiceTest
    {
        [TestMethod]
        public void TestService()
        {
            var data = new DataBlogService();
            var res = data.GetBlogs();
            Check.That(res).IsNotNull();
            Check.That(res.Any());
        }
    }
}
