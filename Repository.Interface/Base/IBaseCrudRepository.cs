using Repository.Model.Base;
using System.Collections.Generic;

namespace Repository.Interface.Base
{
    public interface IBaseCrudRepository<TModel> : IBaseRepository
        where TModel : BaseModel
    {
        int Atualizar(TModel model);

        int Inserir(TModel model);

        TModel ObterModeloPeloId(int id);

        IEnumerable<TModel> ObterTodos();

        int Remover(TModel model);
    }
}
