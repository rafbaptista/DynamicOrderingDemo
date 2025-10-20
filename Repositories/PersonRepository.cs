using DynamicOrderingDemo.Data;
using DynamicOrderingDemo.Entities;
using DynamicOrderingDemo.Extensions;
using DynamicOrderingDemo.Models;

namespace DynamicOrderingDemo.Repositories;

public class PersonRepository(ApplicationContext context)
{
    public async Task<PaginatedList<Person>> GetAllAsync(PersonRequest request)
    {
        return await context
            .Set<Person>()
            .DynamicWhere(new
            {
                Id = request.Id, 
                Name = request.Name,
                Age = request.Age,
                IsActive = true,
            })
            .DynamicOrderBy(request.OrderBy, request.OrderAsc)
            .ToPaginatedListAsync(request.Page, request.PageSize);
    }
}