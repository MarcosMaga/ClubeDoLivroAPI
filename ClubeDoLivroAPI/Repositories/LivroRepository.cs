using ClubeDoLivroAPI.Data;
using ClubeDoLivroAPI.Models;
using ClubeDoLivroAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClubeDoLivroAPI.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly ClubeDoLivroDBContext _dbContext;
        private readonly IEscritorRepository _escritorRepository;

        public LivroRepository(ClubeDoLivroDBContext dbContext, IEscritorRepository escritorRepository)
        {
            _dbContext = dbContext;
            _escritorRepository = escritorRepository;
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
            List<LivroModel> livros = await _dbContext.Livros.ToListAsync();
   
            foreach(LivroModel livro in livros)
                livro.Escritor = await _escritorRepository.GetById(livro.EscritorId);
            return livros;
        }

        public async Task<List<LivroModel>> GetByWriter(int writerId)
        {
            List<LivroModel> livros = await _dbContext.Livros.Where(x => x.EscritorId == writerId).ToListAsync();

            foreach (LivroModel livro in livros)
                livro.Escritor = await _escritorRepository.GetById(livro.EscritorId);
            return livros;
        }

        public async Task<LivroModel> GetById(int id)
        {
            LivroModel livro = await _dbContext.Livros.FirstOrDefaultAsync(X => X.Id == id);
            livro.Escritor = await _escritorRepository.GetById(livro.EscritorId);
            return livro;
        }

    }
}
