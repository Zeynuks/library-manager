using System.Linq.Expressions;

namespace Infrastructure.Foundation.Database.Specification
{
    public abstract class Specification<TEntity> where TEntity : class
    {
        private List<Expression<Func<TEntity, object>>>? _includeExpressions;
        private List<Expression<Func<TEntity, object>>>? _orderByExpressions;
        private List<Expression<Func<TEntity, object>>>? _orderByDescendingExpressions;

        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }
        public IReadOnlyCollection<Expression<Func<TEntity, object>>>? IncludeExpressions => _includeExpressions;
        public IReadOnlyCollection<Expression<Func<TEntity, object>>>? OrderByExpressions => _orderByExpressions;

        public IReadOnlyCollection<Expression<Func<TEntity, object>>>? OrderByDescendingExpressions =>
            _orderByDescendingExpressions;

        public int? Skip { get; private set; }
        public int? Take { get; private set; }

        protected Specification()
        {
            AddFiltering( r => true );
        }

        protected Specification( Expression<Func<TEntity, bool>> query )
        {
            AddFiltering( r => true );
            Criteria = query;
        }

        protected Specification( Specification<TEntity> specification )
        {
            AddFiltering( r => true );

            Criteria = specification.Criteria;
            Skip = specification.Skip;
            Take = specification.Take;

            _includeExpressions = specification.IncludeExpressions?.ToList();
            _orderByExpressions = specification.OrderByExpressions?.ToList();
            _orderByDescendingExpressions = specification.OrderByDescendingExpressions?.ToList();
        }

        public void And( Specification<TEntity> specification )
        {
            AndSpecification<TEntity> combined = new( this, specification );
            Criteria = combined.Criteria;
            Skip = combined.Skip;
            Take = combined.Take;
        }

        protected void AddFiltering( Expression<Func<TEntity, bool>> query )
        {
            Criteria = query;
        }

        protected void AddInclude( Expression<Func<TEntity, object>> query )
        {
            _includeExpressions ??= [ ];
            _includeExpressions.Add( query );
        }

        protected void AddOrderBy( Expression<Func<TEntity, object>> query )
        {
            _orderByExpressions ??= [ ];
            _orderByExpressions.Add( query );
        }

        protected void AddOrderByDescending( Expression<Func<TEntity, object>> query )
        {
            _orderByDescendingExpressions ??= [ ];
            _orderByDescendingExpressions.Add( query );
        }

        protected void ApplyPaging( int skip, int take )
        {
            Skip = skip >= 0 ? skip : 0;
            Take = take > 0 ? take : null;
        }
    }
}