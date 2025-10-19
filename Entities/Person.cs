namespace DynamicOrderingDemo.Entities;

public class Person
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public int Age { get; set; }
    public DateTime BirthDate { get; init; }
    public DateTime CreatedAt { get; init; }
    public bool IsActive { get; set; }
}