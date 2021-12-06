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

            //if (registrador is ITesteRegistrador testeRegistrador)
            //    AbrirCicloVidaTeste(builder, testeRegistrador);
            //else if (registrador is ICicloVidaRegistrador cicloVidaRegistrador)
            //    AbrirCicloVida(builder, cicloVidaRegistrador);

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

        //private static void AbrirCicloVidaTeste(ContainerBuilder builder, ITesteRegistro registro)
        //{
        //    registro.ConfigurarTeste(builder);
        //    _container = builder.Build();

        //    registro.AtribuirCicloVida(_container);
        //}

        //private static void ConfigurarAutoMapper(ContainerBuilder builder, bool ehAmbienteTeste = false)
        //{
        //    builder.RegisterType<MoqObjeto>()
        //        .AsSelf()
        //        .As<IMoqObjeto>()
        //        .InstancePerLifetimeScope();

        //    builder.RegisterType<ConversorObjetos>()
        //           .As<IObjetoFactory>()
        //           .SingleInstance()
        //           .UsingConstructor(typeof(bool))
        //           .WithParameters(new[] { new NamedParameter("ehAmbienteTeste", ehAmbienteTeste) })
        //           .PropertiesAutowired();
        //}

        private static void ConfigurarHandler(ContainerBuilder builder)
        {
            // Registrar interceptador caso de erro, o rollback ou commit é automático
            builder.Register(c => new TransacaoInterceptador(c.Resolve<ITransactionDb>()))
                   .AsSelf()
                   .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }

        private static void OnActivatingInstanceForTesting<TypeOf>(IActivatingEventArgs<TypeOf> e)
        {
            //if (_registrador is ITesteRegistrador testeRegistro)
            //    testeRegistro.OnActivatingInstance<TypeOf>(e);
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

            //builder.RegisterType<CustomLogger>()
            //    .As<ICustomLogger>()
            //    .UsingConstructor(typeof(ConfigLog))
            //    .InstancePerLifetimeScope()
            //    .WithParameters(new[] {
            //        new NamedParameter("configLog", new ConfigLog()
            //        {
            //            Tipo = registrador.Configuracoes.LogConfig.Tipo,
            //            Ativo = registrador.Configuracoes.LogConfig.Ativo,
            //            DbConfigLog = new DbConfigLog()
            //            {
            //                Provedor = "MySql.Data.MySqlClient",
            //                StringConexao = conexaoDto.Default
            //            }
            //        })
            //    });

            //builder.Register<ConfiguracoesDto>((s) => registrador.Configuracoes)
            //       .AsSelf()
            //       .SingleInstance();

            //builder.RegisterType<CommonUtils>()
            //    .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
            //    .AsSelf()
            //    .SingleInstance();

            //builder.RegisterType<MailUtils>()
            //    .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
            //    .AsSelf();

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

            //builder.RegisterAssemblyTypes(Assembly.Load(typeof(BaseFactory).Assembly.GetName()))
            //    .Where(t => t.Name.EndsWith("Factory"))
            //    .OnActivating(OnActivatingInstanceForTesting)
            //    .AsImplementedInterfaces()
            //    .InstancePerLifetimeScope()
            //    .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            
            //ConfigurarAutoMapper(builder, registrador is ITestRegistro);
            ConfigurarHandler(builder);

            registrador.RegistrarComponentesInternos(builder);
        }
    }
}
