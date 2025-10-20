using DynamicOrderingDemo.Entities;
using DynamicOrderingDemo.Interfaces;

namespace DynamicOrderingDemo.Models;

public class PersonRequest : IOrdering, IPagination
{
    public PersonRequest(
        string? orderBy = null, 
        bool? orderAsc = null, 
        int? page = null, 
        int? pageSize = null, 
        string? name = null, 
        int? id = null, 
        int? age = null, 
        bool? isActive = null)
    {
        OrderBy = orderBy ?? nameof(Person.Id);
        OrderAsc = orderAsc ?? true;
        Page = page ?? PaginationConstants.DefaultPage;
        PageSize = pageSize ?? PaginationConstants.DefaultPageSize;
        Name = name ?? string.Empty;
        Id = id;
        Age = age;
        IsActive = isActive ?? true;
    }

    public string OrderBy { get; set; } 
    public bool OrderAsc { get; set; } 
    public int Page { get; set; } 
    public int PageSize { get; set; } 
    public string Name { get; set; } 
    public int? Id { get; set; }
    public int? Age { get; set; }
    public bool IsActive { get; set; } 
}