using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class ReaderCategoryDto
    {
        [Required]
        public int Id { get; init; }

        [Required]
        [StringLength( 100 )]
        public string Name { get; set; }

        [Required]
        public decimal DiscountRate { get; set; }

        public ReaderCategoryDto( int id, string name, decimal discountRate )
        {
            Id = id;
            Name = name;
            DiscountRate = discountRate;
        }
    }

    public class ReaderCategoryCreateDto : ReaderCategoryDto
    {
        public ReaderCategoryCreateDto( int id, string name, decimal discountRate )
            : base( id, name, discountRate )
        {
        }
    }

    public class ReaderCategoryUpdateDto : ReaderCategoryDto
    {
        public ReaderCategoryUpdateDto( int id, string name, decimal discountRate )
            : base( id, name, discountRate )
        {
        }
    }

    public class ReaderCategoryReadDto : ReaderCategoryDto
    {
        public ReaderCategoryReadDto( int id, string name, decimal discountRate )
            : base( id, name, discountRate )
        {
        }
    }
}