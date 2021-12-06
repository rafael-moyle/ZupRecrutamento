using Dto.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Dto.Funcionario
{
    public class FuncionarioDto : BaseDto
    {
        /// <summary>
        /// E-mail do funcionário dentro da empresa
        /// </summary>
        public string EmailCorporativo { get; set; }

        /// <summary>
        /// E-mail pessoal do funcionário
        /// </summary>
        public string EmailPessoal { get; set; }

        /// <summary>
        /// Id do líder deste funcionário (também é um funcionário)
        /// </summary>
        public int? LiderId { get; set; }

        /// <summary>
        /// O nome do líder deste funcionário
        /// </summary>
        public string LiderNome { get; set; }

        /// <summary>
        /// Primeiro nome do funcionário
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Número de chapa do funcionário
        /// </summary>
        public string NumeroChapa { get; set; }

        /// <summary>
        /// A senha para acesso do funcionário
        /// </summary>
        public string Senha { get; set; }

        /// <summary>
        /// Sobrenome do funcionário
        /// </summary>
        public string Sobrenome { get; set; }

        /// <summary>
        /// Telefones
        /// </summary>
        public string Telefone { get; set; }
    }
}
