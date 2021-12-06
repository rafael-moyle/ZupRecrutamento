using Framework.Data.Interface.Service;
using System;

namespace Service.Base
{
    public class BaseService : IDisposable
    {
        //public ConfiguracoesDto Configuracoes { get; set; }
        //public IObjetoFactory ConversorObjetos { get; set; }
        private bool disposedValue;
        public ITransactionDb Transacao { get; set; }

        protected void AbrirTransacao()
        {
            this.Transacao.AbrirTransacao();
        }

        protected void Commit()
        {
            this.Transacao.Commit();
        }

        public void Dispose()
        {
            Dispose(disposing: true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;
            }
        }

        protected void Rollback()
        {
            this.Transacao.Rollback();
        }
    }
}
