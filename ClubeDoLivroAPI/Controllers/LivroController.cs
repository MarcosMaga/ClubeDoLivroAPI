using ClubeDoLivroAPI.Models;
using ClubeDoLivroAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClubeDoLivroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroRepository _livroRepository;
        private readonly IAvaliacaoRepository _avaliacaoRepository;

        public LivroController(ILivroRepository livroRepository, IAvaliacaoRepository avaliacaoRepository)
        {
            _livroRepository = livroRepository;
            _avaliacaoRepository = avaliacaoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<LivroModel>>> GetAllBooks()
        {
            List<LivroModel> livros = await _livroRepository.GetAllBooks();

            foreach(LivroModel livro in livros)
            {
                livro.UserNota = 0;
                List<AvaliacaoModel> avaliacoes = await _avaliacaoRepository.GetAvaliacoesByLivros(livro.Id);
                foreach (AvaliacaoModel avaliacao in avaliacoes)
                    livro.UserNota += avaliacao.Nota;
                livro.UserNota /= avaliacoes.Count();
            }
            return Ok(livros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LivroModel>> GetBookById(int id)
        {
            LivroModel livro = await _livroRepository.GetById(id);
            livro.UserNota = 0;
            List<AvaliacaoModel> avaliacoes = await _avaliacaoRepository.GetAvaliacoesByLivros(livro.Id);

            foreach(AvaliacaoModel avaliacao in avaliacoes)
                livro.UserNota += avaliacao.Nota;
            livro.UserNota /= avaliacoes.Count();

            if(livro == null)
                return NotFound("Livro não encontrado");
            return Ok(livro);
        }

        [HttpGet("Avaliacoes/{id}")]
        public async Task<ActionResult<List<AvaliacaoModel>>> GetAvaliacoesByBook(int id)
        {
            List<AvaliacaoModel> avaliacoes = await _avaliacaoRepository.GetAvaliacoesByLivros(id);
            return Ok(avaliacoes);
        }

        [HttpPost]
        public async Task<ActionResult<LivroModel>> Create([FromBody] LivroModel book)
        {
            LivroModel livro = await _livroRepository.Add(book);
            return Ok(livro);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBookById(int id)
        {
            try
            {
                await _livroRepository.Delete(id);
                return Ok();
            }catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioModel>> UpdateBook([FromBody] LivroModel book, int id)
        {
            book.Id = id;

            try
            {
                LivroModel livro = await _livroRepository.Update(book, id);
                return Ok(book);
            } catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
