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
                  fun.email_corporativo EmailCorporativo
                  , fun.email_pessoal EmailPessoal
                  , fun.id Id
                  , fun.lider_id LiderId
                  , fun.nome Nome
                  , fun.numero_chapa NumeroChapa
                  , fun.senha Senha
                  , fun.sobrenome Sobrenome
                  , fun.telefone Telefone
                  , RTRIM(CONCAT(lid.nome, ' ', lid.sobrenome)) LiderNome
                FROM 
                  funcionario fun
                LEFT JOIN
                  funcionario lid on (lid.id = fun.lider_id)
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
