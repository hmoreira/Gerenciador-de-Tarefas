using Microsoft.EntityFrameworkCore;
using TaskManager.Infrastructure.Models;
using TaskManager.Infrastructure.Repository.Interfaces;

namespace TaskManager.Infrastructure.Repository
{
    public class EFTarefaRepository : ITarefaRepository
    {
        private readonly SQLServerContext _context;               
        public EFTarefaRepository(SQLServerContext context)
        {            
            _context = context;            
        }        

        public async Task Create(Tarefa tarefa)
        {
            try
            {
                _context.Tarefas.Add(tarefa);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task Delete(int id)
        {
            var task = await _context.Tarefas.FindAsync(id);
            if (task != null)
            {
                _context.Tarefas.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Edit(Tarefa task)
        {
            var ret = await _context.Tarefas.FindAsync(task.Id);
            if (ret != null)
            {
                ret.Titulo = task.Titulo;
                ret.Descricao = task.Descricao;
                ret.Status = task.Status;
                ret.DataVencimento = task.DataVencimento;
                ret.UsuarioResponsavelId = task.UsuarioResponsavelId;

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

        public async Task<IEnumerable<Tarefa>> GetAll()
        {            
            return await _context.Tarefas.ToListAsync();
        }

        public async Task<Tarefa?> GetByID(int id)
        {
            return await _context.Tarefas.FindAsync(id);            
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
