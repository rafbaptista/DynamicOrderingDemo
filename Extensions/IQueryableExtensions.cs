using System.Linq.Expressions;
using System.Reflection;
using DynamicOrderingDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace DynamicOrderingDemo.Extensions
{
    public static class IQueryableExtensions
    {
        private static readonly MemoryCache _cache = new(new MemoryCacheOptions
        {
            SizeLimit = 1024
        });
        
        private static readonly MemoryCacheEntryOptions _cacheOptions = new()
        {
            Size = 1,
            SlidingExpiration = TimeSpan.FromMinutes(30)
        };

        public static IOrderedQueryable<TEntity> DynamicOrderBy<TEntity>(this IQueryable<TEntity> source, string propertyName,
            bool isAscending = true)
        {
            var entityType = typeof(TEntity);

            string cacheKey = $"{entityType.FullName}.{propertyName}";

            if (!_cache.TryGetValue(cacheKey, out LambdaExpression keySelector))
            {
                var property = entityType.GetProperties()
                    .FirstOrDefault(prop => string.Equals(prop.Name, propertyName, StringComparison.OrdinalIgnoreCase));

                if (property == null)
                    throw new ArgumentException(
                        $"Property '{propertyName}' does not exist on type '{entityType.Name}'.");

                var parameter = Expression.Parameter(entityType, "x");
                var propertyAccess = Expression.Property(parameter, property);

                if (property.PropertyType.IsValueType)
                {
                    var type = typeof(Func<,>).MakeGenericType(entityType, property.PropertyType);
                    keySelector = Expression.Lambda(type, propertyAccess, parameter);
                }
                else
                {
                    var type = typeof(Func<,>).MakeGenericType(entityType, typeof(object));
                    keySelector = Expression.Lambda(type, Expression.Convert(propertyAccess, typeof(object)),
                        parameter);
                }
                _cache.Set(cacheKey, keySelector, _cacheOptions);
            }

            var query = isAscending
                ? source.Provider.CreateQuery<TEntity>(
                    Expression.Call(
                        typeof(Queryable),
                        "OrderBy",
                        new Type[] { entityType, keySelector.ReturnType },
                        source.Expression,
                        Expression.Quote(keySelector)))
                : source.Provider.CreateQuery<TEntity>(
                    Expression.Call(
                        typeof(Queryable),
                        "OrderByDescending",
                        new Type[] { entityType, keySelector.ReturnType },
                        source.Expression,
                        Expression.Quote(keySelector)));

            return (IOrderedQueryable<TEntity>)query;
        }
        
        public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(
            this IQueryable<T> query,
            int page,
            int pageSize)
            where T : class
        {
            if (page < 1)
                page = PaginationConstants.DefaultPage;
            
            if (pageSize < 1)
                pageSize = PaginationConstants.DefaultPageSize;

            if (pageSize > PaginationConstants.MaxPageSize)
                pageSize = PaginationConstants.MaxPageSize;

            var totalRecords = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedList<T>
            {
                Items = items,
                TotalRecords = totalRecords,
                Page = page,
                PageSize = pageSize
            };
        }
        
        public static IQueryable<T> DynamicWhere<T>(this IQueryable<T> query, object filter)
        {
            Type type = filter.GetType();

            string cacheKey = type.FullName!;

            if (!_cache.TryGetValue(cacheKey, out PropertyInfo[] properties))
            {
                properties = type.GetProperties();
                _cache.Set(cacheKey, properties, _cacheOptions);
            }
            
            foreach (var prop in properties)
            {
                var value = prop.GetValue(filter);
                if (value == null) 
                    continue;

                var parameter = Expression.Parameter(typeof(T), "x");
                var property = Expression.Property(parameter, prop.Name);
                var constant = Expression.Constant(value);

                Expression comparison;
                if (property.Type == typeof(string))
                    comparison = Expression.Call(property, nameof(string.Contains), null, constant);
                else
                    comparison = Expression.Equal(property, constant);

                var lambda = Expression.Lambda<Func<T, bool>>(comparison, parameter);
                query = query.Where(lambda);
            }

            return query;
        }
    }
}