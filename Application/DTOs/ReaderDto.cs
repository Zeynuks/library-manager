using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class ReaderDto
    {
        [Required]
        public int Id { get; init; }
        
        [Required]
        [StringLength( 100 )]
        public string FirstName { get; set; }

        [StringLength( 100 )]
        public string? MiddleName { get; set; }

        [Required]
        [StringLength( 100 )]
        public string LastName { get; set; }

        [Required]
        [StringLength( 200 )]
        public string Address { get; set; }

        [Required]
        [StringLength( 20 )]
        public string PhoneNumber { get; set; }

        public ReaderDto(
            int id,
            string firstName,
            string? middleName,
            string lastName,
            string address,
            string phoneNumber )
        {
            Id = id;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Address = address;
            PhoneNumber = phoneNumber;
        }
    }

    public class ReaderCreateDto : ReaderDto
    {
        [Required]
        public int CategoryId { get; set; }

        public ReaderCreateDto(
            int id,
            string firstName,
            string? middleName,
            string lastName,
            string address,
            string phoneNumber,
            int categoryId )
            : base( id, firstName, middleName, lastName, address, phoneNumber )
        {
            CategoryId = categoryId;
        }
    }

    public class ReaderUpdateDto : ReaderDto
    {
        public ReaderUpdateDto(
            int id,
            string firstName,
            string? middleName,
            string lastName,
            string address,
            string phoneNumber )
            : base( id, firstName, middleName, lastName, address, phoneNumber )
        {
        }
    }

    public class ReaderWithCategoryDto : ReaderDto
    {
        [Required]
        public ReaderCategoryReadDto Category { get; set; }

        public ReaderWithCategoryDto(
            int id,
            string firstName,
            string? middleName,
            string lastName,
            string address,
            string phoneNumber,
            ReaderCategoryReadDto category )
            : base( id, firstName, middleName, lastName, address, phoneNumber )
        {
            Category = category;
        }
    }

    public class ReaderWithRentalsDto : ReaderDto
    {
        [Required]
        public RentalWithBookDto[] Rentals { get; init; }

        public ReaderWithRentalsDto(
            int id,
            string firstName,
            string? middleName,
            string lastName,
            string address,
            string phoneNumber,
            RentalWithBookDto[] rentals )
            : base( id, firstName, middleName, lastName, address, phoneNumber )
        {
            Rentals = rentals;
        }
    }
}