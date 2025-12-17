using Application.DTOs;
using Application.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Controller
{
    /// <summary>
    /// Операции над пользователями.
    /// </summary>
    [ApiController]
    [ApiExplorerSettings( GroupName = "library" )]
    [Route( "api/users" )]
    [Authorize( Roles = "Administrator" )]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController( IUserService userService )
        {
            _userService = userService;
        }

        /// <summary>
        /// Получить пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>Объект пользователя.</returns>
        [HttpGet( "{id:int}" )]
        [SwaggerOperation( OperationId = "UserGetById", Summary = "Получить пользователя по Id" )]
        public async Task<IActionResult> GetById( int id )
        {
            UserDto dto = await _userService.GetById( id );
            return Ok( dto );
        }

        /// <summary>
        /// Получить список всех пользователей.
        /// </summary>
        /// <returns>Список пользователей.</returns>
        [HttpGet]
        [SwaggerOperation( OperationId = "UserGetList", Summary = "Список пользователей" )]
        public async Task<IActionResult> GetList()
        {
            IReadOnlyList<UserDto> list = await _userService.GetList();
            return Ok( list );
        }

        /// <summary>
        /// Создать нового пользователя.
        /// </summary>
        /// <param name="dto">Данные для создания пользователя.</param>
        /// <returns>Созданный пользователь.</returns>
        [HttpPost]
        [SwaggerOperation( OperationId = "UserCreate", Summary = "Создать пользователя" )]
        public async Task<IActionResult> Create( [FromBody] UserCreateDto dto )
        {
            int id = await _userService.Create( dto );
            UserDto created = await _userService.GetById( id );
            return CreatedAtAction( nameof( GetById ), new
            {
                id
            }, created );
        }

        /// <summary>
        /// Обновить данные пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="dto">Новые данные пользователя.</param>
        /// <returns>Код 204 при успешном обновлении.</returns>
        [HttpPut( "{id:int}" )]
        [SwaggerOperation( OperationId = "UserUpdate", Summary = "Обновить пользователя" )]
        public async Task<IActionResult> Update( int id, [FromBody] UserUpdateDto dto )
        {
            await _userService.Update( id, dto );
            return NoContent();
        }

        /// <summary>
        /// Удалить пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>Код 204 при успешном удалении.</returns>
        [HttpDelete( "{id:int}" )]
        [SwaggerOperation( OperationId = "UserDelete", Summary = "Удалить пользователя" )]
        public async Task<IActionResult> Delete( int id )
        {
            await _userService.Remove( id );
            return NoContent();
        }

        /// <summary>
        /// Заблокировать пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>Код 204 при успешной блокировке.</returns>
        [HttpPost( "{id:int}/block" )]
        [SwaggerOperation( OperationId = "UserBlock", Summary = "Заблокировать пользователя" )]
        public async Task<IActionResult> Block( int id )
        {
            await _userService.Block( id );
            return NoContent();
        }

        /// <summary>
        /// Разблокировать пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>Код 204 при успешной разблокировке.</returns>
        [HttpPost( "{id:int}/unblock" )]
        [SwaggerOperation( OperationId = "UserUnblock", Summary = "Разблокировать пользователя" )]
        public async Task<IActionResult> Unblock( int id )
        {
            await _userService.Unblock( id );
            return NoContent();
        }
    }
}