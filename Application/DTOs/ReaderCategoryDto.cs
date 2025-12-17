using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class ReaderCategoryDto
    {
        [Required]
        [StringLength( 100 )]
        public string Name { get; set; }

        [Required]
        [StringLength( 20 )]
        public decimal DiscountRate { get; set; }

        public ReaderCategoryDto( string name, decimal discountRate )
        {
            Name = name;
            DiscountRate = discountRate;
        }
    }

    public class ReaderCategoryCreateDto : ReaderCategoryDto
    {
        public ReaderCategoryCreateDto( string name, decimal discountRate )
            : base( name, discountRate )
        {
        }
    }

    public class ReaderCategoryUpdateDto : ReaderCategoryDto
    {
        [Required]
        public int Id { get; set; }

        public ReaderCategoryUpdateDto( int id, string name, decimal discountRate )
            : base( name, discountRate )
        {
            Id = id;
        }
    }

    public class ReaderCategoryReadDto : ReaderCategoryDto
    {
        [Required]
        public int Id { get; set; }

        public ReaderCategoryReadDto( int id, string name, decimal discountRate )
            : base( name, discountRate )
        {
            Id = id;
        }
    }
}