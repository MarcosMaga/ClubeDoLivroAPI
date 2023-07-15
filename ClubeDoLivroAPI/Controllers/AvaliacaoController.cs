using ClubeDoLivroAPI.Models;
using ClubeDoLivroAPI.Repositories;
using ClubeDoLivroAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClubeDoLivroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvaliacaoController : ControllerBase
    {
        private readonly IAvaliacaoRepository _avaliacaoRepository;

        public AvaliacaoController(IAvaliacaoRepository avaliacaoRepository)
        {
            _avaliacaoRepository = avaliacaoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<AvaliacaoModel>>> GetAllAssessment()
        {
            List<AvaliacaoModel> avaliacoes = await _avaliacaoRepository.GetAllAvaliacao();
            return Ok(avaliacoes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AvaliacaoModel>> GetAssessmentById(int id)
        {
            AvaliacaoModel avaliacao = await _avaliacaoRepository.GetById(id);

            if (avaliacao == null)
                return NotFound("Avaliação não encontrada");
            return Ok(avaliacao);
        }

        [HttpPost]
        public async Task<ActionResult<AvaliacaoModel>> Create([FromBody] AvaliacaoModel assessment)
        {
            if (assessment.Nota > 5 || assessment.Nota < 0)
                return BadRequest("Valores invalidos");
            AvaliacaoModel avaliacao = await _avaliacaoRepository.Add(assessment);
            return Ok(avaliacao);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAssessmentById(int id)
        {
            try
            {
                await _avaliacaoRepository.Delete(id);
                return Ok();
            } catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AvaliacaoModel>> UpdateAssessment([FromBody] AvaliacaoModel assessment, int id)
        {
            assessment.Id = id;

            if (assessment.Nota > 5 || assessment.Nota < 0)
                return BadRequest("Valores invalidos");

            try
            {
                AvaliacaoModel avaliacao = await _avaliacaoRepository.Update(assessment, id);
                return Ok(assessment);
            } catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
