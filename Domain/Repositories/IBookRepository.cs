using Domain.Entities;

namespace Domain.Repositories
{
    public interface IBookRepository
    {
        Task<Book?> TryGet( int id );
        Task<Book?> TryGetWithRentals( int id );
        Task<IReadOnlyList<Book>> GetReadOnlyList();
        void Add( Book book );
        void Delete( Book book );
    }
}