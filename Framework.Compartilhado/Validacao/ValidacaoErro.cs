using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Compartilhado.Validacao
{
    public class ValidacaoErro
    {
        private readonly List<string> erros;
        private readonly string referencia;

        public ValidacaoErro(string referencia, string mensagem)
        {
            this.erros = new List<string>() { mensagem };
            this.referencia = referencia;
        }

        public ValidacaoErro(string referencia, List<string> listaDeMensagens)
        {
            this.erros = listaDeMensagens;
            this.referencia = referencia;
        }

        public bool PossuiErro
        {
            get
            {
                return erros.Count > 0;
            }
        }

        public List<string> Erros
        {
            get
            {
                return this.erros;
            }
        }

        public string Referencia
        {
            get
            {
                return this.referencia;
            }
        }

        public string ObterMensagemDeErro(string indicardorDoErro = null, string separador = null)
        {
            if (!this.PossuiErro)
            {
                return null;
            }

            string _errorSeparator = separador ?? Environment.NewLine;

            List<string> errorsList = this.Erros
                .Select((message) =>
                {
                    return $"{indicardorDoErro}{message}";
                }).ToList();

            string resultado = string.Join(_errorSeparator, errorsList);


            return resultado;
        }
    }
}
