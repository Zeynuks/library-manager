using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers
{
    public static class RentalMapper
    {
        public static RentalDto ToDto( Rental rental )
        {
            return new RentalDto(
                rental.IssueDate,
                rental.ExpectedReturnDate,
                rental.RentalAmount,
                rental.ActualReturnDate
            );
        }

        public static RentalWithReaderDto ToWithReaderDto( Rental rental )
        {
            return new RentalWithReaderDto(
                rental.IssueDate,
                rental.ExpectedReturnDate,
                rental.RentalAmount,
                rental.ActualReturnDate,
                ReaderMapper.ToDto( rental.Reader )
            );
        }

        public static RentalFullDto ToFullDto( Rental rental )
        {
            return new RentalFullDto(
                rental.IssueDate,
                rental.ExpectedReturnDate,
                rental.RentalAmount,
                rental.ActualReturnDate,
                BookMapper.ToDto( rental.Book ),
                ReaderMapper.ToDto( rental.Reader ),
                rental.Fines.Select( FineMapper.ToDto ).ToArray()
            );
        }
    }
}