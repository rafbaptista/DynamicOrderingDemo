using DynamicOrderingDemo.Entities;
using DynamicOrderingDemo.Repositories;

namespace DynamicOrderingDemo.Services;

public class PersonService(PersonRepository personRepository)
{
    public async Task<IEnumerable<Person>> GetAllAsync(string orderBy, bool orderAsc, int page, int pageSize) => await personRepository.GetAllAsync(orderBy, orderAsc, page, pageSize);
}