using Microsoft.EntityFrameworkCore;
using Serialize.Linq.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
#nullable disable

namespace Mercury.Shared.Repository
{
    public class LinqQueryFilter<TEntity> where TEntity : class
    {
        public LinqQueryFilter() { }

        public LinqQueryFilter(Expression<Func<TEntity, bool>> expression)
        {
            var serializer = new ExpressionSerializer(new Serialize.Linq.Serializers.JsonSerializer());
            LinqExpression = serializer.SerializeText(expression);
        }

        public LinqQueryFilter(Expression<Func<TEntity, bool>> expression, List<string> includeProperties)
        {
            var serializer = new ExpressionSerializer(new Serialize.Linq.Serializers.JsonSerializer());
            LinqExpression = serializer.SerializeText(expression);

            IncludePropertyNames = includeProperties;
        }
        /// <summary>
        /// Gets or sets the linq expression for filtering.
        /// </summary>
        public string LinqExpression { get; set; }

        /// <summary>
        /// If you want to return a subset of the properties, you can specify only
        /// the properties that you want to retrieve in the SELECT clause.
        /// Leave empty to return all columns
        /// </summary>
        public List<string> IncludePropertyNames { get; set; } = new List<string>();

        /// <summary>
        /// Specify the property to ORDER BY, if any 
        /// </summary>
        public string OrderByPropertyName { get; set; } = "";

        /// <summary>
        /// Set to true if you want to order DESCENDING
        /// </summary>
        public bool OrderByDescending { get; set; } = false;

        public IEnumerable<TEntity> GetFilteredList(IQueryable<TEntity> query)
        {
            var serializer = new ExpressionSerializer(new Serialize.Linq.Serializers.JsonSerializer());
            var deserializedExpression = serializer.DeserializeText(LinqExpression) as Expression<Func<TEntity, bool>>;

            query.Where(deserializedExpression);

            // Include the specified properties
            foreach (var includeProperty in IncludePropertyNames)
            {
                query = query.Include(includeProperty);
            }

            // order by
            if (OrderByPropertyName != "")
            {
                PropertyInfo prop = typeof(TEntity).GetProperty(OrderByPropertyName);
                if (prop != null)
                {
                    if (OrderByDescending)
                        query = query.OrderByDescending(x => prop.GetValue(x, null));
                    else
                        query = query.OrderBy(x => prop.GetValue(x, null));
                }
            }

            // execute and return the list
            return query.ToList();

        }
    }
}
