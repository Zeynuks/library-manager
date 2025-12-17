using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class BookDto
    {
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

        public BookDto(
            string title,
            string author,
            string genre,
            decimal deposit )
        {
            Title = title;
            Author = author;
            Genre = genre;
            Deposit = deposit;
        }
    }

    public class BookCreateDto : BookDto
    {
        [Required]
        public int TariffId { get; init; }

        public BookCreateDto(
            string title,
            string author,
            string genre,
            decimal deposit,
            int tariffId )
            : base( title, author, genre, deposit )
        {
            TariffId = tariffId;
        }
    }

    public class BookWithTariffDto : BookDto
    {
        [Required]
        public int Id { get; init; }

        [Required]
        public TariffReadDto Tariff { get; init; }

        public BookWithTariffDto(
            int id,
            string title,
            string author,
            string genre,
            decimal deposit,
            TariffReadDto tariff )
            : base( title, author, genre, deposit )
        {
            Id = id;
            Tariff = tariff;
        }
    }

    public class BookWithRentalsDto : BookDto
    {
        [Required]
        public int Id { get; init; }

        [Required]
        public RentalWithReaderDto[] Rentals { get; init; }

        public BookWithRentalsDto(
            int id,
            string title,
            string author,
            string genre,
            decimal deposit,
            RentalWithReaderDto[] rentals )
            : base( title, author, genre, deposit )
        {
            Id = id;
            Rentals = rentals;
        }
    }

    public class BookUpdateDto : BookDto
    {
        [Required]
        public int TariffId { get; init; }

        public BookUpdateDto(
            string title,
            string author,
            string genre,
            decimal deposit,
            int tariffId )
            : base( title, author, genre, deposit )
        {
            TariffId = tariffId;
        }
    }
}