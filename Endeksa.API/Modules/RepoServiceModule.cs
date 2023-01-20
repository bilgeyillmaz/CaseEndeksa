using Autofac;
using Endeksa.Core.Repositories;
using Endeksa.Core.Services;
using Endeksa.Core.UnitOfWork;
using Endeksa.Core.Utilities.Cache;
using Endeksa.Core.Utilities.Cache.RedisCache;
using Endeksa.Repository;
using Endeksa.Repository.Repositories;
using Endeksa.Repository.UnitOfWorks;
using Endeksa.Service.Mapping;
using Endeksa.Service.Services;
using NLayer.Service.Services;
using System;
using System.Reflection;
using Module = Autofac.Module;
namespace NLayer.API.Modules
{
    public class RepoServiceModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            //builder.RegisterType<ICacheService>().As<RedisCacheService>();

            builder.RegisterType<TkgmService>().As<ITkgmService>();

            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(TkgmDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();

        }
    }
}
