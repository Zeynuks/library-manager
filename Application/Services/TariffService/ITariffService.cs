using Application.DTOs;

namespace Application.Services.TariffService
{
    public interface ITariffService
    {
        Task<TariffReadDto> GetById( int id );
        Task<IReadOnlyList<TariffReadDto>> GetList();
        Task<int> Create( TariffCreateDto dto );
        Task Update( int id, TariffUpdateDto dto );
        Task Remove( int id );
    }
}