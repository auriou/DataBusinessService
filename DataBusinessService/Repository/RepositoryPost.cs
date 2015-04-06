using DataBusinessService.Core;
using DataBusinessService.Model;

namespace DataBusinessService.Repository
{
    public class RepositoryPost : EfRepository<ModelContext, Blog, int>

    {
        public RepositoryPost(ModelContext context) : base(context)
        {
        }

    }
}