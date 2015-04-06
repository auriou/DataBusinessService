using System.Linq;
using DataBusinessService.Model;
using DataBusinessService.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataBusinessService.Test
{
    [TestClass]
    public class ModelContTest
    {
        [TestMethod]
        public void TestContext()
        {
            using (var db = new ModelContext())
            {
                var res = db.Blogs.MapToList<BlogDto>().ToList();
            }
        }

        [TestMethod]
        public void TestAdd()
        {
            //using (var db = new ModelContext())
            //{
            //    db.Blogs.Add(new Blog() { Name = "Toto", Url = "www.toto.fr" });
            //    db.Blogs.Add(new Blog() {  Name = "Titi", Url = "www.titi.fr" });
            //    db.SaveChanges();
            //}

            using (var db = new ModelContext())
            {
                var blog = db.Blogs.FirstOrDefault();
                blog.Posts.Add(new Post() {Title = "Titre", Content = "Contenu"});
                blog.Posts.Add(new Post() { Title = "Titre 2", Content = "Contenu 2" });
                db.SaveChanges();
            }
        }

        [TestMethod]
        public void TestRepo()
        {
            using (var db = new ModelContext())
            {
                var repo = new RepositoryBlog(db);
                var res = repo.GetAllList().MapToList<BlogDto>();
                var res2 = repo.GetAllList().ToDtos();
            }
        }
    }
}
