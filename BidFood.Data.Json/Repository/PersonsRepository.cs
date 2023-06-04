using BidFood.Domain.Interfaces;
using BidFood.Domain.Models;
using JsonFlatFileDataStore;

namespace BidFood.Data.Json.Repository
{
    public class PersonsRepository : IDataProvider<Person>
    {
        private readonly string _collectionName = "people";
        private readonly IDataStore _store;
        public PersonsRepository(IDataStore dataStore)
        {
            _store = dataStore;

        }
        private IDocumentCollection<Person> GetCollection()
        {
            return _store.GetCollection<Person>(_collectionName);
        }

        public IEnumerable<Person> GetAll()
        {
            return GetCollection().AsQueryable();
        }

        public Person GetById(int id)
        {
            return GetCollection().AsQueryable().FirstOrDefault(x => x.Id == id);
        }

        public async Task<int> CreateAsync(Person entity)
        {
            try
            {
                int nextId = GetCollection().GetNextIdValue();
                await GetCollection().InsertOneAsync(entity);
                return nextId;
            }
            catch (Exception ex)
            {

                throw new Exception($"Unable to create a new record, error: {ex.Message}");
            }
        }

        public IEnumerable<Person> GetByName(string firstName, string lastName)
        {
            var result = GetCollection().AsQueryable()
                .Where(x => x.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                            x.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase)
                );
            return result;
        }

        public Task<Person> UpdateAsync(int id, Person entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
