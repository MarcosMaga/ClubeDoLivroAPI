using ClubeDoLivroAPI.Data;
using ClubeDoLivroAPI.Models;
using ClubeDoLivroAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ClubeDoLivroAPI.Repositories
{
    public class AvaliacaoRepository : IAvaliacaoRepository
    {
        private readonly ClubeDoLivroDBContext _dbContext;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILivroRepository _livroRepository;

        public AvaliacaoRepository(ClubeDoLivroDBContext dbContext, IUsuarioRepository usuarioRepository, ILivroRepository livroRepository)
        {
            _dbContext = dbContext;
            _usuarioRepository = usuarioRepository;
            _livroRepository = livroRepository;
        }

        public async Task<AvaliacaoModel> Add(AvaliacaoModel avaliacao)
        {
            await _dbContext.Avaliacoes.AddAsync(avaliacao);
            await _dbContext.SaveChangesAsync();

            return avaliacao;
        }

        public async Task<bool> Delete(int id)
        {
            AvaliacaoModel target = await GetById(id);

            if (target == null)
                throw new Exception($"Avaliação com ID {id} não encontrado");

            _dbContext.Avaliacoes.Remove(target);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<AvaliacaoModel> Update(AvaliacaoModel avaliacao, int id)
        {
            AvaliacaoModel target = await GetById(id);

            if (target == null)
                throw new Exception($"Avaliação com ID {id} não encontrado");

            target.Nota = avaliacao.Nota;
            target.Opiniao = avaliacao.Opiniao;

            _dbContext.Avaliacoes.Update(target);
            await _dbContext.SaveChangesAsync();

            return target;
        }

        public async Task<List<AvaliacaoModel>> GetAllAvaliacao()
        {
            List<AvaliacaoModel> avaliacoes = await _dbContext.Avaliacoes.ToListAsync();

            foreach(AvaliacaoModel avaliacao in avaliacoes)
            {
                avaliacao.Livro = await _livroRepository.GetById(avaliacao.LivroId);
                avaliacao.Usuario = await _usuarioRepository.GetById(avaliacao.UsuarioId);
            }

            return avaliacoes;
        }

        public async Task<AvaliacaoModel> GetById(int id)
        {
            AvaliacaoModel avaliacao = await _dbContext.Avaliacoes.FirstOrDefaultAsync(x => x.Id == id);
            avaliacao.Livro = await _livroRepository.GetById(avaliacao.LivroId);
            avaliacao.Usuario = await _usuarioRepository.GetById(avaliacao.UsuarioId);
            return avaliacao;
        }

        public async Task<List<AvaliacaoModel>> GetAvaliacoesByUser(int userId)
        {
            List<AvaliacaoModel> avaliacoes = await _dbContext.Avaliacoes.Where(x => x.UsuarioId == userId).ToListAsync();

            foreach (AvaliacaoModel avaliacao in avaliacoes)
            {
                avaliacao.Livro = await _livroRepository.GetById(avaliacao.LivroId);
                avaliacao.Usuario = await _usuarioRepository.GetById(avaliacao.UsuarioId);
            }

            return avaliacoes;
        }

        public async Task<List<AvaliacaoModel>> GetAvaliacoesByLivros(int bookId)
        {
            List<AvaliacaoModel> avaliacoes = await _dbContext.Avaliacoes.Where(x => x.LivroId == bookId).ToListAsync();

            foreach (AvaliacaoModel avaliacao in avaliacoes)
            {
                avaliacao.Livro = await _livroRepository.GetById(avaliacao.LivroId);
                avaliacao.Usuario = await _usuarioRepository.GetById(avaliacao.UsuarioId);
            }

            return avaliacoes;
        }
    }
}
