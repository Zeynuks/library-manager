using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class FineRepository : IFineRepository
    {
        private readonly LibraryDbContext _dbContext;

        public FineRepository( LibraryDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public async Task<Fine?> TryGet( int id )
        {
            return await _dbContext.Fines
                .Include( f => f.Rental )
                .ThenInclude( r => r.Reader )
                .Include(f => f.Rental )
                .ThenInclude( r => r.Book )
                .FirstOrDefaultAsync( f => f.Id == id );
        }

        public async Task<Fine?> TryGetByRental( int rentalId )
        {
            return await _dbContext.Fines
                .AsNoTracking()
                .FirstOrDefaultAsync( f => f.RentalId == rentalId );
        }

        public async Task<IReadOnlyList<Fine>> GetReadOnlyList()
        {
            return await _dbContext.Fines
                .Include( f => f.Rental )
                .ThenInclude( r => r.Reader )
                .Include( f => f.Rental )
                .ThenInclude( r => r.Book )
                .AsNoTracking()
                .ToListAsync();
        }

        public void Add( Fine fine )
        {
            _dbContext.Fines.Add( fine );
        }

        public void Delete( Fine fine )
        {
            _dbContext.Fines.Remove( fine );
        }
    }
}