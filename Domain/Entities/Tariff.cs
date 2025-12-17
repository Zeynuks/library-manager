using Domain.Exceptions;

namespace Domain.Entities
{
    public class Tariff
    {
        public int Id { get; private init; }

        public string Name { get; private set; }

        //TODO Можно добавить тип, чтобы были возможности добавлять процентную скидку или статическую
        public decimal DailyRate { get; private set; }

        public Tariff( string name, decimal dailyRate )
        {
            Name = ValidateName( name );
            DailyRate = dailyRate;
        }

        public void Update( string name, decimal dailyRate )
        {
            Name = ValidateName( name );
            DailyRate = dailyRate;
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