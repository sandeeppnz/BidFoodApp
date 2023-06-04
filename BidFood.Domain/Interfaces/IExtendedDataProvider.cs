namespace BidFood.Domain.Interfaces;

public partial interface IDataProvider<TEntity>
{
    TEntity GetByName(string firstName, string lastName);
}
