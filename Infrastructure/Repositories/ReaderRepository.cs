using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReaderRepository : IReaderRepository
    {
        private readonly LibraryDbContext _dbContext;

        public ReaderRepository( LibraryDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public async Task<Reader?> TryGet( int id )
        {
            return await _dbContext.Readers
                .Include( r => r.Category )
                .FirstOrDefaultAsync( r => r.Id == id );
        }

        public async Task<Reader?> TryGetByPhone( string phoneNumber )
        {
            return await _dbContext.Readers
                .AsNoTracking()
                .FirstOrDefaultAsync( r => r.PhoneNumber == phoneNumber );
        }

        public async Task<Reader?> TryGetWithRentails( int id )
        {
            return await _dbContext.Readers
                .Include( r => r.Rentals )
                .ThenInclude( r => r.Book )
                .FirstOrDefaultAsync( r => r.Id == id );
        }

        public async Task<IReadOnlyList<Reader>> GetReadOnlyList()
        {
            return await _dbContext.Readers
                .AsNoTracking()
                .ToListAsync();
        }

        public void Add( Reader reader )
        {
            _dbContext.Readers.Add( reader );
        }

        public void Delete( Reader reader )
        {
            _dbContext.Readers.Remove( reader );
        }
    }
}