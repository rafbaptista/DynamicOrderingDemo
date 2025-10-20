namespace DynamicOrderingDemo.Models;

public class PaginatedList<T>
{
    public List<T> Items { get; set; } = Enumerable.Empty<T>().ToList();
    public int TotalRecords { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
}