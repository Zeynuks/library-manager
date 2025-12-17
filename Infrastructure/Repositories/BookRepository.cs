using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _dbContext;

        public BookRepository( LibraryDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public async Task<Book?> TryGet( int id )
        {
            return await _dbContext.Books
                .Include( b => b.Tariff )
                .FirstOrDefaultAsync( b => b.Id == id );
        }

        public async Task<Book?> TryGetWithRentals( int id )
        {
            return await _dbContext.Books
                .Include( b => b.Rentals )
                .FirstOrDefaultAsync( b => b.Id == id );
        }

        public async Task<IReadOnlyList<Book>> GetReadOnlyList()
        {
            return await _dbContext.Books
                .AsNoTracking()
                .ToListAsync();
        }

        public void Add( Book book )
        {
            _dbContext.Books.Add( book );
        }

        public void Delete( Book book )
        {
            _dbContext.Books.Remove( book );
        }
    }
}