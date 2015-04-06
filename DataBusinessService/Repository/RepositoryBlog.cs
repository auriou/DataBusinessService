using System.Data.Entity;
using System.Linq;
using DataBusinessService.Core;
using DataBusinessService.Model;

namespace DataBusinessService.Repository
{
    public class RepositoryBlog : EfRepository<ModelContext, Blog, int>

    {
        public RepositoryBlog(ModelContext context) : base(context)
        {
        }

        //Sauvegarde sp�cifique
        public void SaveData()
        {
        }

        public IQueryable<Blog> GetWithPosts()
        {
            return GetAll().Include(p => p.Posts).AsQueryable();
        }
    }
}