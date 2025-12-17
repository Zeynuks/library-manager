using Domain.Enums;

namespace Domain.Exceptions
{
    public class InvalidCredentialsException : AppException
    {
        public InvalidCredentialsException( string message ) : base( ErrorCode.Unauthorized, message )
        {
        }
    }
}