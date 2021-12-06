using Dapper;
using Framework.Data.Interface;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Repository.Base
{
    public class BaseServiceRepository : BaseRepository
    {
        private bool disposedValue = false;
        protected readonly IDbService _dbService;

        public BaseServiceRepository(IDbService dbService)
        {
            lock (this)
            {
                this._dbService = dbService;
                ConfigureDapper();
            }
        }

        private void ConfigureDapper()
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    ExecutarAcoesAntesDoDispose();
                }

                disposedValue = true;
            }
        }

        protected virtual void ExecutarAcoesAntesDoDispose()
        {

        }

        protected virtual int Execute(string sql, DynamicParameters parameters = null)
        {
            return this._dbService.Connection.Execute(sql, parameters);
        }

        protected virtual IDataReader ExecuteReader(string sql, DynamicParameters parameters = null)
        {
            return this._dbService.Connection.ExecuteReader(sql, parameters, transaction: this._dbService.Transaction);
        }

        protected virtual T ExecuteScalar<T>(string sql, DynamicParameters parameters = null)
        {
            return this._dbService.Connection.ExecuteScalar<T>(sql, parameters, transaction: this._dbService.Transaction);
        }

        protected virtual IEnumerable<T> Select<T>(string sql, DynamicParameters parameters = null)
        {
            return this._dbService.Connection.Query<T>(sql, parameters, transaction: this._dbService.Transaction);
        }

        protected virtual Task<IEnumerable<T>> SelectAsync<T>(string sql, DynamicParameters parameters = null)
        {
            return this._dbService.Connection.QueryAsync<T>(sql, parameters, transaction: this._dbService.Transaction);
        }

        protected virtual T SelectFirstOrDefault<T>(string sql, DynamicParameters parameters = null)
        {
            return this._dbService.Connection.QueryFirstOrDefault<T>(sql, parameters, transaction: this._dbService.Transaction);
        }
    }
}
