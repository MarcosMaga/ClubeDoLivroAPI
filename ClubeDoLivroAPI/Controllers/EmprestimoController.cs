using ClubeDoLivroAPI.Models;
using ClubeDoLivroAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClubeDoLivroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmprestimoController : ControllerBase
    {
        private readonly IEmprestimoRepository _emprestimoRepository;

        public EmprestimoController(IEmprestimoRepository emprestimoRepository)
        {
            _emprestimoRepository = emprestimoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmprestimoModel>>> GetAllLending()
        {
            List<EmprestimoModel> emprestimos = await _emprestimoRepository.GetAllEmpretimo();
            return emprestimos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmprestimoModel>> GetLendingById(int id)
        {
            EmprestimoModel emprestimo = await _emprestimoRepository.GetById(id);

            if (emprestimo == null)
                return NotFound("Emprestimo não encontrado");
            return Ok(emprestimo);
        }

        [HttpPost]
        public async Task<ActionResult<EmprestimoModel>> Create([FromBody] EmprestimoModel lending)
        {
            try
            {
                lending.DataEstimada = lending.DataEmprestimo.AddDays(7);
                EmprestimoModel emprestimo = await _emprestimoRepository.Add(lending);
                return Ok(emprestimo);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteLendingById(int id)
        {
            try
            {
                await _emprestimoRepository.Delete(id);
                return Ok();
            }catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<EmprestimoModel>> SetDataDevolution([FromBody] DateTime datadevolucao, int id)
        {
            EmprestimoModel emprestimo = await _emprestimoRepository.GetById(id);
            emprestimo.DataDevolucao = datadevolucao;

            try
            {
                EmprestimoModel result = await _emprestimoRepository.Update(emprestimo, id);
                return Ok(result);
            } catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
