using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class RentalDto
    {
        [Required]
        public DateOnly IssueDate { get; set; }

        [Required]
        public DateOnly ExpectedReturnDate { get; set; }

        [Required]
        public decimal RentalAmount { get; set; }

        public DateOnly? ActualReturnDate { get; set; }


        public RentalDto(
            DateOnly issueDate,
            DateOnly expectedReturnDate,
            decimal rentalAmount,
            DateOnly? actualReturnDate = null )
        {
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
            int bookId,
            int readerId,
            DateOnly issueDate,
            DateOnly expectedReturnDate,
            decimal rentalAmount )
            : base( issueDate, expectedReturnDate, rentalAmount )
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
            int bookId,
            int readerId,
            DateOnly issueDate,
            DateOnly expectedReturnDate,
            decimal rentalAmount )
            : base( issueDate, expectedReturnDate, rentalAmount )
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

        [Required]
        public FineDto[] Fines { get; set; }

        public RentalFullDto(
            DateOnly issueDate,
            DateOnly expectedReturnDate,
            decimal rentalAmount,
            DateOnly? actualReturnDate,
            BookDto book,
            ReaderDto reader,
            FineDto[] fines ) : base( issueDate, expectedReturnDate, rentalAmount, actualReturnDate )
        {
            Book = book;
            Reader = reader;
            Fines = fines;
        }
    }

    public class RentalWithReaderDto : RentalDto
    {
        [Required]
        public ReaderDto Reader { get; set; }

        public RentalWithReaderDto(
            DateOnly issueDate,
            DateOnly expectedReturnDate,
            decimal rentalAmount,
            DateOnly? actualReturnDate,
            ReaderDto reader ) : base( issueDate, expectedReturnDate, rentalAmount, actualReturnDate )
        {
            Reader = reader;
        }
    }

    public class RentalWithBookDto : RentalDto
    {
        [Required]
        public BookDto Book { get; set; }

        public RentalWithBookDto(
            DateOnly issueDate,
            DateOnly expectedReturnDate,
            decimal rentalAmount,
            DateOnly? actualReturnDate,
            BookDto book ) : base( issueDate, expectedReturnDate, rentalAmount, actualReturnDate )
        {
            Book = book;
        }
    }
}