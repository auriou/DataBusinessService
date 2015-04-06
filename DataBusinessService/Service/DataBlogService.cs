using System.Collections.Generic;
using System.Linq;
using DataBusinessService.Model;
using DataBusinessService.Repository;

namespace DataBusinessService.Service
{
    //TODO: Créer un T4 pour générer les services selon les interface ou attributs 
    public partial class DataBlogService : IBlogService, IPostService
    {
        public IEnumerable<BlogDto> GetBlogs()
        {
            return ModelWork.Execute<RepositoryBlog, IEnumerable<BlogDto>>(
                p => p.GetWithPosts().ToList().ToDtos());
        }
    }
}