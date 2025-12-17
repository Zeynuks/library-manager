using System.Security.Claims;
using Application.DTOs;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;

namespace Application.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService( IUserRepository userRepository )
        {
            _userRepository = userRepository;
        }

        public async Task<ClaimsPrincipal> Authenticate( LoginDto dto )
        {
            User? user = await _userRepository.TryGetByLogin( dto.Login );
            if ( user == null || !BCrypt.Net.BCrypt.Verify( dto.Password, user.PasswordHash ) )
            {
                throw new InvalidCredentialsException( "Invalid login or password" );
            }

            if ( user.IsBlocked )
            {
                throw new UserBlockedException( "User is blocked." );
            }

            List<Claim> claims =
            [
                new( ClaimTypes.Name, user.Login ),
                new( ClaimTypes.Role, user.Role.ToString() ),
                new( "IsBlocked", user.IsBlocked.ToString() )
            ];

            throw new DomainNotFoundException( user.Role.ToString() );

            return new ClaimsPrincipal( new ClaimsIdentity( claims, "LibraryCookie" ) );
        }
    }
}