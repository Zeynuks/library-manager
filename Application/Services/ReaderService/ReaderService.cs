using Application.DTOs;
using Application.Mappers;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Foundation;
using Domain.Repositories;

namespace Application.Services.ReaderService
{
    public class ReaderService : IReaderService
    {
        private readonly IReaderRepository _readerRepository;
        private readonly IReaderCategoryRepository _readerCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReaderService(
            IReaderRepository readerRepository,
            IReaderCategoryRepository readerCategoryRepository,
            IUnitOfWork unitOfWork )
        {
            _readerRepository = readerRepository;
            _readerCategoryRepository = readerCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ReaderWithCategoryDto> GetById( int id )
        {
            Reader? reader = await _readerRepository.TryGet( id );
            if ( reader is null )
            {
                throw new DomainNotFoundException( $"Reader with ID {id} could not be found." );
            }

            return ReaderMapper.ToWithCategoryDto( reader );
        }

        public async Task<ReaderWithRentalsDto> GetWithRentals( int id )
        {
            Reader? reader = await _readerRepository.TryGetWithRentails( id );
            if ( reader is null )
            {
                throw new DomainNotFoundException( $"Reader with ID {id} could not be found." );
            }

            return ReaderMapper.ToWithRentalsDto( reader );
        }

        public async Task<IReadOnlyList<ReaderWithCategoryDto>> GetList()
        {
            IReadOnlyList<Reader> readers =
                await _readerRepository.GetReadOnlyList();

            return readers
                .Select( ReaderMapper.ToWithCategoryDto )
                .ToList();
        }

        public async Task<int> Create( ReaderCreateDto dto )
        {
            ReaderCategory? category = await _readerCategoryRepository.TryGet( dto.CategoryId );
            if ( category is null )
            {
                throw new DomainNotFoundException( $"Category with ID {dto.CategoryId} not found." );
            }

            Reader reader = new(
                dto.FirstName,
                dto.MiddleName,
                dto.LastName,
                dto.Address,
                dto.PhoneNumber,
                dto.CategoryId );

            _readerRepository.Add( reader );
            await _unitOfWork.CommitAsync();

            return reader.Id;
        }

        public async Task Update( int id, ReaderUpdateDto dto )
        {
            Reader? reader = await _readerRepository.TryGet( id );
            if ( reader is null )
            {
                throw new DomainNotFoundException( $"Reader with ID {id} could not be found." );
            }

            reader.Update(
                dto.FirstName,
                dto.MiddleName,
                dto.LastName,
                dto.Address,
                dto.PhoneNumber,
                reader.CategoryId );

            await _unitOfWork.CommitAsync();
        }

        public async Task Remove( int id )
        {
            Reader? reader = await _readerRepository.TryGetWithRentails( id );
            if ( reader is null )
            {
                throw new DomainNotFoundException( $"Reader with ID {id} could not be found." );
            }

            if ( reader.Rentals.Any( r => r.ActualReturnDate == null ) )
            {
                throw new DomainValidationException( "Reader have opened rentals." );
            }

            _readerRepository.Delete( reader );
            await _unitOfWork.CommitAsync();
        }
    }
}