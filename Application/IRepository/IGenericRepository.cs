
namespace Application.IRepository
{
    public interface IGenericRepository<TEntity>
    {
        Task<List<TEntity>> GetAll();
        Task<TEntity?> GetByID(int id);
        Task Add(TEntity entity);
        void Update(TEntity entity);
        void HardDelete(TEntity entity);
        
    }
}
