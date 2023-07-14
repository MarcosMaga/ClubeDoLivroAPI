namespace ClubeDoLivroAPI.Models
{
    public class LivroModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public int? Paginas { get; set; }
        public int EscritorId { get; set; }
        public virtual EscritorModel? Escritor { get; set; }
    }
}
