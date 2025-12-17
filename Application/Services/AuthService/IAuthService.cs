using System.Security.Claims;
using Application.DTOs;

namespace Application.Services.AuthService
{
    public interface IAuthService
    {
        Task<ClaimsPrincipal> Authenticate( LoginDto dto );
    }
}