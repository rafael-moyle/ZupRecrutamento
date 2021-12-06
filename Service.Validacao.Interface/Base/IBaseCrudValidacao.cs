using Repository.Model.Base;

namespace Service.Validacao.Interface.Base
{
    public interface IBaseCrudValidacao<TModel> : IBaseValidacao
        where TModel : BaseModel
    {
        void ValidarAoAtualizar(TModel modelo);

        void ValidarAoInserir(TModel modelo);

        void ValidarAoRemover(TModel modelo);
    }
}
