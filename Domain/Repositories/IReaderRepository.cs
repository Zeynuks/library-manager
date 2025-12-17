using Domain.Entities;

namespace Domain.Repositories
{
    public interface IReaderRepository
    {
        Task<Reader?> TryGet( int id );
        Task<Reader?> TryGetByPhone( string phoneNumber );
        Task<Reader?> TryGetWithRentails( int id );
        Task<IReadOnlyList<Reader>> GetReadOnlyList();
        void Add( Reader reader );
        void Delete( Reader reader );
    }
}