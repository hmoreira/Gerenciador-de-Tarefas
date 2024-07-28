using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TaskManager.Infrastructure.Repository.Interfaces;
using TaskManager.API.DTO;
using TaskManager.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using TaskManager.API.Authentication;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {        
        private readonly ITarefaRepository _tarefaRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public TaskController(ITarefaRepository tarefaRepository, IMapper mapper,
                              IConfiguration configuration)
        {
            _tarefaRepository = tarefaRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        #region Endpoints
        // GET: api/<UserController>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _tarefaRepository.GetByID(id);
            if (user == null)
                return BadRequest("Usuario nao encontrado");
            else
            {
                return Ok(_mapper.Map<UserPutGet>(_mapper.Map<Usuario>(user)));
            }
        }        

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Tarefa tarefa)
        {
            return await PostPut(tarefa);
        }

        // PUT api/<UserController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Usuario usuario)
        {
            try
            {
                //await _userRepository.
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _tarefaRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }        
        
        #endregion

        #region Metodos Auxiliares

        async Task<IActionResult> PostPut(Tarefa tarefa, bool isPut = false)
        {
            var validations = Core.Utils.ModelValidator.ValidateModel(tarefa, isPut);
            if (!validations.Any())
            {
                try
                {
                    if (!isPut)
                        await _tarefaRepository.Create(_mapper.Map<Infrastructure.Models.Tarefa>(tarefa));
                    else
                        await _tarefaRepository.Edit(_mapper.Map<Infrastructure.Models.Tarefa>(tarefa));

                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            else
                return BadRequest(validations);
        }

        #endregion
    }
}
