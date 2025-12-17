using Application.DTOs;
using Application.Mappers;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Foundation;
using Domain.Repositories;

namespace Application.Services.ReaderCategoryService
{
    public class ReaderCategoryService : IReaderCategoryService
    {
        private readonly IReaderCategoryRepository _readerCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReaderCategoryService(
            IReaderCategoryRepository readerCategoryRepository,
            IUnitOfWork unitOfWork )
        {
            _readerCategoryRepository = readerCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ReaderCategoryReadDto> GetById( int id )
        {
            ReaderCategory? category = await _readerCategoryRepository.TryGet( id );
            if ( category is null )
            {
                throw new DomainNotFoundException( $"ReaderCategory with ID {id} could not be found." );
            }

            return ReaderCategoryMapper.ToReadDto( category );
        }

        public async Task<IReadOnlyList<ReaderCategoryDto>> GetList()
        {
            IReadOnlyList<ReaderCategory> categories = await _readerCategoryRepository.GetReadOnlyList();

            return categories
                .Select( ReaderCategoryMapper.ToDto )
                .ToList();
        }

        public async Task<int> Create( ReaderCategoryCreateDto dto )
        {
            ReaderCategory category = new( dto.Name, dto.DiscountRate );

            _readerCategoryRepository.Add( category );
            await _unitOfWork.CommitAsync();

            return category.Id;
        }

        public async Task Update( int id, ReaderCategoryUpdateDto dto )
        {
            ReaderCategory? category = await _readerCategoryRepository.TryGet( id );
            if ( category is null )
            {
                throw new DomainNotFoundException( $"Reader Category with ID {id} could not be found." );
            }

            category.Update( dto.Name, dto.DiscountRate );
            await _unitOfWork.CommitAsync();
        }

        public async Task Remove( int id )
        {
            ReaderCategory? category = await _readerCategoryRepository.TryGet( id );
            if ( category is null )
            {
                throw new DomainNotFoundException( $"Reader Category with ID {id} could not be found." );
            }

            _readerCategoryRepository.Delete( category );
            await _unitOfWork.CommitAsync();
        }
    }
}