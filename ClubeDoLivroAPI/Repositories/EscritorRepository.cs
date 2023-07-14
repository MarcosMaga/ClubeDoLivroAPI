using ClubeDoLivroAPI.Data;
using ClubeDoLivroAPI.Models;
using ClubeDoLivroAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClubeDoLivroAPI.Repositories
{
    public class EscritorRepository : IEscritorRepository
    {
        private readonly ClubeDoLivroDBContext _dbContext;

        public EscritorRepository(ClubeDoLivroDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<EscritorModel> Add(EscritorModel escritor)
        {
            await _dbContext.Escritores.AddAsync(escritor);
            await _dbContext.SaveChangesAsync();

            return escritor;
        }

        public async Task<bool> Delete(int id)
        {
            EscritorModel target = await GetById(id);

            if (target == null)
                throw new Exception($"Escritor com ID {id} não encontrado");

            _dbContext.Escritores.Remove(target);
            await _dbContext.SaveChangesAsync();

            return true;
        }
  
        public async Task<EscritorModel> Update(EscritorModel escritor, int id)
        {
            EscritorModel target = await GetById(id);

            if (target == null)
                throw new Exception($"Escritor com ID {id} não encontrado");

            target.Nome = escritor.Nome;
            target.Ano = escritor.Ano;

            _dbContext.Escritores.Update(target);
            await _dbContext.SaveChangesAsync();

            return target;
        }

        public async Task<List<EscritorModel>> GetAllEscritores()
        {
            return await _dbContext.Escritores.ToListAsync();
        }

        public async Task<EscritorModel> GetById(int id)
        {
            return await _dbContext.Escritores.FirstOrDefaultAsync(X => X.Id == id);
        }
    }
}
