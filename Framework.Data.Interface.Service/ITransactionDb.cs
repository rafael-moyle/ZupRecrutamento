using System;

namespace Framework.Data.Interface.Service
{
    public interface ITransactionDb : IDisposable
    {
        bool TransacaoExiste { get; }

        void AbrirTransacao();

        void Commit();

        void Rollback();
    }
}
