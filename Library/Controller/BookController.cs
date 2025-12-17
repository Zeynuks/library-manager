using Application.DTOs;
using Application.Services.BookService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Controller
{
    /// <summary>
    /// Операции над книгой.
    /// </summary>
    [ApiController]
    [ApiExplorerSettings( GroupName = "library" )]
    [Route( "api/books" )]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController( IBookService bookService )
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Получить книгу по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор книги.</param>
        /// <returns>Объект книги.</returns>
        [HttpGet( "{id:int}" )]
        [Authorize(Roles = "Administrator,Manager,Operator")]
        [SwaggerOperation( OperationId = "BookGetById", Summary = "Получить книгу по Id" )]
        public async Task<IActionResult> GetById( int id )
        {
            BookWithTariffDto dto = await _bookService.GetById( id );
            return Ok( dto );
        }

        /// <summary>
        /// Получить список прокатов по книге.
        /// </summary>
        /// <param name="id">Идентификатор книги.</param>
        /// <returns>Список прокатов заданной книги.</returns>
        [HttpGet( "{id:int}/rentails" )]
        [Authorize(Roles = "Administrator,Manager,Operator")]
        [SwaggerOperation( OperationId = "BookGetWithRentals", Summary = "Список прокатов по книге" )]
        public async Task<IActionResult> GetByBook( int id )
        {
            BookWithRentalsDto rentals = await _bookService.GetWithRentals( id );
            return Ok( rentals );
        }

        /// <summary>
        /// Получить список книг.
        /// </summary>
        /// <returns>Список книг.</returns>
        [HttpGet]
        [Authorize(Roles = "Administrator,Manager,Operator")]
        [SwaggerOperation( OperationId = "BookGetList", Summary = "Список книг" )]
        public async Task<IActionResult> GetList()
        {
            IReadOnlyList<BookDto> list = await _bookService.GetList();
            return Ok( list );
        }

        /// <summary>
        /// Создать новую книгу.
        /// </summary>
        /// <param name="dto">Данные для создания книги.</param>
        /// <returns>Созданная книга.</returns>
        [HttpPost]
        [Authorize(Roles = "Manager")]
        [SwaggerOperation( OperationId = "BookCreate", Summary = "Создать книгу" )]
        public async Task<IActionResult> Create( [FromBody] BookCreateDto dto )
        {
            int id = await _bookService.Create( dto );
            BookWithTariffDto created = await _bookService.GetById( id );
            return CreatedAtAction( nameof( GetById ), new
            {
                id
            }, created );
        }

        /// <summary>
        /// Обновить книгу.
        /// </summary>
        /// <param name="id">Идентификатор книги.</param>
        /// <param name="dto">Новые данные книги.</param>
        /// <returns>Код 204 при успешном обновлении.</returns>
        [HttpPut( "{id:int}" )]
        [Authorize(Roles = "Manager")]
        [SwaggerOperation( OperationId = "BookUpdate", Summary = "Обновить книгу" )]
        public async Task<IActionResult> Update( int id, [FromBody] BookUpdateDto dto )
        {
            await _bookService.Update( id, dto );
            return NoContent();
        }

        /// <summary>
        /// Удалить книгу.
        /// </summary>
        /// <param name="id">Идентификатор книги.</param>
        /// <returns>Код 204 при успешном удалении.</returns>
        [HttpDelete( "{id:int}" )]
        [Authorize(Roles = "Manager")]
        [SwaggerOperation( OperationId = "BookDelete", Summary = "Удалить книгу" )]
        public async Task<IActionResult> Delete( int id )
        {
            await _bookService.Remove( id );
            return NoContent();
        }
    }
}