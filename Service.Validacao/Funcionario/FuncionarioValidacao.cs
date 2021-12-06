using Repository.Model;
using Service.Validacao.Base;
using Service.Validacao.Interface.Funcionario;
using System;
using System.Text.RegularExpressions;

namespace Service.Validacao.Funcionario
{
    public class FuncionarioValidacao : BaseCrudValidacao<FuncionarioModel>, IFuncionarioValidacao
    {
        public override void ValidarAoAtualizar(FuncionarioModel modelo)
        {
            this.EmailCorporativo_DeveSerValido(modelo);
            this.EmailCorporativo_EhObrigatorio(modelo);
            this.Nome_EhObrigatorio(modelo);

            this.VerificarValidacao();
        }

        public override void ValidarAoInserir(FuncionarioModel modelo)
        {
            this.EmailCorporativo_DeveSerValido(modelo);
            this.EmailCorporativo_EhObrigatorio(modelo);
            this.Nome_EhObrigatorio(modelo);

            this.VerificarValidacao();
        }

        public override void ValidarAoRemover(FuncionarioModel modelo)
        {
            
        }


        private void EmailCorporativo_DeveSerValido(FuncionarioModel modelo)
        {
            if (string.IsNullOrWhiteSpace(modelo.EmailCorporativo) == true)
            {
                return;
            }

            var ehValido = ValidarSeEmailEhValido(modelo.EmailCorporativo);

            if (ehValido == false)
            {
                this.validacaoSumario.AddErro("Funcionario", "O e-mail corporativo do funcionário deve ser válido.");
            }
        }

        private void EmailCorporativo_EhObrigatorio(FuncionarioModel modelo)
        {
            if (string.IsNullOrWhiteSpace(modelo.EmailCorporativo) == false)
            {
                return;
            }

            this.validacaoSumario.AddErro("Funcionario", "O e-mail corporativo do funcionário deve ser informado.");
        }

        private void Nome_EhObrigatorio(FuncionarioModel modelo)
        {
            if(string.IsNullOrWhiteSpace(modelo.Nome) == false)
            {
                return;
            }

            this.validacaoSumario.AddErro("Funcionario", "O nome do funcionário deve ser informado.");
        }

        private bool ValidarSeEmailEhValido(string email)
        {
            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
