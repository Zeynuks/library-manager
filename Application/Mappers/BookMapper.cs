using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers
{
    public static class BookMapper
    {
        public static BookDto ToDto( Book book )
        {
            return new BookDto(
                book.Title,
                book.Author,
                book.Genre,
                book.Deposit
            );
        }

        public static BookWithTariffDto ToWithTariffDto( Book book )
        {
            return new BookWithTariffDto(
                book.Id,
                book.Title,
                book.Author,
                book.Genre,
                book.Deposit,
                TariffMapper.ToReadDto( book.Tariff )
            );
        }

        public static BookWithRentalsDto ToWithRentalsDto( Book book )
        {
            return new BookWithRentalsDto(
                book.Id,
                book.Title,
                book.Author,
                book.Genre,
                book.Deposit,
                book.Rentals.Select( RentalMapper.ToWithReaderDto ).ToArray()
            );
        }
    }
}