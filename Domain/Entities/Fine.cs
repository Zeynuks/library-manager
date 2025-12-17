using Domain.Exceptions;

namespace Domain.Entities
{
    public class Fine
    {
        public int Id { get; private init; }
        public int RentalId { get; private set; }
        public string Description { get; private set; }
        public decimal Amount { get; private set; }
        public virtual Rental Rental { get; private set; } = null!;

        public Fine( int rentalId, string description, decimal amount )
        {
            RentalId = rentalId;
            Description = ValidateDescription( description );
            Amount = ValidateAmount( amount );
        }

        public void Update( string description, decimal amount )
        {
            Description = ValidateDescription( description );
            Amount = ValidateAmount( amount );
        }

        private static string ValidateDescription( string description )
        {
            if ( string.IsNullOrWhiteSpace( description ) )
            {
                throw new DomainValidationException( "Description cannot be empty." );
            }

            return description.Trim();
        }

        private static decimal ValidateAmount( decimal amount )
        {
            if ( amount <= 0 )
            {
                throw new DomainValidationException( "Fine amount must be greater than zero." );
            }

            return amount;
        }
    }
}