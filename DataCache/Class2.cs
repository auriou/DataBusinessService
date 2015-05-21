using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCache
{

    //mettre attributs sur les dto pour enumerer une liste d'identifiant du cache
    public class AppFabricCache<T> : ICache<T>
    {

        public void Set(string key, T value)
        {
            Set(key, value, default(TimeSpan));
        }

        public void Set(string key, T value, TimeSpan expirationTimeSpan)
        {

            var token = (T)AppFabricCacheProvider.Cache.Get(key);
            if (token == null)
            {
                AppFabricCacheProvider.Cache.Add(key, value, expirationTimeSpan);
            }
            else
            {
                AppFabricCacheProvider.Cache[key] = value;
                if (expirationTimeSpan != default(TimeSpan))
                {
                    AppFabricCacheProvider.Cache.ResetObjectTimeout(key, expirationTimeSpan);
                }
            }
        }

        public void Remove(string key)
        {
            AppFabricCacheProvider.Cache.Remove(key);
        }

        public T Get(string key)
        {
            return (T)AppFabricCacheProvider.Cache.Get(key);
        }
    }




    internal class AppFabricCacheProvider
    {
        private static DataCacheFactory dataCacheFactory;
        private static DataCache dataCache;

        private static readonly string APPFABRIC_SERVER_HOSTNAME =
            ConfigurationManager.AppSettings["APPFABRIC_SERVER_HOSTNAME"];

        private static readonly int APPFABRIC_SERVER_PORT =
            Int32.Parse(ConfigurationManager.AppSettings["APPFABRIC_SERVER_PORT"]);

        private static readonly string APPFABRIC_CACHENAME = ConfigurationManager.AppSettings["APPFABRIC_CACHENAME"];

        public static DataCache Cache
        {
            get { return GetCache(); }
        }

        private static DataCache GetCache()
        {
            if (dataCache != null)
                return dataCache;

            Console.WriteLine(APPFABRIC_SERVER_HOSTNAME);
            Console.WriteLine(APPFABRIC_SERVER_PORT);

            var servers = new List<DataCacheServerEndpoint>(1)
            {
                new DataCacheServerEndpoint(APPFABRIC_SERVER_HOSTNAME, APPFABRIC_SERVER_PORT)
            };

            var configuration = new DataCacheFactoryConfiguration
            {
                Servers = servers,
                LocalCacheProperties = new DataCacheLocalCacheProperties()
            };

            DataCacheClientLogManager.ChangeLogLevel(System.Diagnostics.TraceLevel.Off);

            dataCacheFactory = new DataCacheFactory(configuration);
            dataCache = dataCacheFactory.GetCache(APPFABRIC_CACHENAME);

            return dataCache;
        }
    }


    public interface ICache<T>
    {
        T Get(string key);
        void Set(string key, T obj);
        void Set(string key, T obj, TimeSpan expirationTimeSpan);
        void Remove(string key);
    }
}
