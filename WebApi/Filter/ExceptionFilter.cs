using Framework.Compartilhado.Validacao.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace WebApi.Filter
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        public ExceptionFilter()
        {
            
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            ValidacaoException validacaoException = context.Exception as ValidacaoException;

            if (validacaoException != null)
            {
                context.HttpContext.Response.StatusCode = validacaoException.StatusCode.GetHashCode();
                context.Result = new JsonResult(validacaoException.Validacao.ObterMensagemDoErro("- ", "<br>"));
            }


            return Task.FromResult(false);
        }
    }
}
