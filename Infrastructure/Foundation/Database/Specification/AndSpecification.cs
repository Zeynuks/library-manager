using System.Linq.Expressions;

namespace Infrastructure.Foundation.Database.Specification
{
    public class AndSpecification<TEntity> : Specification<TEntity>
        where TEntity : class
    {
        public AndSpecification( Specification<TEntity> left, Specification<TEntity> right )
        {
            RegisterFilteringQuery( left, right );
        }

        private void RegisterFilteringQuery( Specification<TEntity> left, Specification<TEntity> right )
        {
            Expression<Func<TEntity, bool>>? leftExpression = left.Criteria;
            Expression<Func<TEntity, bool>>? rightExpression = right.Criteria;

            if ( leftExpression is null && rightExpression is null )
            {
                return;
            }

            if ( leftExpression is not null && rightExpression is null )
            {
                AddFiltering( leftExpression );
                return;
            }

            if ( leftExpression is null && rightExpression is not null )
            {
                AddFiltering( rightExpression );
                return;
            }

            ReplaceExpressionVisitor replaceVisitor = new(
                rightExpression!.Parameters.Single(),
                leftExpression!.Parameters.Single()
            );

            Expression replacedBody = replaceVisitor.Visit( rightExpression.Body );

            BinaryExpression? andExpression = Expression.AndAlso(
                leftExpression.Body,
                replacedBody
            );

            Expression<Func<TEntity, bool>>? lambda = Expression.Lambda<Func<TEntity, bool>>(
                andExpression,
                leftExpression.Parameters.Single()
            );

            AddFiltering( lambda );
        }
    }
}