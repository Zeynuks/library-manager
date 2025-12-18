using Domain.Exceptions;

namespace Domain.Entities
{
    public class Reader
    {
        public int Id { get; private init; }
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }
        public string Address { get; private set; } //TODO Можно вынести в отдельную сущность
        public string PhoneNumber { get; private set; }
        public int CategoryId { get; private set; }
        public virtual ReaderCategory Category { get; private set; } = null!;
        public virtual ICollection<Rental> Rentals { get; private set; } = new List<Rental>();

        public Reader(
            string firstName,
            string middleName,
            string lastName,
            string address,
            string phoneNumber,
            int categoryId )
        {
            FirstName = ValidateName( firstName );
            MiddleName = middleName;
            LastName = ValidateName( lastName );
            Address = ValidateAddress( address );
            PhoneNumber = ValidatePhoneNumber( phoneNumber );
            CategoryId = categoryId;
        }

        public void Update(
            string firstName,
            string middleName,
            string lastName,
            string address,
            string phoneNumber,
            int categoryId )
        {
            FirstName = ValidateName( firstName );
            MiddleName = middleName;
            LastName = ValidateName( lastName );
            Address = ValidateAddress( address );
            PhoneNumber = ValidatePhoneNumber( phoneNumber );
            CategoryId = categoryId;
        }

        private static string ValidateName( string name )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new DomainValidationException( "Name cannot be empty." );
            }

            return name.Trim();
        }

        private static string ValidateAddress( string address )
        {
            if ( string.IsNullOrWhiteSpace( address ) )
            {
                throw new DomainValidationException( "Address cannot be empty." );
            }

            return address.Trim();
        }

        private static string ValidatePhoneNumber( string phoneNumber )
        {
            if ( string.IsNullOrWhiteSpace( phoneNumber ) )
            {
                throw new DomainValidationException( "Phone number cannot be empty." );
            }

            if ( phoneNumber.Length is < 6 or > 20 )
            {
                throw new DomainValidationException( "Phone number has invalid length." );
            }

            return phoneNumber.Trim();
        }
    }
}