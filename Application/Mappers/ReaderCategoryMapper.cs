using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers
{
    public static class ReaderCategoryMapper
    {
        public static ReaderCategoryReadDto ToReadDto( ReaderCategory category )
        {
            return new ReaderCategoryReadDto(
                category.Id,
                category.Name,
                category.DiscountRate
            );
        }

        public static ReaderCategoryDto ToDto( ReaderCategory category )
        {
            return new ReaderCategoryDto(
                category.Id,
                category.Name,
                category.DiscountRate
            );
        }
    }
}