using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Compartilhado.Validacao
{
    public class ValidacaoSumario
    {
        private readonly Dictionary<string, ValidacaoErro> erros;

        public ValidacaoSumario()
        {
            this.erros = new Dictionary<string, ValidacaoErro>();
        }

        public bool PossuiErro
        {
            get
            {
                return erros.Count > 0;
            }
        }

        public string MensagemDoErro
        {
            get
            {
                return this.ObterMensagemDoErro();
            }
        }

        public Dictionary<string, ValidacaoErro> Erros
        {
            get
            {
                return this.erros;
            }
        }

        public void AddErro(string referencia, string mensagem)
        {
            if (!erros.ContainsKey(referencia))
            {
                ValidacaoErro validacaoErro = new ValidacaoErro(referencia, mensagem);

                erros.Add(referencia, validacaoErro);

                return;
            }

            erros[referencia].Erros.Add(mensagem);
        }

        public void AddValidacaoErro(ValidacaoErro validacaoErro)
        {
            if (!erros.ContainsKey(validacaoErro.Referencia))
            {
                erros.Add(validacaoErro.Referencia, validacaoErro);

                return;
            }

            erros[validacaoErro.Referencia].Erros.AddRange(validacaoErro.Erros);
        }

        public string ObterMensagemDoErro(string indicadorDoErro = null, string separador = null)
        {
            if (!this.PossuiErro)
            {
                return null;
            }

            string _separador = separador ?? Environment.NewLine;

            List<string> errorList = this.Erros
                .Select((dictError) =>
                {
                    string errosDaReferencia = dictError.Value.ObterMensagemDeErro(indicadorDoErro, _separador);

                    return errosDaReferencia;
                }).ToList();

            string resultado = string.Join(_separador, errorList);


            return resultado;
        }
    }
}
