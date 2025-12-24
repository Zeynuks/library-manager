using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class BookDto
    {
        [Required]
        public int Id { get; init; }
        
        [Required]
        [StringLength( 200 )]
        public string Title { get; init; }

        [Required]
        [StringLength( 200 )]
        public string Author { get; init; }

        [Required]
        [StringLength( 100 )]
        public string Genre { get; init; }

        [Required]
        public decimal Deposit { get; init; }
        
        [Required]
        public int TariffId { get; init; }

        public BookDto(
            int id,
            string title,
            string author,
            string genre,
            decimal deposit,
            int tariffId)
        {
            Id = id;
            Title = title;
            Author = author;
            Genre = genre;
            Deposit = deposit;
            TariffId = tariffId;
        }
    }

    public class BookCreateDto : BookDto
    {
        public BookCreateDto(
            int id,
            string title,
            string author,
            string genre,
            decimal deposit,
            int tariffId )
            : base( id, title, author, genre, deposit, tariffId )
        {
        }
    }

    public class BookWithTariffDto : BookDto
    {
        [Required]
        public TariffReadDto Tariff { get; init; }

        public BookWithTariffDto(
            int id,
            string title,
            string author,
            string genre,
            decimal deposit,
            int tariffId,
            TariffReadDto tariff )
            : base( id, title, author, genre, deposit, tariffId )
        {
            Tariff = tariff;
        }
    }

    public class BookWithRentalsDto : BookDto
    {
        [Required]
        public RentalWithReaderDto[] Rentals { get; init; }

        public BookWithRentalsDto(
            int id,
            string title,
            string author,
            string genre,
            decimal deposit,
            int tariffId,
            RentalWithReaderDto[] rentals )
            : base( id, title, author, genre, deposit, tariffId )
        {
            Rentals = rentals;
        }
    }

    public class BookUpdateDto : BookDto
    {
        public BookUpdateDto(
            int id,
            string title,
            string author,
            string genre,
            decimal deposit,
            int tariffId )
            : base( id, title, author, genre, deposit, tariffId )
        {
        }
    }
}