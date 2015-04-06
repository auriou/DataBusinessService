using System;
using DataBusinessService.Core;
using DataBusinessService.Model;

namespace DataBusinessService
{
    public class ModelWork : UnitOfWork<ModelContext>, IContextGen
    {
        public ModelWork() : base(new ModelContext())
        {
        }

        public ModelWork(ModelContext context) : base(context)
        {
        }


        //TODO: Ajouter automatiquement les mappages avec le T4
        static ModelWork()
        {
            BlogDto.InitializeMapper();
            PostDto.InitializeMapper();
        }

        public static TResult Execute<TRepo, TResult>(Func<TRepo, TResult> function) where TResult : class where TRepo : class
        {
            using (var work = new ModelWork())
            {
                return function.Invoke(work.Repository<TRepo>());
            }
        }
    }
}