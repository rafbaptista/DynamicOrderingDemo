namespace DynamicOrderingDemo.Interfaces;

public interface IOrdering
{
    public string OrderBy { get; set; }
    public bool OrderAsc { get; set; }
}