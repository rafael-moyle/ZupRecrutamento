using Dto.Funcionario;
using Service.Interface.Base;
using System.Collections.Generic;

namespace Service.Interface.Funcionario
{
    public interface IFuncionarioService : IBaseService
    {
        void Atualizar(FuncionarioDto funcionario);

        int Inserir(FuncionarioDto funcionario);
        
        FuncionarioDto ObterPeloId(int id);

        IEnumerable<FuncionarioDto> ObterTodos(FuncionarioFiltroDto filtro = null);

        void Remover(int id);
    }
}
