using Application.DTOs;

namespace Application.Services.ReaderService
{
    public interface IReaderService
    {
        Task<ReaderWithCategoryDto> GetById( int id );
        Task<ReaderWithRentalsDto> GetWithRentals( int id );
        Task<IReadOnlyList<ReaderWithCategoryDto>> GetList();
        Task<int> Create( ReaderCreateDto dto );
        Task Update( int id, ReaderUpdateDto dto );
        Task Remove( int id );
    }
}