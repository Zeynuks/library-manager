using Application.DTOs;
using Application.Services.TariffService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Controller
{
    /// <summary>
    /// Операции над тарифом.
    /// </summary>
    [ApiController]
    [ApiExplorerSettings( GroupName = "library" )]
    [Route( "api/tariffs" )]
    public class TariffController : ControllerBase
    {
        private readonly ITariffService _tariffService;

        public TariffController( ITariffService tariffService )
        {
            _tariffService = tariffService;
        }

        /// <summary>
        /// Получить тариф по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор тарифа.</param>
        /// <returns>Объект тарифа.</returns>
        [HttpGet( "{id:int}" )]
        [Authorize( Roles = "Administrator,Manager,Operator" )]
        [SwaggerOperation( OperationId = "TariffGetById", Summary = "Получить тариф по Id" )]
        public async Task<IActionResult> GetById( int id )
        {
            TariffReadDto dto = await _tariffService.GetById( id );
            return Ok( dto );
        }

        /// <summary>
        /// Получить список тарифов.
        /// </summary>
        /// <returns>Список тарифов.</returns>
        [HttpGet]
        [Authorize( Roles = "Administrator,Manager,Operator" )]
        [SwaggerOperation( OperationId = "TariffGetList", Summary = "Список тарифов" )]
        public async Task<IActionResult> GetList()
        {
            IReadOnlyList<TariffReadDto> list = await _tariffService.GetList();
            return Ok( list );
        }

        /// <summary>
        /// Создать новый тариф.
        /// </summary>
        /// <param name="dto">Данные для создания тарифа.</param>
        /// <returns>Созданный тариф.</returns>
        [HttpPost]
        [Authorize( Roles = "Manager" )]
        [SwaggerOperation( OperationId = "TariffCreate", Summary = "Создать тариф" )]
        public async Task<IActionResult> Create( [FromBody] TariffCreateDto dto )
        {
            int id = await _tariffService.Create( dto );
            TariffReadDto created = await _tariffService.GetById( id );
            return CreatedAtAction( nameof( GetById ), new
            {
                id
            }, created );
        }

        /// <summary>
        /// Обновить тариф.
        /// </summary>
        /// <param name="id">Идентификатор тарифа.</param>
        /// <param name="dto">Новые данные тарифа.</param>
        /// <returns>Код 204 при успешном обновлении.</returns>
        [HttpPut( "{id:int}" )]
        [Authorize( Roles = "Manager" )]
        [SwaggerOperation( OperationId = "TariffUpdate", Summary = "Обновить тариф" )]
        public async Task<IActionResult> Update( int id, [FromBody] TariffUpdateDto dto )
        {
            await _tariffService.Update( id, dto );
            return NoContent();
        }

        /// <summary>
        /// Удалить тариф.
        /// </summary>
        /// <param name="id">Идентификатор тарифа.</param>
        /// <returns>Код 204 при успешном удалении.</returns>
        [HttpDelete( "{id:int}" )]
        [Authorize( Roles = "Manager" )]
        [SwaggerOperation( OperationId = "TariffDelete", Summary = "Удалить тариф" )]
        public async Task<IActionResult> Delete( int id )
        {
            await _tariffService.Remove( id );
            return NoContent();
        }
    }
}