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
                fine.Description,
                fine.Amount
            );
        }
    }
}