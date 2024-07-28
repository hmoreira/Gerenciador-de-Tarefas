﻿using TaskManager.Core.Enums;
using TaskManager.Infrastructure.Models;

namespace TaskManager.Infrastructure.Repository.Interfaces
{
    public interface IUserRepository : IBaseRepository<Usuario>
    {        
        Task<Usuario?> ValidaUsuario(string username, string senha);        
        Task Create(string nome, string username, UserFuncaoEnum funcao, string senha);
    }
}
