using ClubeDoLivroAPI.Models;

namespace ClubeDoLivroAPI.Repositories.Interfaces
{
    public interface ILivroRepository
    {
        Task<List<LivroModel>> GetAllBooks();
        Task<List<LivroModel>> GetByWriter(int writerId);
        Task<LivroModel> GetById(int id);
        Task<LivroModel> Add(LivroModel book);
        Task<LivroModel> Update(LivroModel book, int id);
        Task<bool> Delete(int id);
    }
}
