using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class ReaderDto
    {
        [Required]
        [StringLength( 100 )]
        public string FirstName { get; set; }

        [StringLength( 100 )]
        public string MiddleName { get; set; }

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
            string firstName,
            string middleName,
            string lastName,
            string address,
            string phoneNumber )
        {
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
            string firstName,
            string middleName,
            string lastName,
            string address,
            string phoneNumber,
            int categoryId )
            : base( firstName, middleName, lastName, address, phoneNumber )
        {
            CategoryId = categoryId;
        }
    }

    public class ReaderUpdateDto : ReaderDto
    {
        public ReaderUpdateDto(
            string firstName,
            string middleName,
            string lastName,
            string address,
            string phoneNumber )
            : base( firstName, middleName, lastName, address, phoneNumber )
        {
        }
    }

    public class ReaderWithCategoryDto : ReaderDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public ReaderCategoryReadDto Category { get; set; }

        public ReaderWithCategoryDto(
            int id,
            string firstName,
            string middleName,
            string lastName,
            string address,
            string phoneNumber,
            ReaderCategoryReadDto category )
            : base( firstName, middleName, lastName, address, phoneNumber )
        {
            Id = id;
            Category = category;
        }
    }

    public class ReaderWithRentalsDto : ReaderDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public RentalDto[] Rentals { get; init; }

        public ReaderWithRentalsDto(
            int id,
            string firstName,
            string middleName,
            string lastName,
            string address,
            string phoneNumber,
            RentalDto[] rentals )
            : base( firstName, middleName, lastName, address, phoneNumber )
        {
            Id = id;
            Rentals = rentals;
        }
    }
}