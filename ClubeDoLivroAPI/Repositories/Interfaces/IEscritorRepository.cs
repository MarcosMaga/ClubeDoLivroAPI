using ClubeDoLivroAPI.Models;

namespace ClubeDoLivroAPI.Repositories.Interfaces
{
    public interface IEscritorRepository
    {
        Task<List<EscritorModel>> GetAllEscritores();
        Task<EscritorModel> GetById(int id);
        Task<EscritorModel> Add(EscritorModel escritor);
        Task<EscritorModel> Update(EscritorModel escritor, int id);
        Task<bool> Delete(int id);
    }
}
