using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TariffRepository : ITariffRepository
    {
        private readonly LibraryDbContext _dbContext;

        public TariffRepository( LibraryDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public async Task<Tariff?> TryGet( int id )
        {
            return await _dbContext.Tariffs
                .FirstOrDefaultAsync( t => t.Id == id );
        }

        public async Task<IReadOnlyList<Tariff>> GetReadOnlyList()
        {
            return await _dbContext.Tariffs
                .AsNoTracking()
                .ToListAsync();
        }

        public void Add( Tariff tariff )
        {
            _dbContext.Tariffs.Add( tariff );
        }

        public void Delete( Tariff tariff )
        {
            _dbContext.Tariffs.Remove( tariff );
        }
    }
}