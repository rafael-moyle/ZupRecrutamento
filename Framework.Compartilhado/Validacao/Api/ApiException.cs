using System.Net;

namespace Framework.Compartilhado.Validacao.Api
{
    public class ApiException : System.Exception
    {
        private readonly HttpStatusCode statusCode;

        public ApiException(HttpStatusCode statusCode, string message, System.Exception ex)
            : base(message, ex)
        {
            this.statusCode = statusCode;
        }

        public ApiException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            this.statusCode = statusCode;
        }

        public ApiException(HttpStatusCode statusCode)
        {
            this.statusCode = statusCode;
        }

        public HttpStatusCode StatusCode
        {
            get
            {
                return this.statusCode;
            }
        }
    }
}
