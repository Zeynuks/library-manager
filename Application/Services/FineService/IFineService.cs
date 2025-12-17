using Application.DTOs;

namespace Application.Services.FineService
{
    public interface IFineService
    {
        Task<FineReadDto> GetById( int id );
        Task<IReadOnlyList<FineDto>> GetList();
        Task<int> Create( FineCreateDto dto );
        Task Update( int id, FineUpdateDto dto );
        Task Remove( int id );
    }
}