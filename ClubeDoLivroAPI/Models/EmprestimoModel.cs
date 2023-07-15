using System.ComponentModel.DataAnnotations.Schema;

namespace ClubeDoLivroAPI.Models
{
    public class EmprestimoModel
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int LivroId { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataEstimada { get; set; }
        public DateTime? DataDevolucao { get; set; }

        [NotMapped]
        public LivroModel? Livro { get; set; }

        [NotMapped]
        public UsuarioModel? Usuario { get; set; }
    }
}
