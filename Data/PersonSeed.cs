using DynamicOrderingDemo.Entities;

namespace DynamicOrderingDemo.Data;

public static class PersonSeed
{
    public static IEnumerable<Person> Create()
    {
        return new List<Person>
        {
            new Person
            {
                Id = 1,
                Name = "Alice Souza",
                Age = 28,
                BirthDate = new DateTime(1997, 3, 12),
                CreatedAt = DateTime.UtcNow.AddDays(-300),
                IsActive = true
            },
            new Person
            {
                Id = 2,
                Name = "Bruno Carvalho",
                Age = 35,
                BirthDate = new DateTime(1990, 7, 24),
                CreatedAt = DateTime.UtcNow.AddDays(-1200),
                IsActive = true
            },
            new Person
            {
                Id = 3,
                Name = "Carla Mendes", Age = 42,
                BirthDate = new DateTime(1983, 1, 9),
                CreatedAt = DateTime.UtcNow.AddDays(-2100),
                IsActive = false
            },
            new Person
            {
                Id = 4, 
                Name = "Diego Martins", 
                Age = 31, BirthDate = new DateTime(1994, 11, 3),
                CreatedAt = DateTime.UtcNow.AddDays(-800), 
                IsActive = true
            },
            new Person
            {
                Id = 5, 
                Name = "Eduarda Lima", 
                Age = 26, BirthDate = new DateTime(1999, 6, 17),
                CreatedAt = DateTime.UtcNow.AddDays(-150), 
                IsActive = true
            },
            new Person
            {
                Id = 6, 
                Name = "Felipe Andrade", 
                Age = 39, BirthDate = new DateTime(1986, 4, 2),
                CreatedAt = DateTime.UtcNow.AddDays(-1600), 
                IsActive = false
            },
            new Person
            {
                Id = 7, 
                Name = "Gabriela Nunes", 
                Age = 22, BirthDate = new DateTime(2003, 12, 29),
                CreatedAt = DateTime.UtcNow.AddDays(-90), 
                IsActive = true
            },
            new Person
            {
                Id = 8, 
                Name = "Henrique Alves", 
                Age = 48, BirthDate = new DateTime(1977, 2, 14),
                CreatedAt = DateTime.UtcNow.AddDays(-2500), 
                IsActive = false
            },
            new Person
            {
                Id = 9, 
                Name = "Isabela Rocha", 
                Age = 30, BirthDate = new DateTime(1995, 9, 8),
                CreatedAt = DateTime.UtcNow.AddDays(-600), 
                IsActive = true
            },
            new Person
            {
                Id = 10, 
                Name = "João Pereira", 
                Age = 33, BirthDate = new DateTime(1992, 8, 25),
                CreatedAt = DateTime.UtcNow.AddDays(-1000), 
                IsActive = true
            },
            new Person
            {
                Id = 11, 
                Name = "Karen Oliveira", Age = 29,
                BirthDate = new DateTime(1996, 5, 30),
                CreatedAt = DateTime.UtcNow.AddDays(-450), 
                IsActive = true
            },
            new Person
            {
                Id = 12, 
                Name = "Lucas Barbosa", Age = 41, 
                BirthDate = new DateTime(1984, 10, 19),
                CreatedAt = DateTime.UtcNow.AddDays(-1800),
                IsActive = false
            },
            new Person
            {
                Id = 13,
                Name = "Mariana Ribeiro",
                Age = 24, BirthDate = new DateTime(2001, 2, 6),
                CreatedAt = DateTime.UtcNow.AddDays(-200),
                IsActive = true
            },
            new Person
            {
                Id = 14, 
                Name = "Nicolas Costa", 
                Age = 37, BirthDate = new DateTime(1988, 7, 13),
                CreatedAt = DateTime.UtcNow.AddDays(-1300), 
                IsActive = false
            },
            new Person
            {
                Id = 15, 
                Name = "Otávia Fernandes", 
                Age = 45, BirthDate = new DateTime(1980, 11, 21),
                CreatedAt = DateTime.UtcNow.AddDays(-2300), 
                IsActive = true
            }
        };
    }
}