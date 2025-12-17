using Application.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Application.Services.AuthService;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Controller
{
    /// <summary>
    /// Аутентификация пользователей.
    /// </summary>
    [ApiController]
    [ApiExplorerSettings( GroupName = "library" )]
    [Route( "account" )]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController( IAuthService authService )
        {
            _authService = authService;
        }

        /// <summary>
        /// Логин пользователя.
        /// </summary>
        /// <param name="dto">Логин и пароль пользователя.</param>
        /// <returns>Сообщение об успешном входе или ошибка.</returns>
        [HttpPost( "login" )]
        [AllowAnonymous]
        [SwaggerOperation( OperationId = "AccountLogin", Summary = "Вход пользователя" )]
        public async Task<IActionResult> Login( [FromBody] LoginDto dto )
        {
            ClaimsPrincipal principal = await _authService.Authenticate( dto );
            await HttpContext.SignInAsync(
                "LibraryCookie",
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true
                } );
            return Ok( "Logged in successfully." );
        }

        /// <summary>
        /// Логаут пользователя.
        /// </summary>
        /// <returns>Сообщение об успешном выходе.</returns>
        [HttpPost( "logout" )]
        [Authorize]
        [SwaggerOperation( OperationId = "AccountLogout", Summary = "Выход пользователя" )]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync( "LibraryCookie" );
            return Ok( "Logged out successfully." );
        }
        
        /// <summary>
        /// Получить информацию о текущем пользователе.
        /// </summary>
        /// <returns>Имя и роли пользователя.</returns>
        [HttpGet("me")]
        [Authorize]
        [SwaggerOperation(OperationId = "AccountGetCurrent", Summary = "Получить текущего пользователя")]
        public IActionResult GetCurrent()
        {
            return Ok(new
            {
                Login = User.Identity?.Name,
                Roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value)
            });
        }
    }
}