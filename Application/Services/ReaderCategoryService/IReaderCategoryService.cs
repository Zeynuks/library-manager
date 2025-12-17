using Application.DTOs;

namespace Application.Services.ReaderCategoryService
{
    public interface IReaderCategoryService
    {
        Task<ReaderCategoryReadDto> GetById( int id );
        Task<IReadOnlyList<ReaderCategoryDto>> GetList();
        Task<int> Create( ReaderCategoryCreateDto dto );
        Task Update( int id, ReaderCategoryUpdateDto dto );
        Task Remove( int id );
    }
}