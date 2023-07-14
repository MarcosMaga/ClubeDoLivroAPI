using ClubeDoLivroAPI.Models;

namespace ClubeDoLivroAPI.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<List<UsuarioModel>> GetAllUsers();
        Task<UsuarioModel> GetById(int id);
        Task<UsuarioModel> Add(UsuarioModel user);
        Task<UsuarioModel> Update(UsuarioModel user, int id);
        Task<bool> Delete(int id);
    }
}
