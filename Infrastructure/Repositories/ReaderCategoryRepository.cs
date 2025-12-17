using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReaderCategoryRepository : IReaderCategoryRepository
    {
        private readonly LibraryDbContext _dbContext;

        public ReaderCategoryRepository( LibraryDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public async Task<ReaderCategory?> TryGet( int id )
        {
            return await _dbContext.ReaderCategories
                .FirstOrDefaultAsync( c => c.Id == id );
        }

        public async Task<IReadOnlyList<ReaderCategory>> GetReadOnlyList()
        {
            return await _dbContext.ReaderCategories
                .AsNoTracking()
                .ToListAsync();
        }

        public void Add( ReaderCategory category )
        {
            _dbContext.ReaderCategories.Add( category );
        }

        public void Delete( ReaderCategory category )
        {
            _dbContext.ReaderCategories.Remove( category );
        }
    }
}