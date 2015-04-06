using System.Linq;
using DataBusinessService.Model;
using DataBusinessService.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace DataBusinessService.Test
{
    [TestClass]
    public class UnitOfWorkTest
    {
        [TestMethod]
        public void TestUnitOfWork()
        {
            BlogDto blogDto;

            using (var work = new ModelWork())
            {
                var myrepo = work.Repository<RepositoryBlog>();
                blogDto = myrepo.FirstOrDefault().ToDto();
            }

            //disconnect
            var name = blogDto.Name + "-1";
            blogDto.Name = name;


            using (var work = new ModelWork())
            {
                var blogDb = blogDto.ToDb();
                work.SaveChanges(blogDb);
            }

            //control
            using (var work = new ModelWork())
            {
                var myrepo = work.Repository<RepositoryBlog>();
                blogDto = myrepo.FirstOrDefault().ToDto();
                Check.That(blogDto.Name).IsEqualTo(name);
            }
        }

        [TestMethod]
        public void TestChildrenDtoCopy()
        {
            using (var work = new ModelWork())
            {
                var myrepo = work.Repository<RepositoryBlog>();
                var blogs = myrepo.GetWithPosts().ToDtos();
                var postDto = blogs.SelectMany(p => p.Posts).FirstOrDefault();
                Check.That(postDto is PostDto);
            }
        }
    }
}
