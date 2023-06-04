using BidFood.Domain.Interfaces;
using BidFood.Domain.Models;
using JsonFlatFileDataStore;

namespace BidFood.Data.Json.Repository
{
    public class PersonsRepository : IDataProvider<Person>
    {
        private readonly string _collectionName = "people";
        private readonly IDocumentCollection<Person> _collection;
        public PersonsRepository(IDataStore dataStore)
        {
        _collection = dataStore.GetCollection<Person>(_collectionName);
        }

        public IEnumerable<Person> GetAll()
        {
            return _collection.AsQueryable();
        }

        public Person GetById(int id)
        {
            return _collection.AsQueryable().FirstOrDefault(x => x.Id == id);
        }

        public async Task<int> CreateAsync(Person entity)
        {
            try
            {
                int nextId = _collection.GetNextIdValue(); //initial index is zero
                entity.Id = ++nextId;
                await _collection.InsertOneAsync(entity);
                return nextId;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to create a new record, error: {ex.Message}", ex);
            }
        }

        public IEnumerable<Person> GetByName(string firstName, string lastName)
        {
            var result = _collection.AsQueryable()
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
