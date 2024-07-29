namespace TaskManager.Infrastructure.Repository.Interfaces
{
    public interface IBaseRepository<T>
    {        
        Task Delete(int id);        
        Task<T?> GetByID(int id);
        Task<IEnumerable<T>> GetAll();
        Task<int> SaveChanges();        
    }
    
    
}
