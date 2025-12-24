using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers
{
    public static class ReaderMapper
    {
        public static ReaderDto ToDto( Reader reader )
        {
            return new ReaderDto(
                reader.Id,
                reader.FirstName,
                reader.MiddleName,
                reader.LastName,
                reader.Address,
                reader.PhoneNumber
            );
        }

        public static ReaderWithCategoryDto ToWithCategoryDto( Reader reader )
        {
            return new ReaderWithCategoryDto(
                reader.Id,
                reader.FirstName,
                reader.MiddleName,
                reader.LastName,
                reader.Address,
                reader.PhoneNumber,
                reader.CategoryId,
                ReaderCategoryMapper.ToReadDto( reader.Category )
            );
        }

        public static ReaderWithRentalsDto ToWithRentalsDto( Reader reader )
        {
            return new ReaderWithRentalsDto(
                reader.Id,
                reader.FirstName,
                reader.MiddleName,
                reader.LastName,
                reader.Address,
                reader.PhoneNumber,
                reader.Rentals.Select( RentalMapper.ToWithBookDto ).ToArray()
            );
        }
    }
}