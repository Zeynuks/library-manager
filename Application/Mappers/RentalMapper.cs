using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers
{
    public static class RentalMapper
    {
        public static RentalDto ToDto( Rental rental )
        {
            return new RentalDto(
                rental.Id,
                rental.IssueDate,
                rental.ExpectedReturnDate,
                rental.RentalAmount,
                rental.ActualReturnDate
            );
        }

        public static RentalWithReaderDto ToWithReaderDto( Rental rental )
        {
            return new RentalWithReaderDto(
                rental.Id,
                rental.IssueDate,
                rental.ExpectedReturnDate,
                rental.RentalAmount,
                rental.ActualReturnDate,
                ReaderMapper.ToDto( rental.Reader )
            );
        }

        public static RentalWithBookDto ToWithBookDto( Rental rental )
        {
            return new RentalWithBookDto(
                rental.Id,
                rental.IssueDate,
                rental.ExpectedReturnDate,
                rental.RentalAmount,
                rental.ActualReturnDate,
                BookMapper.ToDto( rental.Book )
            );
        }

        public static RentalFullDto ToFullDto( Rental rental )
        {
            return new RentalFullDto(
                rental.Id,
                rental.IssueDate,
                rental.ExpectedReturnDate,
                rental.RentalAmount,
                rental.ActualReturnDate,
                BookMapper.ToDto( rental.Book ),
                ReaderMapper.ToDto( rental.Reader )
            );
        }
    }
}