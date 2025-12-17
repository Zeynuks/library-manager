using Application.DTOs;
using Application.Services.FineService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Controller
{
    /// <summary>
    /// Операции над штрафом.
    /// </summary>
    [ApiController]
    [ApiExplorerSettings( GroupName = "library" )]
    [Route( "api/fines" )]
    public class FineController : ControllerBase
    {
        private readonly IFineService _fineService;

        public FineController( IFineService fineService )
        {
            _fineService = fineService;
        }

        /// <summary>
        /// Получить штраф по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор штрафа.</param>
        /// <returns>Объект штрафа.</returns>
        [HttpGet( "{id:int}" )]
        [Authorize(Roles = "Administrator,Manager,Operator")]
        [SwaggerOperation( OperationId = "FineGetById", Summary = "Получить штраф по Id" )]
        public async Task<IActionResult> GetById( int id )
        {
            FineReadDto dto = await _fineService.GetById( id );
            return Ok( dto );
        }

        /// <summary>
        /// Получить список штрафов.
        /// </summary>
        /// <returns>Список штрафов.</returns>
        [HttpGet]
        [Authorize(Roles = "Administrator,Manager,Operator")]
        [SwaggerOperation( OperationId = "FineGetList", Summary = "Список штрафов" )]
        public async Task<IActionResult> GetList()
        {
            IReadOnlyList<FineDto> list = await _fineService.GetList();
            return Ok( list );
        }

        /// <summary>
        /// Создать новый штраф.
        /// </summary>
        /// <param name="dto">Данные для создания штрафа.</param>
        /// <returns>Созданный штраф.</returns>
        [HttpPost]
        [Authorize(Roles = "Manager")]
        [SwaggerOperation( OperationId = "FineCreate", Summary = "Создать штраф" )]
        public async Task<IActionResult> Create( [FromBody] FineCreateDto dto )
        {
            int id = await _fineService.Create( dto );
            FineReadDto created = await _fineService.GetById( id );
            return CreatedAtAction( nameof( GetById ), new
            {
                id
            }, created );
        }

        /// <summary>
        /// Обновить штраф.
        /// </summary>
        /// <param name="id">Идентификатор штрафа.</param>
        /// <param name="dto">Новые данные штрафа.</param>
        /// <returns>Код 204 при успешном обновлении.</returns>
        [HttpPut( "{id:int}" )]
        [Authorize(Roles = "Manager")]
        [SwaggerOperation( OperationId = "FineUpdate", Summary = "Обновить штраф" )]
        public async Task<IActionResult> Update( int id, [FromBody] FineUpdateDto dto )
        {
            await _fineService.Update( id, dto );
            return NoContent();
        }

        /// <summary>
        /// Удалить штраф.
        /// </summary>
        /// <param name="id">Идентификатор штрафа.</param>
        /// <returns>Код 204 при успешном удалении.</returns>
        [HttpDelete( "{id:int}" )]
        [Authorize(Roles = "Manager")]
        [SwaggerOperation( OperationId = "FineDelete", Summary = "Удалить штраф" )]
        public async Task<IActionResult> Delete( int id )
        {
            await _fineService.Remove( id );
            return NoContent();
        }
    }
}