using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers
{
    public static class FineMapper
    {
        public static FineReadDto ToReadDto( Fine fine )
        {
            return new FineReadDto(
                fine.Id,
                fine.RentalId,
                fine.Description,
                fine.Amount
            );
        }

        public static FineDto ToDto( Fine fine )
        {
            return new FineDto(
                fine.Id,
                fine.Description,
                fine.Amount
            );
        }
        
        public static FineWithRentalDto ToWithRentalDto( Fine fine )
        {
            return new FineWithRentalDto(
                fine.Id,
                fine.Description,
                fine.Amount,
                RentalMapper.ToFullDto( fine.Rental )
            );
        }
    }
}