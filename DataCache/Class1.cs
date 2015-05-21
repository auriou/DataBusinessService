using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCache
{
    public class Class1
    {
        public void Test()
        {
            var category = new Category();
            var cacheClient = new CacheClient();
            cacheClient.Store<Category>(category);

            var categories = cacheClient.GetAll<Category>();

            IRedisTypedClient<Category> phones = redisClient.As<Category>();
            Phone phoneFive = phones.GetValue("5");
        }
    }

    internal class Category
    {
    }

    internal class CacheClient
    {
        public CacheClient()
        {
        }

        internal object GetAll<T>()
        {
            throw new NotImplementedException();
        }

        internal void Store<T>(T category)
        {
            throw new NotImplementedException();
        }
    }
}
