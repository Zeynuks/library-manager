using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class RentalDto
    {
        [Required]
        public int Id { get; init; }
        
        [Required]
        public DateOnly IssueDate { get; set; }

        [Required]
        public DateOnly ExpectedReturnDate { get; set; }

        [Required]
        public decimal RentalAmount { get; set; }

        public DateOnly? ActualReturnDate { get; set; }


        public RentalDto(
            int id,
            DateOnly issueDate,
            DateOnly expectedReturnDate,
            decimal rentalAmount,
            DateOnly? actualReturnDate = null )
        {
            Id = id;
            IssueDate = issueDate;
            ExpectedReturnDate = expectedReturnDate;
            RentalAmount = rentalAmount;
            ActualReturnDate = actualReturnDate;
        }
    }

    public class RentalCreateDto : RentalDto
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        public int ReaderId { get; set; }

        public RentalCreateDto(
            int id,
            int bookId,
            int readerId,
            DateOnly issueDate,
            DateOnly expectedReturnDate,
            decimal rentalAmount )
            : base( id, issueDate, expectedReturnDate, rentalAmount )
        {
            BookId = bookId;
            ReaderId = readerId;
        }
    }

    public class RentalUpdateDto : RentalDto
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        public int ReaderId { get; set; }

        public RentalUpdateDto(
            int id,
            int bookId,
            int readerId,
            DateOnly issueDate,
            DateOnly expectedReturnDate,
            decimal rentalAmount )
            : base( id, issueDate, expectedReturnDate, rentalAmount )
        {
            BookId = bookId;
            ReaderId = readerId;
        }
    }

    public class RentalFullDto : RentalDto
    {
        [Required]
        public BookDto Book { get; set; }

        [Required]
        public ReaderDto Reader { get; set; }

        public RentalFullDto(
            int id,
            DateOnly issueDate,
            DateOnly expectedReturnDate,
            decimal rentalAmount,
            DateOnly? actualReturnDate,
            BookDto book,
            ReaderDto reader ) : base( id, issueDate, expectedReturnDate, rentalAmount, actualReturnDate )
        {
            Book = book;
            Reader = reader;
        }
    }

    public class RentalWithReaderDto : RentalDto
    {
        [Required]
        public ReaderDto Reader { get; set; }

        public RentalWithReaderDto(
            int id,
            DateOnly issueDate,
            DateOnly expectedReturnDate,
            decimal rentalAmount,
            DateOnly? actualReturnDate,
            ReaderDto reader ) : base( id, issueDate, expectedReturnDate, rentalAmount, actualReturnDate )
        {
            Reader = reader;
        }
    }

    public class RentalWithBookDto : RentalDto
    {
        [Required]
        public BookDto Book { get; set; }

        public RentalWithBookDto(
            int id,
            DateOnly issueDate,
            DateOnly expectedReturnDate,
            decimal rentalAmount,
            DateOnly? actualReturnDate,
            BookDto book ) : base( id, issueDate, expectedReturnDate, rentalAmount, actualReturnDate )
        {
            Book = book;
        }
    }
}