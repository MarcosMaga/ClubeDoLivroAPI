using ClubeDoLivroAPI.Models;

namespace ClubeDoLivroAPI.Repositories.Interfaces
{
    public interface IEmprestimoRepository
    {
        Task<List<EmprestimoModel>> GetAllEmpretimo();
        Task<List<EmprestimoModel>> GetByUser(int userId);
        Task<EmprestimoModel> GetById(int id);
        Task<EmprestimoModel> Add(EmprestimoModel emprestimo);
        Task<EmprestimoModel> Update(EmprestimoModel emprestimo, int id);
        Task<bool> Delete(int id);
        Task<bool> VerifyBookIsDisponible(int id, DateTime date);
    }
}
