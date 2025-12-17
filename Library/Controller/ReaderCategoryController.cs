using Application.DTOs;
using Application.Services.ReaderCategoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Controller
{
    /// <summary>
    /// Операции над категорией читателя.
    /// </summary>
    [ApiController]
    [ApiExplorerSettings( GroupName = "library" )]
    [Route( "api/reader-categories" )]
    public class ReaderCategoryController : ControllerBase
    {
        private readonly IReaderCategoryService _categoryService;

        public ReaderCategoryController( IReaderCategoryService categoryService )
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Получить категорию читателя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <returns>Объект категории читателя.</returns>
        [HttpGet( "{id:int}" )]
        [Authorize(Roles = "Administrator,Manager,Operator")]
        [SwaggerOperation( OperationId = "ReaderCategoryGetById", Summary = "Получить категорию читателя по Id" )]
        public async Task<IActionResult> GetById( int id )
        {
            ReaderCategoryReadDto dto = await _categoryService.GetById( id );
            return Ok( dto );
        }

        /// <summary>
        /// Получить список категорий читателей.
        /// </summary>
        /// <returns>Список категорий читателей.</returns>
        [HttpGet]
        [Authorize(Roles = "Administrator,Manager,Operator")]
        [SwaggerOperation( OperationId = "ReaderCategoryGetList", Summary = "Список категорий читателей" )]
        public async Task<IActionResult> GetList()
        {
            IReadOnlyList<ReaderCategoryDto> list = await _categoryService.GetList();
            return Ok( list );
        }

        /// <summary>
        /// Создать категорию читателя.
        /// </summary>
        /// <param name="dto">Данные для создания категории.</param>
        /// <returns>Созданная категория читателя.</returns>
        [HttpPost]
        [Authorize(Roles = "Manager")]
        [SwaggerOperation( OperationId = "ReaderCategoryCreate", Summary = "Создать категорию читателя" )]
        public async Task<IActionResult> Create( [FromBody] ReaderCategoryCreateDto dto )
        {
            int id = await _categoryService.Create( dto );
            ReaderCategoryReadDto created = await _categoryService.GetById( id );
            return CreatedAtAction( nameof( GetById ), new
            {
                id
            }, created );
        }

        /// <summary>
        /// Обновить категорию читателя.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="dto">Новые данные категории.</param>
        /// <returns>Код 204 при успешном обновлении.</returns>
        [HttpPut( "{id:int}" )]
        [Authorize(Roles = "Manager")]
        [SwaggerOperation( OperationId = "ReaderCategoryUpdate", Summary = "Обновить категорию читателя" )]
        public async Task<IActionResult> Update( int id, [FromBody] ReaderCategoryUpdateDto dto )
        {
            await _categoryService.Update( id, dto );
            return NoContent();
        }

        /// <summary>
        /// Удалить категорию читателя.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <returns>Код 204 при успешном удалении.</returns>
        [HttpDelete( "{id:int}" )]
        [Authorize(Roles = "Manager")]
        [SwaggerOperation( OperationId = "ReaderCategoryDelete", Summary = "Удалить категорию читателя" )]
        public async Task<IActionResult> Delete( int id )
        {
            await _categoryService.Remove( id );
            return NoContent();
        }
    }
}