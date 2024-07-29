using TaskManager.Infrastructure.Models;

namespace TaskManager.Infrastructure.Repository.Interfaces
{
    public interface ITarefaRepository : IBaseRepository<Tarefa>
    {
        Task Create(Tarefa entity);
        Task Edit(Tarefa tarefaOriginal, Tarefa alterada);
        Task<IEnumerable<Tarefa>> GetAllByUser(int userId);
    }
    
    
}
