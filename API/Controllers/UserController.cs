using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Infrastructure.Models;
using TaskManager;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TaskManagerContext _context;
        IMapper _mapper;

        public UserController(TaskManagerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<List<Core.Entidades.Usuario>> GetUsuarios()
        {
            var users = await _context.Usuarios.ToListAsync();
            var ret = _mapper.Map<List<Core.Entidades.Usuario>>(users);
            return ret;
        }
    }
}
