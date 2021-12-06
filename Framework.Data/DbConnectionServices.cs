using Framework.Data.Interface;
using Framework.Data.Interface.Service;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Framework.Data
{
    public class DbConnectionServices : IDbService, ITransactionDb
    {
        private readonly string _connectionString;
        private IDbConnection _dbConnection;
        private IDbTransaction _dbTransaction;
        private int _transactionCount;
        private bool disposedValue = false;

        public DbConnectionServices(string connectionString)
        {
            lock (this)
            {
                this._connectionString = connectionString;
                this._transactionCount = 0;
            }
        }

        public IDbConnection Connection
        {
            get
            {
                lock (this)
                {
                    OpenConnection();

                    if (TransacaoExiste)
                    {
                        return _dbTransaction.Connection;
                    }

                    return _dbConnection;
                }
            }
        }

        public bool TransacaoExiste
        {
            get
            {
                return this._dbTransaction != null;
            }
        }

        public IDbTransaction Transaction
        {
            get
            {
                return _dbTransaction;
            }
        }

        public void AbrirTransacao()
        {
            lock (this)
            {
                if (!TransacaoExiste)
                    this._dbTransaction = this.Connection.BeginTransaction();

                this._transactionCount++;
            }
        }

        public void Commit()
        {
            if (TransacaoExiste)
            {
                if (this._transactionCount <= 1)
                {
                    this._dbTransaction.Commit();
                    this._dbTransaction = null;
                }

                this._transactionCount--;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Rollback()
        {
            if (TransacaoExiste && _dbTransaction.Connection.State == ConnectionState.Open)
            {
                this._dbTransaction.Rollback();
                this._dbTransaction = null;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    CloseConnection();
                }

                disposedValue = true;
            }
        }

        private void CloseConnection(bool? mustCommitPendingTransactions = null)
        {
            if (this._dbConnection != null)
            {
                if (mustCommitPendingTransactions.HasValue)
                {
                    if (mustCommitPendingTransactions.Value)
                    {
                        Commit();
                    }
                    else
                    {
                        Rollback();
                    }
                }

                this._dbConnection.Dispose();
                this._dbConnection.Close();

                if (TransacaoExiste)
                    this._dbTransaction.Dispose();

                this._dbConnection = null;
                this._dbTransaction = null;
            }
        }

        private void OpenConnection()
        {
            lock (this)
            {
                if (_dbConnection == null || _dbConnection.State == ConnectionState.Closed)
                {
                    _dbConnection = new SqlConnection(this._connectionString);
                    _dbConnection.Open();
                }
            }
        }
    }
}
