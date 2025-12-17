using Domain.Exceptions;

namespace Domain.Entities
{
    public class Rental
    {
        public int Id { get; private init; }
        public int BookId { get; private set; }
        public int ReaderId { get; private set; }
        public DateOnly IssueDate { get; private init; }
        public DateOnly ExpectedReturnDate { get; private set; }
        public DateOnly? ActualReturnDate { get; private set; }
        public decimal RentalAmount { get; private set; }
        public virtual Book Book { get; private set; } = null!;
        public virtual Reader Reader { get; private set; } = null!;
        public virtual ICollection<Fine> Fines { get; private set; } = new List<Fine>();

        public Rental(
            int bookId,
            int readerId,
            DateOnly issueDate,
            DateOnly expectedReturnDate,
            decimal rentalAmount )
        {
            BookId = bookId;
            ReaderId = readerId;
            IssueDate = ValidateDate( issueDate );
            ExpectedReturnDate = ValidateExpectedReturnDate( issueDate, expectedReturnDate );
            RentalAmount = rentalAmount;
        }

        public static decimal CalculateRentalAmount(
            DateOnly issueDate,
            DateOnly expectedReturnDate,
            decimal dailyRate,
            decimal discountRate )
        {
            int rentalDays = Math.Max( 1, expectedReturnDate.DayNumber - issueDate.DayNumber + 1 );
            return rentalDays * dailyRate * ( 1 - discountRate );
        }

        public void Update( DateOnly issueDate, DateOnly expectedReturnDate, decimal rentalAmount )
        {
            ExpectedReturnDate = ValidateExpectedReturnDate( issueDate, expectedReturnDate );
            RentalAmount = rentalAmount;
        }

        public void ReturnBook( DateOnly actualReturnDate )
        {
            if ( ActualReturnDate.HasValue )
            {
                throw new DomainValidationException( "Rental is already closed." );
            }

            ActualReturnDate = ValidateActualReturnDate( IssueDate, actualReturnDate );
        }

        private static DateOnly ValidateDate( DateOnly issueDate )
        {
            if ( issueDate > DateOnly.FromDateTime( DateTime.UtcNow ) )
            {
                throw new DomainValidationException( "Issue date cannot be in the future." );
            }

            return issueDate;
        }

        private static DateOnly ValidateExpectedReturnDate( DateOnly issueDate, DateOnly expectedReturnDate )
        {
            if ( expectedReturnDate <= issueDate )
            {
                throw new DomainValidationException( "Expected return date must be after issue date." );
            }

            return expectedReturnDate;
        }

        private static DateOnly ValidateActualReturnDate( DateOnly issueDate, DateOnly actualReturnDate )
        {
            if ( actualReturnDate < issueDate )
            {
                throw new DomainValidationException( "Actual return date cannot be earlier than issue date." );
            }

            return actualReturnDate;
        }
    }
}