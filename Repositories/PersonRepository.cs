using DynamicOrderingDemo.Data;
using DynamicOrderingDemo.Entities;
using DynamicOrderingDemo.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DynamicOrderingDemo.Repositories;

public class PersonRepository(ApplicationContext context)
{
    public async Task<IEnumerable<Person>> GetAllAsync(
        string orderBy, 
        bool orderAsc, 
        int page, 
        int pageSize)
    {
        return await context
            .Set<Person>()
            .OrderBy(orderBy, orderAsc)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}