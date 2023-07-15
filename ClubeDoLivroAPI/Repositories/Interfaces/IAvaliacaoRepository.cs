using ClubeDoLivroAPI.Models;

namespace ClubeDoLivroAPI.Repositories.Interfaces
{
    public interface IAvaliacaoRepository
    {
        Task<List<AvaliacaoModel>> GetAllAvaliacao();
        Task<List<AvaliacaoModel>> GetAvaliacoesByUser(int userId);
        Task<List<AvaliacaoModel>> GetAvaliacoesByLivros(int bookId);
        Task<AvaliacaoModel> GetById(int id);
        Task<AvaliacaoModel> Add(AvaliacaoModel avaliacao);
        Task<AvaliacaoModel> Update(AvaliacaoModel avaliacao, int id);
        Task<bool> Delete(int id);
    }
}
