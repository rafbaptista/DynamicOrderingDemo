using DynamicOrderingDemo.Entities;
using DynamicOrderingDemo.Models;
using DynamicOrderingDemo.Repositories;

namespace DynamicOrderingDemo.Services;

public class PersonService(PersonRepository personRepository)
{
    public async Task<PaginatedList<Person>> GetAllAsync(PersonRequest request) 
        => await personRepository.GetAllAsync(request);
}