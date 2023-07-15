using ClubeDoLivroAPI.Data;
using ClubeDoLivroAPI.Models;
using ClubeDoLivroAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ClubeDoLivroAPI.Repositories
{
    public class EmprestimoRepository : IEmprestimoRepository
    {
        private readonly ClubeDoLivroDBContext _dbContext;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILivroRepository _livroRepository;

        public EmprestimoRepository(ClubeDoLivroDBContext dbContext, IUsuarioRepository usuarioRepository, ILivroRepository livroRepository)
        {
            _dbContext = dbContext;
            _usuarioRepository = usuarioRepository;
            _livroRepository = livroRepository;
        }

        public async Task<EmprestimoModel> Add(EmprestimoModel emprestimo)
        {
            LivroModel livroTarget = await _livroRepository.GetById(emprestimo.LivroId);

            if (!await VerifyBookIsDisponible(emprestimo.LivroId, emprestimo.DataEmprestimo))
                throw new Exception("O livro já está emprestado.");

            await _dbContext.Emprestimos.AddAsync(emprestimo);
            await _dbContext.SaveChangesAsync();
            return emprestimo;
        }

        public async Task<bool> Delete(int id)
        {
            EmprestimoModel target = await GetById(id);

            if (target == null)
                throw new Exception($"Emprestimo com ID {id} não encontrado");

            _dbContext.Emprestimos.Remove(target);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<EmprestimoModel> Update(EmprestimoModel emprestimo, int id)
        {
            EmprestimoModel target = await GetById(id);

            if (target == null)
                throw new Exception($"Emprestimo com ID {id} não encontrado");

            target = emprestimo;

            _dbContext.Emprestimos.Update(target);
            await _dbContext.SaveChangesAsync();

            return target;
        }

        public async Task<List<EmprestimoModel>> GetAllEmpretimo()
        {
            List<EmprestimoModel> emprestimos = await _dbContext.Emprestimos.ToListAsync();

            foreach (EmprestimoModel emprestimo in emprestimos)
            {
                emprestimo.Livro = await _livroRepository.GetById(emprestimo.LivroId);
                emprestimo.Usuario = await _usuarioRepository.GetById(emprestimo.UsuarioId);
            }

            return emprestimos;
        }

        public async Task<EmprestimoModel> GetById(int id)
        {
            EmprestimoModel emprestimo = await _dbContext.Emprestimos.FirstOrDefaultAsync(x => x.Id == id);
            emprestimo.Livro = await _livroRepository.GetById(emprestimo.LivroId);
            emprestimo.Usuario = await _usuarioRepository.GetById(emprestimo.UsuarioId);
            return emprestimo;
        }

        public async Task<bool> VerifyBookIsDisponible(int id, DateTime date)
        {
            EmprestimoModel target = await _dbContext.Emprestimos.Where(x => x.LivroId == id).OrderByDescending(x => x.DataEmprestimo).FirstOrDefaultAsync();

            if (target == null || target.DataDevolucao != null)
                return true;
            return false;
        }

        public async Task<List<EmprestimoModel>> GetByUser(int userId)
        {
            List<EmprestimoModel> emprestimos = await _dbContext.Emprestimos.Where(x => x.UsuarioId == userId).ToListAsync();

            foreach (EmprestimoModel emprestimo in emprestimos)
            {
                emprestimo.Livro = await _livroRepository.GetById(emprestimo.LivroId);
                emprestimo.Usuario = await _usuarioRepository.GetById(emprestimo.UsuarioId);
            }

            return emprestimos;
        }
    }
}
