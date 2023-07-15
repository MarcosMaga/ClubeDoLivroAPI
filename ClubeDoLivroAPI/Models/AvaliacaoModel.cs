namespace ClubeDoLivroAPI.Models
{
    public class AvaliacaoModel
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int LivroId { get; set; }
        public float Nota { get; set; }
        public string? Opiniao { get; set; }
        public virtual LivroModel? Livro { get; set; }
        public virtual UsuarioModel? Usuario { get; set; }
    }
}
