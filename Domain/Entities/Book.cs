using Domain.Exceptions;

namespace Domain.Entities
{
    public class Book
    {
        public int Id { get; private init; }
        public string Title { get; private set; }
        public string Author { get; private set; } //TODO Можно вынести в отдельную сущность
        public string Genre { get; private set; } //TODO Можно вынести в отдельную сущность
        public decimal Deposit { get; private set; }
        public int TariffId { get; private set; }
        public virtual Tariff Tariff { get; private set; } = null!;
        public virtual ICollection<Rental> Rentals { get; private set; } = new List<Rental>();

        public Book( string title, string author, string genre, decimal deposit, int tariffId )
        {
            Title = ValidateTitle( title );
            Author = ValidateAuthor( author );
            Genre = ValidateGenre( genre );
            Deposit = ValidateDeposit( deposit );
            TariffId = tariffId;
        }

        public void Update( string title, string author, string genre, decimal deposit, int tariffId )
        {
            Title = ValidateTitle( title );
            Author = ValidateAuthor( author );
            Genre = ValidateGenre( genre );
            Deposit = ValidateDeposit( deposit );
            TariffId = tariffId;
        }

        private static string ValidateTitle( string title )
        {
            if ( string.IsNullOrWhiteSpace( title ) )
            {
                throw new DomainValidationException( "Title cannot be empty." );
            }

            return title.Trim();
        }

        private static string ValidateAuthor( string author )
        {
            if ( string.IsNullOrWhiteSpace( author ) )
            {
                throw new DomainValidationException( "Author name cannot be empty." );
            }

            return author.Trim();
        }

        private static string ValidateGenre( string genre )
        {
            if ( string.IsNullOrWhiteSpace( genre ) )
            {
                throw new DomainValidationException( "Genre cannot be empty." );
            }

            return genre.Trim();
        }

        private static decimal ValidateDeposit( decimal deposit )
        {
            if ( deposit <= 0 )
            {
                throw new DomainValidationException( "Deposit must be greater than zero." );
            }

            return deposit;
        }
    }
}