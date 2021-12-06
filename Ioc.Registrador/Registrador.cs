using Autofac;
using Ioc.Interface.Base;

namespace Ioc.Registrador
{
    public static class Registrador
    {
        public static void Registrar(ContainerBuilder builder, IContainerRegistrador registrador)
        {
            RegistradorIoc.Registrar(builder, registrador);
        }
    }
}
