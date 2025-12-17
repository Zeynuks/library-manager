using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; private init; }
        public string Login { get; private set; }
        public string PasswordHash { get; private set; }
        public UserRole Role { get; private set; }
        public bool IsBlocked { get; private set; }

        public User( string login, string passwordHash, UserRole role )
        {
            Login = ValidateLogin( login );
            PasswordHash = ValidatePasswordHash( passwordHash );
            Role = role;
            IsBlocked = false;
        }

        public void Update( string login, UserRole role )
        {
            Login = ValidateLogin( login );
            Role = role;
        }

        public void ChangePassword( string newPasswordHash )
        {
            PasswordHash = ValidatePasswordHash( newPasswordHash );
        }

        public void Block()
        {
            IsBlocked = true;
        }

        public void Unblock()
        {
            IsBlocked = false;
        }

        private static string ValidateLogin( string login )
        {
            if ( string.IsNullOrWhiteSpace( login ) )
            {
                throw new DomainValidationException( "Username cannot be empty." );
            }

            return login.Trim();
        }

        private static string ValidatePasswordHash( string passwordHash )
        {
            if ( string.IsNullOrWhiteSpace( passwordHash ) )
            {
                throw new DomainValidationException( "Password cannot be empty." );
            }

            return passwordHash;
        }
    }
}