using Framework.Compartilhado.Validacao;
using Framework.Compartilhado.Validacao.Api;
using System;

namespace Service.Validacao.Base
{
    public abstract class BaseValidacao : IDisposable
    {
        protected ValidacaoSumario validacaoSumario;
        private bool disposedValue;

        public BaseValidacao()
        {
            validacaoSumario = new ValidacaoSumario();
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    validacaoSumario = null;
                }

                disposedValue = true;
            }
        }

        protected virtual void VerificarValidacao()
        {
            if (validacaoSumario != null && validacaoSumario.PossuiErro)
            {
                throw new ValidacaoException(validacaoSumario);
            }
        }
    }
}
