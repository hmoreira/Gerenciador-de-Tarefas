using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TaskManager.Infrastructure.Repository.Interfaces;
using TaskManager.API.DTO;
using TaskManager.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using TaskManager.Core.Services.Tarefa;
using TaskManager.Core.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaRepository _tarefaRepository;
        private readonly IUsuarioRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private int UsuarioLogadoId { get; set; }

        public TarefaController(ITarefaRepository tarefaRepository, IMapper mapper,
                              IUsuarioRepository userRepository,
                              IConfiguration configuration)
        {
            _tarefaRepository = tarefaRepository;
            _userRepository = userRepository;   
            _mapper = mapper;
            _configuration = configuration;            
        }

        #region Endpoints
        // GET: api/<UserController>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var tarefa = await _tarefaRepository.GetByID(id);
            if (tarefa == null)
                return BadRequest("Tarefa nao encontrada");
            else
            {
                return Ok(_mapper.Map<TarefaPutGet>(_mapper.Map<Tarefa>(tarefa)));
            }
        }

        [HttpGet]        
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Infrastructure.Models.Tarefa> tarefas;
            var funcao = Enum.Parse<UserFuncaoEnum>(User.FindFirst("Funcao").Value);
            UsuarioLogadoId = int.Parse(User.FindFirst("Id").Value);

            if (funcao == UserFuncaoEnum.Admin) //ve todas as tarefas
                tarefas = await _tarefaRepository.GetAll();
            else //ve apenas as tarefas que usuario logado é responsavel ou criou
                tarefas = await _tarefaRepository.GetAllByUser(UsuarioLogadoId);

            return Ok(_mapper.Map<List<TarefaPutGet>>(_mapper.Map<List<Tarefa>>(tarefas)));            
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TarefaPost tarefa)
        {
            UsuarioLogadoId = int.Parse(User.FindFirst("Id").Value);
            tarefa.UsuarioCriadorId = UsuarioLogadoId;
            return await PostPut(tarefa);
        }

        // PUT api/<UserController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TarefaPutGet tarefaP)
        {
            try
            {
                UsuarioLogadoId = int.Parse(User.FindFirst("Id").Value);
                var ret = await ExecPutDelete(tarefaP);

                if (ret != "")
                    return BadRequest(ret); 
                else
                    return Ok();
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
                await ExecPutDelete(null, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region Metodos Auxiliares

        async Task<IActionResult> PostPut(TarefaBase tarefaP, bool isPut = false, Infrastructure.Models.Tarefa? tarefaOriginal = null)
        {
            var tarefaNovaAlterada = _mapper.Map<Tarefa>(tarefaP);
            var validations = Core.Utils.ModelValidator.ValidateModel(tarefaNovaAlterada, isPut);
            if (!validations.Any())
            {
                try
                {
                    var efTarefaNovaAlterada = _mapper.Map<Infrastructure.Models.Tarefa>(tarefaNovaAlterada);
                    if (!isPut)
                        await _tarefaRepository.Create(efTarefaNovaAlterada);                    
                    else
                        await _tarefaRepository.Edit(tarefaOriginal, efTarefaNovaAlterada);

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

        async Task<string> ExecPutDelete(TarefaPutGet? tarefaP = null, int? tarefaId = null)
        {
            Infrastructure.Models.Tarefa tarefaOriginal;

            if (tarefaP == null)
                tarefaOriginal = await _tarefaRepository.GetByID(tarefaId.Value);
            else
                tarefaOriginal = await _tarefaRepository.GetByID(tarefaP.Id);
            
            if (tarefaOriginal == null)
                return "Tarefa nao encontrada";

            var usuarioCriador = await _userRepository.GetByID(tarefaOriginal.UsuarioCriadorId);
            if (usuarioCriador == null)
                return "Usuario criador nao encontrado";

            var userFuncao = Enum.Parse<UserFuncaoEnum>(User.FindFirst("Funcao").Value);
            var isValid = ValidaPutDelete.Validacao(UsuarioLogadoId, userFuncao, usuarioCriador.Id);

            if (tarefaP != null)
            {
                if (!isValid)
                    return "Usuario regular so pode editar suas tarefas";
                else
                    await PostPut(tarefaP, true, tarefaOriginal);
            }
            else //delete
            {
                if (!isValid)
                    return "Usuario regular so pode deletar suas tarefas";
                else
                    await _tarefaRepository.Delete(tarefaId.Value);                
            }

            return "";
        }
        #endregion
    }
}
