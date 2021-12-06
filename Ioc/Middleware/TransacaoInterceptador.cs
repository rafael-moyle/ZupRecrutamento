using Castle.DynamicProxy;
using Framework.Data.Interface.Service;
using System;

namespace Ioc.Middleware
{
    /// <summary>
    /// Interceptador para cada requisição. Em caso de erro, o rollback será automático
    /// </summary>
    public class TransacaoInterceptador : IInterceptor
    {
        private readonly ITransactionDb _transaction;

        public TransacaoInterceptador(ITransactionDb transaction)
        {
            this._transaction = transaction;
        }

        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception excecao)
            {
                this.TentarRollback();
                
                // Aqui pode ser inserido alguma lógica para gravar erros customizados ou enviar um alerta para os devs

                throw;
            }
        }

        private void TentarRollback()
        {
            try
            {
                this._transaction.Rollback();
            }
            finally
            {

            }
        }
    }
}
