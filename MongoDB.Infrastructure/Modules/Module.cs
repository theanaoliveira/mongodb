using Autofac;
using AutoMapper;
using MongoDB.Infrastructure.MongoDBAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDB.Infrastructure.Modules
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var connection = Environment.GetEnvironmentVariable("MONGO_CONN");

            builder.RegisterAssemblyTypes(typeof(InfrastructureException).Assembly)
                .Where(w => w.Namespace.Contains("MongoDBAccess"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(InfrastructureException).Assembly)
                .Where(t => t.Namespace.Contains("MongoDBAccess") && typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic)
                .As<Profile>();

            builder.Register(c => new MapperConfiguration(cfg =>
            {
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                    cfg.AddProfile(profile);

            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>()
                .CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();

            if (!string.IsNullOrEmpty(connection))
            {
                using (var context = new MongoContext())
                {
                    
                }
            }
        }
    }
}
