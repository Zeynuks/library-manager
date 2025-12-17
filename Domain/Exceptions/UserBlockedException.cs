using Domain.Enums;

namespace Domain.Exceptions
{
    public class UserBlockedException : AppException
    {
        public UserBlockedException( string message ) : base( ErrorCode.Forbidden, message )
        {
        }
    }
}