using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Enums;
using TaskManager.Infrastructure.Models;
using TaskManager.Infrastructure.Repository.Interfaces;

namespace TaskManager.Infrastructure.Repository
{
    public class EFUserRepository : IUserRepository
    {
        private readonly SQLServerContext _context;
        public EFUserRepository(SQLServerContext context)
        {            
            _context = context;
        }        

        public async Task Create(string nome, string username, UserFuncaoEnum funcao, string senha)
        {
            try
            {
                var sql = string.Format("INSERT INTO Usuario (Nome, Funcao, Username, Senha) VALUES " +
                                    "('{0}', {1}, '{2}', HASHBYTES('SHA2_256', '{3}'))", nome,
                                    (short)funcao, username, senha);
                await _context.Database.ExecuteSqlRawAsync(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task Delete(int id)
        {
            var user = await _context.Usuarios.FindAsync(id);
            if (user != null)
            {
                _context.Usuarios.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Edit(Usuario usuario)
        {
            var ret = await _context.Usuarios.FindAsync(usuario.Id);
            if (ret != null)
            {
                ret.Funcao = usuario.Funcao;
                ret.Nome = usuario.Nome;

                try
                {
                    _context.Update(ret);
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    throw;
                }

            }
            else
                throw new Exception("Usuario nao encontrado");
        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {            
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario?> GetByID(int id)
        {
            return await _context.Usuarios.FindAsync(id);            
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<Usuario?> ValidaUsuario(string username, string senha)
        {
            var sql = string.Format("SELECT * FROM Usuario " +
                                    "WHERE Username = '{0}' AND Senha = HASHBYTES('SHA2_256', '{1}')",
                                    username, senha);
            return await _context.Usuarios.FromSqlRaw(sql).FirstOrDefaultAsync();                       
        }
    }
}
