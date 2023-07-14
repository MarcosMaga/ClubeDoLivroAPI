using ClubeDoLivroAPI.Data;
using ClubeDoLivroAPI.Models;
using ClubeDoLivroAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClubeDoLivroAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ClubeDoLivroDBContext _dbContext;

        public UsuarioRepository(ClubeDoLivroDBContext context) 
        {
            _dbContext = context;
        }

        public async Task<UsuarioModel> Add(UsuarioModel user)
        {
            await _dbContext.Usuarios.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<bool> Delete(int id)
        {
            UsuarioModel target = await GetById(id);

            if (target == null)
                throw new Exception($"Usuário com ID {id} não encontrado");

            _dbContext.Usuarios.Remove(target);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<UsuarioModel> Update(UsuarioModel user, int id)
        {
            UsuarioModel target = await GetById(id);

            if (target == null)
                throw new Exception($"Usuário com ID {id} não encontrado");

            target.Email = user.Email;
            target.Nome = user.Nome;

            _dbContext.Usuarios.Update(target);
            await _dbContext.SaveChangesAsync();

            return target;
        }

        public async Task<List<UsuarioModel>> GetAllUsers()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }

        public async Task<UsuarioModel> GetById(int id)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
