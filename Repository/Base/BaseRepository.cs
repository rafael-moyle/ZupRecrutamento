using Repository.Interface.Base;
using System;

namespace Repository.Base
{
    public class BaseRepository : IBaseRepository
    {
        private bool disposedValue;

        public BaseRepository()
        {

        }

        ~BaseRepository()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;
            }
        }
    }
}
