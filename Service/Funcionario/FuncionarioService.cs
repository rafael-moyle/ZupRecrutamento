using Dto.Funcionario;
using Repository.Interface.Funcionario;
using Repository.Model;
using Service.Base;
using Service.Interface.Funcionario;
using Service.Validacao.Interface.Funcionario;
using System.Collections.Generic;
using System.Linq;

namespace Service.Funcionario
{
    public class FuncionarioService : BaseService, IFuncionarioService
    {
        public IFuncionarioRepository FuncionarioRepository { get; set; }
        public IFuncionarioValidacao FuncionarioValidacao { get; set; }


        public void Atualizar(FuncionarioDto funcionario)
        {
            FuncionarioModel modeloParaSalvar = this.FuncionarioRepository.ObterModeloPeloId(funcionario.Id);
            modeloParaSalvar.EmailCorporativo = funcionario.EmailCorporativo?.Trim();
            modeloParaSalvar.EmailPessoal = funcionario.EmailPessoal?.Trim();
            modeloParaSalvar.LiderId = funcionario.LiderId;
            modeloParaSalvar.Nome = funcionario.Nome?.Trim();
            modeloParaSalvar.NumeroChapa = funcionario.NumeroChapa?.Trim();
            modeloParaSalvar.Sobrenome = funcionario.Sobrenome?.Trim();
            modeloParaSalvar.Telefone = funcionario.Telefone?.Trim();

            this.FuncionarioValidacao.ValidarAoAtualizar(modeloParaSalvar);

            this.FuncionarioRepository.Atualizar(modeloParaSalvar);
        }

        public int Inserir(FuncionarioDto funcionario)
        {
            FuncionarioModel modeloParaSalvar = new FuncionarioModel();
            modeloParaSalvar.EmailCorporativo = funcionario.EmailCorporativo?.Trim();
            modeloParaSalvar.EmailPessoal = funcionario.EmailPessoal?.Trim();
            modeloParaSalvar.LiderId = funcionario.LiderId;
            modeloParaSalvar.Nome = funcionario.Nome?.Trim();
            modeloParaSalvar.NumeroChapa = funcionario.NumeroChapa?.Trim();
            modeloParaSalvar.Sobrenome = funcionario.Sobrenome?.Trim();
            modeloParaSalvar.Telefone = funcionario.Telefone?.Trim();

            this.FuncionarioValidacao.ValidarAoInserir(modeloParaSalvar);

            int resultado = this.FuncionarioRepository.Inserir(modeloParaSalvar);


            return resultado;
        }
        
        public FuncionarioDto ObterPeloId(int id)
        {
            FuncionarioFiltroDto filtro = new FuncionarioFiltroDto();
            filtro.Id = id;


            return this.ObterTodos(filtro).SingleOrDefault();
        }

        public IEnumerable<FuncionarioDto> ObterTodos(FuncionarioFiltroDto filtro = null)
        {
            var resultado = this.FuncionarioRepository.ObterTodos(filtro);


            return resultado;
        }

        public void Remover(int id)
        {
            var modeloParaRemover = this.FuncionarioRepository.ObterModeloPeloId(id);

            this.FuncionarioValidacao.ValidarAoRemover(modeloParaRemover);

            this.FuncionarioRepository.Remover(modeloParaRemover);
        }
    }
}
