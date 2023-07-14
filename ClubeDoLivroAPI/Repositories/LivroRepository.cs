using ClubeDoLivroAPI.Data;
using ClubeDoLivroAPI.Models;
using ClubeDoLivroAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClubeDoLivroAPI.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly ClubeDoLivroDBContext _dbContext;

        public LivroRepository(ClubeDoLivroDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<LivroModel> Add(LivroModel book)
        {
            await _dbContext.Livros.AddAsync(book);
            await _dbContext.SaveChangesAsync();

            return book;
        }

        public async Task<bool> Delete(int id)
        {
            LivroModel target = await GetById(id);

            if (target == null)
                throw new Exception($"Livro com ID {id} não encontrado");

            _dbContext.Livros.Remove(target);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<LivroModel> Update(LivroModel book, int id)
        {
            LivroModel target = await GetById(id);

            if (target == null)
                throw new Exception($"Livro com ID {id} não encontrado");

            target.Nome = book.Nome;
            target.Descricao = book.Descricao;
            target.Paginas = book.Paginas;

            _dbContext.Livros.Update(target);
            await _dbContext.SaveChangesAsync();

            return target;
        }

        public async Task<List<LivroModel>> GetAllBooks()
        {
            return await _dbContext.Livros.ToListAsync();
        }

        public async Task<LivroModel> GetById(int id)
        {
            return await _dbContext.Livros.FirstOrDefaultAsync(X => X.Id == id);
        }

    }
}
