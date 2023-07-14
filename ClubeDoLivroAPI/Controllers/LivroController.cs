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
        private readonly IEscritorRepository _escritorRepository;

        public LivroController(ILivroRepository livroRepository, IEscritorRepository escritorRepository)
        {
            _livroRepository = livroRepository;
            _escritorRepository = escritorRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<LivroModel>>> GetAllBooks()
        {
            List<LivroModel> livros = await _livroRepository.GetAllBooks();

            foreach (var livro in livros)
            {
                livro.Escritor = await _escritorRepository.GetById(livro.EscritorId);
            }
            return Ok(livros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LivroModel>> GetBookById(int id)
        {
            LivroModel livro = await _livroRepository.GetById(id);
            livro.Escritor = await _escritorRepository.GetById(livro.EscritorId);

            if(livro == null)
                return NotFound("Livro não encontrado");
            return Ok(livro);
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
