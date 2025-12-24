using Application.DTOs;
using Application.Services.ReaderService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Controller
{
    /// <summary>
    /// Операции над читателем.
    /// </summary>
    [ApiController]
    [ApiExplorerSettings( GroupName = "library" )]
    [Route( "api/readers" )]
    public class ReaderController : ControllerBase
    {
        private readonly IReaderService _readerService;

        public ReaderController( IReaderService readerService )
        {
            _readerService = readerService;
        }

        /// <summary>
        /// Получить читателя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор читателя.</param>
        /// <returns>Объект читателя.</returns>
        [HttpGet( "{id:int}" )]
        [Authorize( Roles = "Administrator,Manager,Operator" )]
        [SwaggerOperation( OperationId = "ReaderGetById", Summary = "Получить читателя по Id" )]
        public async Task<IActionResult> GetById( int id )
        {
            ReaderWithCategoryDto dto = await _readerService.GetById( id );
            return Ok( dto );
        }

        /// <summary>
        /// Получить список прокатов по читателю.
        /// </summary>
        /// <param name="id">Идентификатор читателя.</param>
        /// <returns>Список прокатов заданного читателя.</returns>
        [HttpGet( "{id:int}/rentals" )]
        [Authorize( Roles = "Administrator,Manager,Operator" )]
        [SwaggerOperation( OperationId = "ReaderGetWithRentals", Summary = "Список прокатов по читателю" )]
        public async Task<IActionResult> GetWithRentals( int id )
        {
            ReaderWithRentalsDto dto = await _readerService.GetWithRentals( id );
            return Ok( dto );
        }

        /// <summary>
        /// Получить список читателей.
        /// </summary>
        /// <returns>Список читателей.</returns>
        [HttpGet]
        [Authorize( Roles = "Administrator,Manager,Operator" )]
        [SwaggerOperation( OperationId = "ReaderGetList", Summary = "Список читателей" )]
        public async Task<IActionResult> GetList()
        {
            IReadOnlyList<ReaderWithCategoryDto> list = await _readerService.GetList();

            return Ok( list );
        }

        /// <summary>
        /// Создать нового читателя.
        /// </summary>
        /// <param name="dto">Данные для создания читателя.</param>
        /// <returns>Созданный читатель.</returns>
        [HttpPost]
        [Authorize( Roles = "Operator" )]
        [SwaggerOperation( OperationId = "ReaderCreate", Summary = "Создать читателя" )]
        public async Task<IActionResult> Create( [FromBody] ReaderCreateDto dto )
        {
            int id = await _readerService.Create( dto );
            ReaderWithCategoryDto created = await _readerService.GetById( id );

            return CreatedAtAction(
                nameof( GetById ),
                new
                {
                    id
                },
                created );
        }

        /// <summary>
        /// Обновить данные читателя.
        /// </summary>
        /// <param name="id">Идентификатор читателя.</param>
        /// <param name="dto">Новые данные читателя.</param>
        /// <returns>Код 204 при успешном обновлении.</returns>
        [HttpPut( "{id:int}" )]
        [Authorize( Roles = "Operator" )]
        [SwaggerOperation( OperationId = "ReaderUpdate", Summary = "Обновить читателя" )]
        public async Task<IActionResult> Update( int id, [FromBody] ReaderUpdateDto dto )
        {
            await _readerService.Update( id, dto );
            return NoContent();
        }

        /// <summary>
        /// Удалить читателя.
        /// </summary>
        /// <param name="id">Идентификатор читателя.</param>
        /// <returns>Код 204 при успешном удалении.</returns>
        [HttpDelete( "{id:int}" )]
        [Authorize( Roles = "Operator" )]
        [SwaggerOperation( OperationId = "ReaderDelete", Summary = "Удалить читателя" )]
        public async Task<IActionResult> Delete( int id )
        {
            await _readerService.Remove( id );
            return NoContent();
        }
    }
}