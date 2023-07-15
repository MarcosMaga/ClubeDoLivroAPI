using ClubeDoLivroAPI.Models;
using ClubeDoLivroAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClubeDoLivroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EscritorController : ControllerBase
    {
        private readonly IEscritorRepository _escritorRepository;
        private readonly ILivroRepository _livroRepository;

        public EscritorController(IEscritorRepository escritorRepository, ILivroRepository livroRepository)
        {
            _escritorRepository = escritorRepository;
            _livroRepository = livroRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<EscritorModel>>> GetAllWriters()
        {
            List<EscritorModel> escritores = await _escritorRepository.GetAllEscritores();
            return Ok(escritores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EscritorModel>> GetWriterById(int id)
        {
            EscritorModel escritor = await _escritorRepository.GetById(id);

            if (escritor == null)
                return NotFound("Escritor não encontrado");
            return Ok(escritor);
        }

        [HttpGet("{id}/Livros")]
        public async Task<ActionResult<List<LivroModel>>> GetAllBooksByWriter(int id)
        {
            List<LivroModel> livros = await _livroRepository.GetByWriter(id);
            return Ok(livros);
        }

        [HttpPost]
        public async Task<ActionResult<EscritorModel>> Create([FromBody] EscritorModel writer)
        {
            EscritorModel escritor = await _escritorRepository.Add(writer);
            return Ok(escritor);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteWriterById(int id)
        {
            try
            {
                await _escritorRepository.Delete(id);
                return Ok();
            }catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EscritorModel>> UpdateWriter([FromBody] EscritorModel writer, int id)
        {
            writer.Id = id;

            try
            {
                EscritorModel escritor = await _escritorRepository.Update(writer, id);
                return Ok(writer);
            } catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
