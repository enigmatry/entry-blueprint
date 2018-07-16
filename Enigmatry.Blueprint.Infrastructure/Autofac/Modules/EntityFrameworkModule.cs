using System;
using System.Linq;
using Autofac;
using Enigmatry.Blueprint.Core.Data;
using Enigmatry.Blueprint.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Enigmatry.Blueprint.Infrastructure.Autofac.Modules
{
    public class EntityFrameworkModule : Module
    {
        public DbContextOptions DbContextOptions { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.RegisterGeneric(typeof(EntityFrameworkRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EntityFrameworkQuery<>))
                .As(typeof(IQueryable<>)).InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(IRepository<>).Assembly)
                .Where(
                    type =>
                        ImplementsInterface(typeof(IRepository<>), type) ||
                        type.Name.EndsWith("Repository")
                ).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.Register(c => DbContextOptions).As<DbContextOptions>().InstancePerLifetimeScope();
            // needs to be registered both as self and as DbContext or the tests might not work as expected
            builder.RegisterType<BlueprintContext>().AsSelf().As<DbContext>().InstancePerLifetimeScope();
            builder.RegisterType<DbContextUnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
        }

        private static bool ImplementsInterface(Type interfaceType, Type concreteType)
        {
            return
                concreteType.GetInterfaces().Any(
                    t =>
                        (interfaceType.IsGenericTypeDefinition && t.IsGenericType
                            ? t.GetGenericTypeDefinition()
                            : t) == interfaceType);
        }
    }
}