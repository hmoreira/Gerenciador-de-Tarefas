using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TaskManager.Infrastructure.Repository.Interfaces;
using TaskManager.API.DTO;
using TaskManager.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using TaskManager.API.Authentication;
using TaskManager.Core.Services.Usuario;
using TaskManager.Core.Enums;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {        
        private readonly IUsuarioRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UsuarioController(IUsuarioRepository userRepository, IMapper mapper,
                              IConfiguration configuration)
        {            
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
            var user = await _userRepository.GetByID(id);
            if (user == null)
                return BadRequest("Usuario nao encontrado");
            else
            {
                return Ok(_mapper.Map<UsuarioPutGet>(_mapper.Map<Usuario>(user)));
            }
        }

        [HttpGet]        
        public async Task<IActionResult> GetAll()
        {
            if (!IsAdmin())
                return BadRequest("Usuario precisa ser admin");

            var users = await _userRepository.GetAll();
            if (!users.Any())
                return BadRequest("Nenhum usuario cadastrado");
            else
                return Ok(_mapper.Map<List<UsuarioPutGet>>(_mapper.Map<List<Usuario>>(users)));            
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Usuario usuario)
        {
            if (!IsAdmin())
                return BadRequest("Usuario precisa ser admin pra criação");
            else
                return await PostPut(usuario);
        }

        // PUT api/<UserController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UsuarioPutGet usuario)
        {
            if (!IsAdmin())
                return BadRequest("Usuario precisa ser admin pra edição");
            else
                return await PostPut(_mapper.Map<Usuario>(usuario), true);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!IsAdmin())
                return BadRequest("Usuario precisa ser admin pra remoção");

            try
            {
                await _userRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]UsuarioLogin login)
        {
            var ret = await _userRepository.ValidaUsuario(login.Username, login.Senha);
            if (ret == null)
                return BadRequest("Usuario nao encontrado");
            else
            {
                var user = _mapper.Map<UsuarioPutGet>(_mapper.Map<Usuario>(ret));
                var authSection = _configuration.GetSection("Authentication");

                var authorizationData = new AuthenticationData(
                    authSection.GetValue<string>("SecretKey"),
                    authSection.GetValue<string>("Issuer"),
                    authSection.GetValue<string>("Audience"),
                    authSection.GetValue<int>("HoursExpiration"));
                
                return Ok(TokenGenerator.GenerateJwt(user, authorizationData));
            }
        }
        #endregion

        #region Metodos Auxiliares

        async Task<IActionResult> PostPut(Usuario usuario, bool isPut = false)
        {
            var validations = Core.Utils.ModelValidator.ValidateModel(usuario, isPut);
            if (!validations.Any())
            {
                try
                {
                    if (!isPut)
                        await _userRepository.Create(usuario.Nome, usuario.Username, usuario.Funcao, usuario.Senha);                                            
                    else
                        await _userRepository.Edit(new Infrastructure.Models.Usuario
                        {
                            Funcao = (short)usuario.Funcao,
                            Id = usuario.Id,
                            Nome = usuario.Nome
                        });

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

        bool IsAdmin()
        {
            var funcao = Enum.Parse<UserFuncaoEnum>(User.FindFirst("Funcao").Value);
            if (ValidaFuncao.IsAdmin(funcao))
                return true;
            else
                return false;
        }
        #endregion


    }
}
