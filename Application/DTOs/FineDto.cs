using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Application.DTOs
{
    public class FineDto
    {
        [Required]
        public int Id { get; init; }
        
        [Required]
        [StringLength( 500 )]
        public string Description { get; set; }

        [Required]
        [Range( 0.01, double.MaxValue )]
        public decimal Amount { get; set; }

        public FineDto( int id, string description, decimal amount )
        {
            Id = id;
            Description = description;
            Amount = amount;
        }
    }

    public class FineCreateDto : FineDto
    {
        [Required]
        public int RentalId { get; set; }

        public FineCreateDto( int id, int rentalId, string description, decimal amount )
            : base( id, description, amount )
        {
            RentalId = rentalId;
        }
    }

    public class FineUpdateDto : FineDto
    {
        public FineUpdateDto( int id, string description, decimal amount )
            : base( id, description, amount )
        {
        }
    }

    public class FineReadDto : FineDto
    {
        [Required]
        public int RentalId { get; set; }

        public FineReadDto( int id, int rentalId, string description, decimal amount )
            : base( id, description, amount )
        {
            RentalId = rentalId;
        }
    }

    public class FineWithRentalDto : FineDto
    {
        [Required]
        public RentalFullDto Rental { get; set; }

        public FineWithRentalDto( int id, string description, decimal amount, RentalFullDto rental )
            : base( id, description, amount )
        {
            Rental = rental;
        }
    }
}