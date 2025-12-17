using Infrastructure.Foundation.Database.Specification;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Database
{
    public class SpecificationEvaluator<TEntity> : Specification<TEntity>
        where TEntity : class
    {
        public SpecificationEvaluator( Specification<TEntity> specification )
            : base( specification )
        {
        }

        public virtual IQueryable<TEntity> Apply( IQueryable<TEntity> queryable )
        {
            if ( Criteria is not null )
            {
                queryable = queryable.Where( Criteria );
            }

            if ( IncludeExpressions?.Count > 0 )
            {
                queryable = IncludeExpressions.Aggregate( queryable,
                    ( current, includeQuery ) => current.Include( includeQuery ) );
            }

            if ( OrderByExpressions?.Count > 0 )
            {
                IOrderedQueryable<TEntity> orderedQueryable = queryable.OrderBy( OrderByExpressions.First() );

                orderedQueryable = OrderByExpressions.Skip( 1 )
                    .Aggregate( orderedQueryable, ( current, orderQuery ) => current.ThenBy( orderQuery ) );

                queryable = orderedQueryable;
            }

            if ( OrderByDescendingExpressions?.Count > 0 )
            {
                IOrderedQueryable<TEntity> orderedQueryable =
                    queryable.OrderByDescending( OrderByDescendingExpressions.First() );

                orderedQueryable = OrderByDescendingExpressions.Skip( 1 )
                    .Aggregate( orderedQueryable, ( current, orderQuery ) => current.ThenByDescending( orderQuery ) );

                queryable = orderedQueryable;
            }

            if ( Skip is not null )
            {
                queryable = queryable.Skip( Skip.Value );
            }

            if ( Take is not null )
            {
                queryable = queryable.Take( Take.Value );
            }

            return queryable;
        }
    }
}