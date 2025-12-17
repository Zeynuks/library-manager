using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly LibraryDbContext _dbContext;

        public RentalRepository( LibraryDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public async Task<Rental?> TryGet( int id )
        {
            return await _dbContext.Rentals
                .Include( r => r.Book )
                .Include( r => r.Reader )
                .Include( r => r.Fines )
                .FirstOrDefaultAsync( r => r.Id == id );
        }

        public async Task<IReadOnlyList<Rental>> GetByReader( int readerId )
        {
            return await _dbContext.Rentals
                .AsNoTracking()
                .Where( r => r.ReaderId == readerId )
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Rental>> GetByBook( int bookId )
        {
            return await _dbContext.Rentals
                .AsNoTracking()
                .Where( r => r.BookId == bookId && r.ActualReturnDate == null )
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Rental>> GetReadOnlyList()
        {
            return await _dbContext.Rentals
                .AsNoTracking()
                .ToListAsync();
        }

        public void Add( Rental rental )
        {
            _dbContext.Rentals.Add( rental );
        }

        public void Delete( Rental rental )
        {
            _dbContext.Rentals.Remove( rental );
        }
    }
}