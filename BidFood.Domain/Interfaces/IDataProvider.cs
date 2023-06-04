namespace BidFood.Domain.Interfaces
{
    public interface IDataProvider<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        Task<int> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(int id, TEntity entity);
        Task DeleteAsync(int id);
    }
}
