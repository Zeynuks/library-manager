using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> TryGet( int id );
        Task<User?> TryGetByLogin( string login );
        Task<IReadOnlyList<User>> GetReadOnlyList();
        Task<bool> Exists( string login );
        void Add( User user );
        void Delete( User user );
    }
}