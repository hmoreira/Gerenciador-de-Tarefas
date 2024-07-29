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
            _context.Tarefas.Remove(task);
            await _context.SaveChangesAsync();            
        }

        public async Task Edit(Tarefa tarefaOriginal, Tarefa tarefaAlterada)
        {
            tarefaOriginal.Titulo = tarefaAlterada.Titulo;
            tarefaOriginal.Descricao = tarefaAlterada.Descricao;
            tarefaOriginal.Status = tarefaAlterada.Status;
            tarefaOriginal.DataVencimento = tarefaAlterada.DataVencimento;
            tarefaOriginal.UsuarioResponsavelId = tarefaAlterada.UsuarioResponsavelId;

            try
            {
                _context.Update(tarefaOriginal);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }        

        public async Task<IEnumerable<Tarefa>> GetAll()
        {            
            return await _context.Tarefas.ToListAsync();
        }

        public async Task<IEnumerable<Tarefa>> GetAllByUser(int userId)
        {
            return await _context.Tarefas
                .Where(w => w.UsuarioResponsavelId == userId || w.UsuarioCriadorId == userId)
                .ToListAsync();
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
