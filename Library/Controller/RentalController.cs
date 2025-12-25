using Application.DTOs;
using Application.Services.RentalService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Controller
{
    /// <summary>
    /// Операции над прокатом книги.
    /// </summary>
    [ApiController]
    [ApiExplorerSettings( GroupName = "library" )]
    [Route( "api/rentals" )]
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalController( IRentalService rentalService )
        {
            _rentalService = rentalService;
        }

        /// <summary>
        /// Получить прокат по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор проката.</param>
        /// <returns>Объект проката книги.</returns>
        [HttpGet( "{id:int}" )]
        [Authorize( Roles = "Administrator,Manager,Operator" )]
        [SwaggerOperation( OperationId = "RentalGetById", Summary = "Получить прокат по Id" )]
        public async Task<IActionResult> GetById( int id )
        {
            RentalFullDto dto = await _rentalService.GetById( id );
            return Ok( dto );
        }

        /// <summary>
        /// Получить список прокатов.
        /// </summary>
        /// <returns>Список прокатов книг.</returns>
        [HttpGet]
        [Authorize( Roles = "Administrator,Manager,Operator" )]
        [SwaggerOperation( OperationId = "RentalGetList", Summary = "Список прокатов" )]
        public async Task<IActionResult> GetList()
        {
            IReadOnlyList<RentalFullDto> list = await _rentalService.GetList();
            return Ok( list );
        }

        /// <summary>
        /// Создать новый прокат.
        /// </summary>
        /// <param name="dto">Данные для создания проката.</param>
        /// <returns>Созданный прокат книги.</returns>
        [HttpPost]
        [Authorize( Roles = "Operator" )]
        [SwaggerOperation( OperationId = "RentalCreate", Summary = "Создать прокат" )]
        public async Task<IActionResult> Create( [FromBody] RentalCreateDto dto )
        {
            int id = await _rentalService.Create( dto );
            RentalFullDto created = await _rentalService.GetById( id );
            return CreatedAtAction( nameof( GetById ), new
            {
                id
            }, created );
        }

        /// <summary>
        /// Обновить прокат.
        /// </summary>
        /// <param name="id">Идентификатор проката.</param>
        /// <param name="dto">Новые данные проката.</param>
        /// <returns>Код 204 при успешном обновлении.</returns>
        [HttpPut( "{id:int}" )]
        [Authorize( Roles = "Operator" )]
        [SwaggerOperation( OperationId = "RentalUpdate", Summary = "Обновить прокат" )]
        public async Task<IActionResult> Update( int id, [FromBody] RentalUpdateDto dto )
        {
            await _rentalService.Update( id, dto );
            return NoContent();
        }

        /// <summary>
        /// Вернуть книгу и рассчитать итоговую сумму платежа.
        /// </summary>
        /// <param name="id">Идентификатор проката.</param>
        /// <param name="dto">Данные возврата книги.</param>
        /// <returns>Сумма к оплате.</returns>
        [HttpPost( "{id:int}/return" )]
        [Authorize( Roles = "Operator" )]
        [SwaggerOperation( OperationId = "RentalReturn", Summary = "Вернуть книгу и рассчитать сумму платежа" )]
        public async Task<IActionResult> ReturnBook(
            int id,
            [FromBody] RentalReturnDto dto )
        {
            decimal totalAmount =
                await _rentalService.ReturnBook( id, dto.ActualReturnDate );

            return Ok( totalAmount );
        }

        /// <summary>
        /// Удалить прокат.
        /// </summary>
        /// <param name="id">Идентификатор проката.</param>
        /// <returns>Код 204 при успешном удалении.</returns>
        [HttpDelete( "{id:int}" )]
        [Authorize( Roles = "Manager" )]
        [SwaggerOperation( OperationId = "RentalDelete", Summary = "Удалить прокат" )]
        public async Task<IActionResult> Delete( int id )
        {
            await _rentalService.Remove( id );
            return NoContent();
        }
    }
}