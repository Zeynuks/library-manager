using Domain.Entities;

namespace Domain.Repositories
{
    public interface IReaderCategoryRepository
    {
        Task<ReaderCategory?> TryGet( int id );
        Task<IReadOnlyList<ReaderCategory>> GetReadOnlyList();
        void Add( ReaderCategory category );
        void Delete( ReaderCategory category );
    }
}