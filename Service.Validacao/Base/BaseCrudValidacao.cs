using Framework.Compartilhado.Validacao.Api;
using Repository.Model.Base;
using Service.Validacao.Interface.Base;

namespace Service.Validacao.Base
{
    public abstract class BaseCrudValidacao<TModel> : BaseValidacao, IBaseCrudValidacao<TModel>
            where TModel : BaseModel
    {
        public BaseCrudValidacao()
        {

        }

        public virtual void ValidarAoAtualizar(TModel modelo)
        {
            this.ValidarModelo(modelo);
        }

        public virtual void ValidarAoInserir(TModel modelo)
        {
            this.ValidarModelo(modelo);
        }

        public virtual void ValidarAoRemover(TModel modelo)
        {
            this.ValidarModelo(modelo);
        }

        private void ValidarModelo(TModel modelo)
        {
            if (modelo == null)
            {
                throw new ValidacaoException(typeof(TModel).Name, "Um modelo nulo não pode ser validado.");
            }
        }
    }
}
