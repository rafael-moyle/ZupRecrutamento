using Autofac;
using System;

namespace Ioc.Interface.Base
{
    public interface ICicloVidaRegistrador : IContainerRegistrador, IDisposable
    {
        void AtribuirCicloVida(IContainer container);
    }
}
