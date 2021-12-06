using System;
using System.Data;

namespace Framework.Data.Interface
{
    public interface IDbService : IDisposable
    {
        IDbConnection Connection { get; }

        IDbTransaction Transaction { get; }
    }
}
