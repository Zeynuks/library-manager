using Application.DTOs;

namespace Application.Services.BookService
{
    public interface IBookService
    {
        Task<BookWithTariffDto> GetById( int id );
        Task<BookWithRentalsDto> GetWithRentals( int id );
        Task<IReadOnlyList<BookWithTariffDto>> GetList();
        Task<int> Create( BookCreateDto dto );
        Task Update( int id, BookUpdateDto dto );
        Task Remove( int id );
        Task<bool> IsOccupied( int id );
    }
}