using Dapper;
using Dto.Funcionario;
using Framework.Data.Interface;
using Repository.Base;
using Repository.Interface.Funcionario;
using Repository.Model;
using System.Collections.Generic;
using System.Text;

namespace Repository.Funcionario
{
    public class FuncionarioRepository : BaseCrudRepository<FuncionarioModel>, IFuncionarioRepository
    {
        public FuncionarioRepository(IDbService dbService)
            : base(dbService)
        {

        }

        public IEnumerable<FuncionarioDto> ObterTodos(FuncionarioFiltroDto filtro = null)
        {
            filtro = filtro ?? new FuncionarioFiltroDto();

            DynamicParameters parametroLista = new DynamicParameters();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(
                @"
                SELECT 
                  fun.email_corporativo
                  , fun.email_pessoal
                  , fun.id
                  , fun.lider_id
                  , fun.nome
                  , fun.numero_chapa
                  , fun.senha
                  , fun.sobrenome
                  , fun.telefone
                FROM 
                  funcionario fun
                WHERE
                  1 = 1 ");

            // Filtros
            if (filtro.Id.HasValue == true)
            {
                sql.AppendLine("  and fun.id = @Id ");
                parametroLista.Add("Id", filtro.Id);
            }

            if (string.IsNullOrWhiteSpace(filtro.NumeroChapa) == false)
            {
                sql.AppendLine("  and fun.numero_chapa = @NumeroChapa ");
                parametroLista.Add("NumeroChapa", filtro.NumeroChapa?.Trim());
            }

            // Ordem
            sql.AppendLine(
                @"ORDER BY
                    fun.nome ");

            var resultado = this.Select<FuncionarioDto>(sql.ToString(), parametroLista);


            return resultado;
        }
    }
}
