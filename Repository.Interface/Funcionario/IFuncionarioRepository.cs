using Dto.Funcionario;
using Repository.Interface.Base;
using Repository.Model;
using System.Collections.Generic;

namespace Repository.Interface.Funcionario
{
    public interface IFuncionarioRepository : IBaseCrudRepository<FuncionarioModel>
    {
        IEnumerable<FuncionarioDto> ObterTodos(FuncionarioFiltroDto filtro = null);
    }
}
