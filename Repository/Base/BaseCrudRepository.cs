using Dapper;
using Framework.Data.Interface;
using Repository.Interface.Base;
using Repository.Model.Base;
using System.Collections.Generic;

namespace Repository.Base
{
    public class BaseCrudRepository<TModel> : BaseServiceRepository, IBaseCrudRepository<TModel>
        where TModel : BaseModel
    {
        public BaseCrudRepository(IDbService dbService)
                   : base(dbService)
        {
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLServer);
        }

        public virtual int Atualizar(TModel entity)
        {
            return this._dbService.Connection.Update<TModel>(entity, transaction: this._dbService.Transaction);
        }

        public virtual int Inserir(TModel entity)
        {
            return this._dbService.Connection.Insert<int, TModel>(entity, transaction: this._dbService.Transaction);
        }

        public virtual TModel ObterModeloPeloId(int id)
        {
            return this._dbService.Connection.Get<TModel>(id, transaction: this._dbService.Transaction);
        }

        public virtual IEnumerable<TModel> ObterTodos()
        {
            IEnumerable<TModel> resultado = this._dbService.Connection.GetList<TModel>();

            return resultado;
        }

        public virtual int Remover(TModel entity)
        {
            return this._dbService.Connection.Delete<TModel>(entity, transaction: this._dbService.Transaction);
        }
    }
}
