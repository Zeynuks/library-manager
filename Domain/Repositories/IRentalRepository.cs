using Domain.Entities;

namespace Domain.Repositories
{
    public interface IRentalRepository
    {
        Task<Rental?> TryGet( int id );
        Task<IReadOnlyList<Rental>> GetByReader( int readerId );
        Task<IReadOnlyList<Rental>> GetByBook( int bookId );
        Task<IReadOnlyList<Rental>> GetReadOnlyList();
        void Add( Rental rental );
        void Delete( Rental rental );
    }
}