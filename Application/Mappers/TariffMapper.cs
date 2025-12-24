using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers
{
    public static class TariffMapper
    {
        public static TariffReadDto ToReadDto( Tariff tariff )
        {
            return new TariffReadDto(
                tariff.Id,
                tariff.Name,
                tariff.DailyRate
            );
        }

        public static TariffDto ToDto( Tariff tariff )
        {
            return new TariffDto(
                tariff.Id,
                tariff.Name,
                tariff.DailyRate
            );
        }
    }
}