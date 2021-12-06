using Repository.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Model
{
    [Table("funcionario")]
    public class FuncionarioModel : BaseModel
    {
        [Column("email_corporativo")]
        public string EmailCorporativo { get; set; }

        [Column("email_pessoal")]
        public string EmailPessoal { get; set; }

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("lider_id")]
        public int? LiderId { get; set; }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("numero_chapa")]
        public string NumeroChapa { get; set; }

        [Column("senha")]
        public string Senha { get; set; }

        [Column("sobrenome")]
        public string Sobrenome { get; set; }

        [Column("telefone")]
        public string Telefone { get; set; }
    }
}
