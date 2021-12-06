using Autofac;
using Autofac.Core;
using Autofac.Extras.DynamicProxy;
using Framework.Data;
using Framework.Data.Interface;
using Framework.Data.Interface.Service;
using Ioc.Interface.Base;
using Ioc.Middleware;
using Microsoft.Extensions.Configuration;
using Repository.Base;
using Service.Base;
using Service.Validacao.Base;
using System.Reflection;

namespace Ioc
{
    public class RegistradorIoc
    {
        private static IContainer _container;
        private static IContainerRegistrador _registrador;

        public static void Registrar(ContainerBuilder builder, IContainerRegistrador registrador)
        {
            _registrador = registrador;
            RegistrarModulos(builder, registrador);

            if (registrador is ICicloVidaRegistrador cicloVidaRegistrador)
            {
                AbrirCicloVida(builder, cicloVidaRegistrador);
            }
        }

        private static void AbrirCicloVida(ContainerBuilder builder, ICicloVidaRegistrador registro)
        {
            _container = builder.Build();

            registro.AtribuirCicloVida(_container);
        }

        private static void ConfigurarHandler(ContainerBuilder builder)
        {
            // Registrar interceptador caso de erro, o rollback ou commit é automático
            builder.Register(c => new TransacaoInterceptador(c.Resolve<ITransactionDb>()))
                   .AsSelf()
                   .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }

        private static void OnActivatingInstanceForTesting<TypeOf>(IActivatingEventArgs<TypeOf> e)
        {
            
        }

        private static void RegistrarModulos(ContainerBuilder builder, IContainerRegistrador registrador)
        {
            builder.RegisterType<DbConnectionServices>()
                .As<ITransactionDb>()
                .As<IDbService>()
                .UsingConstructor(typeof(string))
                .InstancePerLifetimeScope()
                .WithParameters(new[] {
                    new NamedParameter("connectionString", registrador.Configuration?.GetConnectionString("Default"))
                });

            builder.RegisterAssemblyTypes(Assembly.Load(typeof(BaseServiceRepository).Assembly.GetName()))
                .Where(t => t.Name.EndsWith("Repository"))
                .OnActivating(OnActivatingInstanceForTesting)
                .AsImplementedInterfaces()
                .EnableClassInterceptors()
                .InterceptedBy(typeof(TransacaoInterceptador))
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterAssemblyTypes(Assembly.Load(typeof(BaseService).Assembly.GetName()))
                .Where(t => t.Name.EndsWith("Service"))
                .OnActivating(OnActivatingInstanceForTesting)
                .AsImplementedInterfaces()
                .EnableClassInterceptors()
                .InterceptedBy(typeof(TransacaoInterceptador))
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterAssemblyTypes(Assembly.Load(typeof(BaseValidacao).Assembly.GetName()))
                .Where(t => t.Name.EndsWith("Validacao"))
                .AsImplementedInterfaces()
                .EnableClassInterceptors()
                .InterceptedBy(typeof(TransacaoInterceptador))
                .InstancePerDependency()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            ConfigurarHandler(builder);

            registrador.RegistrarComponentesInternos(builder);
        }
    }
}
