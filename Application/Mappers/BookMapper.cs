using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers
{
    public static class BookMapper
    {
        public static BookDto ToDto( Book book )
        {
            return new BookDto(
                book.Id,
                book.Title,
                book.Author,
                book.Genre,
                book.Deposit,
                book.TariffId
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
                book.TariffId,
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
                book.TariffId,
                book.Rentals.Select( RentalMapper.ToWithReaderDto ).ToArray()
            );
        }
    }
}