using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryDbContext _dbContext;

        public UserRepository( LibraryDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public async Task<User?> TryGet( int id )
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync( u => u.Id == id );
        }

        public async Task<User?> TryGetByLogin( string login )
        {
            return await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync( u => u.Login == login );
        }

        public async Task<IReadOnlyList<User>> GetReadOnlyList()
        {
            return await _dbContext.Users
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> Exists( string login )
        {
            return await _dbContext.Users
                .AnyAsync( u => u.Login == login );
        }

        public void Add( User user )
        {
            _dbContext.Users.Add( user );
        }

        public void Delete( User user )
        {
            _dbContext.Users.Remove( user );
        }
    }
}