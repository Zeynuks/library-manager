using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class FineDto
    {
        [Required]
        [StringLength( 500 )]
        public string Description { get; set; }

        [Required]
        [Range( 0.01, double.MaxValue )]
        public decimal Amount { get; set; }

        public FineDto( string description, decimal amount )
        {
            Description = description;
            Amount = amount;
        }
    }

    public class FineCreateDto : FineDto
    {
        [Required]
        public int RentalId { get; set; }

        public FineCreateDto( int rentalId, string description, decimal amount )
            : base( description, amount )
        {
            RentalId = rentalId;
        }
    }

    public class FineUpdateDto : FineDto
    {
        public FineUpdateDto( string description, decimal amount )
            : base( description, amount )
        {
        }
    }

    public class FineReadDto : FineDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int RentalId { get; set; }

        public FineReadDto( int id, int rentalId, string description, decimal amount )
            : base( description, amount )
        {
            Id = id;
            RentalId = rentalId;
        }
    }
}