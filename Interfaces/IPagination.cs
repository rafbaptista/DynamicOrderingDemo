namespace DynamicOrderingDemo.Interfaces;

public interface IPagination
{
    public int Page { get; set; }
    public int PageSize { get; set; }
}