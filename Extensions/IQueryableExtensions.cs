using System.Linq.Expressions;
using Microsoft.Extensions.Caching.Memory;

namespace DynamicOrderingDemo.Extensions
{
    public static class IQueryableExtensions
    {
        private static readonly MemoryCache _cache = new(new MemoryCacheOptions
        {
            SizeLimit = 1024
        });

        public static IOrderedQueryable<TEntity> OrderBy<TEntity, TKey>(this IQueryable<TEntity> source,
            Expression<Func<TEntity, TKey>> keySelector, bool isAscending)
        {
            var query = isAscending
                ? source.OrderBy(keySelector)
                : source.OrderByDescending(keySelector);

            return query;
        }

        public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string propertyName,
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

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    Size = 1,
                    SlidingExpiration = TimeSpan.FromMinutes(30)
                };

                _cache.Set(cacheKey, keySelector, cacheEntryOptions);
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
    }
}