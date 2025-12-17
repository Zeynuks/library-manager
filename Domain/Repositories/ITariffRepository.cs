using Domain.Entities;

namespace Domain.Repositories
{
    public interface ITariffRepository
    {
        Task<Tariff?> TryGet( int id );
        Task<IReadOnlyList<Tariff>> GetReadOnlyList();
        void Add( Tariff tariff );
        void Delete( Tariff tariff );
    }
}