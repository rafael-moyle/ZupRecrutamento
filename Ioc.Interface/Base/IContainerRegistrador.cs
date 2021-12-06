using Autofac;
using Autofac.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Ioc.Interface.Base
{
    public interface IContainerRegistrador
    {
        IConfiguration Configuration { get; }
        
        HttpContextAccessor HttpContextAccessor { get; }

        void OnActivatingInstance<TypeOf>(IActivatingEventArgs<TypeOf> e);

        void RegistrarComponentesInternos(ContainerBuilder builder);
    }
}
