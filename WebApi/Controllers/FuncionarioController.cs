using Dto.Funcionario;
using Microsoft.AspNetCore.Mvc;
using Service.Interface.Funcionario;
using WebApi.Base;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/funcionario")]
    public class FuncionarioController : BaseApiController
    {
        public IFuncionarioService FuncionarioService { get; set; }


        [HttpPost]
        [Route("{id:int}/alterar-senha")]
        public IActionResult AlterarSenha([FromQuery] int id)
        {
            return Ok();
        }
        
        [HttpPut]
        public IActionResult Atualizar([FromBody] FuncionarioDto funcionario)
        {
            this.FuncionarioService.Atualizar(funcionario);


            return Ok();
        }

        [HttpPost]
        public IActionResult Inserir([FromBody] FuncionarioDto funcionario)
        {
            var resultado = this.FuncionarioService.Inserir(funcionario);


            return Ok(resultado);
        }
        
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult ObterPeloId([FromRoute] int id)
        {
            var resultado = this.FuncionarioService.ObterPeloId(id);

            if (resultado == null)
            {
                return NotFound();
            }


            return Ok(resultado);
        }

        [HttpGet]
        public IActionResult ObterTodos()
        {
            var resultado = this.FuncionarioService.ObterTodos();


            return Ok(resultado);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Remover([FromRoute] int id)
        {
            this.FuncionarioService.Remover(id);


            return Ok();
        }
    }
}
