namespace Framework.Compartilhado.Validacao.Api
{
    public class ValidacaoException : ApiException
    {
        private readonly ValidacaoSumario validacao;

        public ValidacaoException()
            : base(System.Net.HttpStatusCode.PreconditionFailed)
        {
        }

        public ValidacaoException(ValidacaoSumario validacao)
            : base(System.Net.HttpStatusCode.PreconditionFailed)
        {
            this.validacao = validacao;
        }

        public ValidacaoException(string referencia, string mensagem)
            : base(System.Net.HttpStatusCode.PreconditionFailed)
        {
            this.validacao = new ValidacaoSumario();
            this.validacao.AddErro(referencia, mensagem);
        }

        public ValidacaoSumario Validacao
        {
            get
            {
                return this.validacao;
            }
        }
    }
}
