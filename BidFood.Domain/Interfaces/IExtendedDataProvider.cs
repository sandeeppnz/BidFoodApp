namespace BidFood.Domain.Interfaces;

public partial interface IDataProvider<TEntity>
{
    IEnumerable<TEntity> GetByName(string firstName, string lastName);
}
