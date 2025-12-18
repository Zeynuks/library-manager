using Application.DTOs;

namespace Application.Services.RentalService
{
    public interface IRentalService
    {
        Task<RentalFullDto> GetById( int id );
        Task<IReadOnlyList<RentalFullDto>> GetList();
        Task<int> Create( RentalCreateDto dto );
        Task Update( int id, RentalUpdateDto dto );
        Task<decimal> ReturnBook( int id, DateOnly actualReturnDate );
        Task Remove( int id );
    }
}