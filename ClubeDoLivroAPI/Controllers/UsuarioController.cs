using ClubeDoLivroAPI.Models;
using ClubeDoLivroAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClubeDoLivroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        private readonly IEmprestimoRepository _emprestimoRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository, IAvaliacaoRepository avaliacaoRepository, IEmprestimoRepository emprestimoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _avaliacaoRepository = avaliacaoRepository;
            _emprestimoRepository = emprestimoRepository;
        }


        [HttpGet]
        public async Task<ActionResult<List<UsuarioModel>>> GetAllUsers()
        {
            List<UsuarioModel> usuarios = await _usuarioRepository.GetAllUsers();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioModel>> GetUserById(int id)
        {
            UsuarioModel usuario = await _usuarioRepository.GetById(id);

            if (usuario == null)
                return NotFound("Usuário não encontrado");
            return Ok(usuario);
        }

        [HttpGet("{id}/Avaliacoes")]
        public async Task<ActionResult<List<AvaliacaoModel>>> GetAssessmentByUser(int id) 
        {
            List<AvaliacaoModel> avaliacoes = await _avaliacaoRepository.GetAvaliacoesByUser(id);
            return Ok(avaliacoes);
        }

        [HttpGet("{id}/Emprestimos")]
        public async Task<ActionResult<List<EmprestimoModel>>> GetLendingByUser(int id)
        {
            List<EmprestimoModel> emprestimos = await _emprestimoRepository.GetByUser(id);
            return Ok(emprestimos);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> Create([FromBody] UsuarioModel usuario)
        {
            UsuarioModel user = await _usuarioRepository.Add(usuario);
            return Ok(user);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteUserById(int id)
        {
            try
            {
                await _usuarioRepository.Delete(id);
                return Ok();
            }catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioModel>> UpdateUser([FromBody] UsuarioModel usuario, int id)
        {
            usuario.Id = id;

            try
            {
                UsuarioModel user = await _usuarioRepository.Update(usuario, id);
                return Ok(user);
            } catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
