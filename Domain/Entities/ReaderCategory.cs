using Domain.Exceptions;

namespace Domain.Entities
{
    public class ReaderCategory
    {
        public int Id { get; private init; }
        public string Name { get; private set; }
        public decimal DiscountRate { get; private set; }
        public virtual ICollection<Reader> Readers { get; private set; } = new List<Reader>();

        public ReaderCategory( string name, decimal discountRate )
        {
            Name = ValidateName( name );
            DiscountRate = discountRate;
        }

        public void Update( string name, decimal discountRate )
        {
            Name = ValidateName( name );
            DiscountRate = discountRate;
        }

        private static string ValidateName( string name )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new DomainValidationException( "Name cannot be empty." );
            }

            return name.Trim();
        }
    }
}