using Domain.Entities;

namespace Domain.Repositories
{
    public interface IFineRepository
    {
        Task<Fine?> TryGet( int id );
        Task<Fine?> TryGetByRental( int rentalId );
        Task<IReadOnlyList<Fine>> GetReadOnlyList();
        void Add( Fine fine );
        void Delete( Fine fine );
    }
}