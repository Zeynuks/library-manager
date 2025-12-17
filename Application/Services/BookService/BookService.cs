using Application.DTOs;
using Application.Mappers;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Foundation;
using Domain.Repositories;

namespace Application.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ITariffRepository _tariffRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookService(
            IBookRepository bookRepository,
            ITariffRepository tariffRepository,
            IUnitOfWork unitOfWork )
        {
            _bookRepository = bookRepository;
            _tariffRepository = tariffRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BookWithTariffDto> GetById( int id )
        {
            Book? book = await _bookRepository.TryGet( id );
            if ( book is null )
            {
                throw new DomainNotFoundException( $"Book with ID {id} could not be found." );
            }

            return BookMapper.ToWithTariffDto( book );
        }

        public async Task<BookWithRentalsDto> GetWithRentals( int id )
        {
            Book? book = await _bookRepository.TryGetWithRentals( id );
            if ( book is null )
            {
                throw new DomainNotFoundException( $"Book with ID {id} could not be found." );
            }

            return BookMapper.ToWithRentalsDto( book );
        }

        public async Task<IReadOnlyList<BookDto>> GetList()
        {
            IReadOnlyList<Book> books = await _bookRepository.GetReadOnlyList();

            return books
                .Select( BookMapper.ToDto )
                .ToList();
        }

        public async Task<int> Create( BookCreateDto dto )
        {
            Tariff? tariff = await _tariffRepository.TryGet( dto.TariffId );
            if ( tariff is null )
            {
                throw new DomainNotFoundException( $"Tariff with ID {dto.TariffId} not found." );
            }

            Book book = new(
                dto.Title,
                dto.Author,
                dto.Genre,
                dto.Deposit,
                dto.TariffId );

            _bookRepository.Add( book );
            await _unitOfWork.CommitAsync();

            return book.Id;
        }

        public async Task Update( int id, BookUpdateDto dto )
        {
            Book? book = await _bookRepository.TryGet( id );
            if ( book is null )
            {
                throw new DomainNotFoundException( $"Book with ID {id} could not be found." );
            }

            Tariff? tariff = await _tariffRepository.TryGet( dto.TariffId );
            if ( tariff is null )
            {
                throw new DomainNotFoundException( $"Tariff with ID {dto.TariffId} not found." );
            }

            book.Update(
                dto.Title,
                dto.Author,
                dto.Genre,
                dto.Deposit,
                dto.TariffId );

            await _unitOfWork.CommitAsync();
        }

        public async Task Remove( int id )
        {
            Book? book = await _bookRepository.TryGet( id );
            if ( book is null )
            {
                throw new DomainNotFoundException( $"Book with ID {id} not be found." );
            }

            _bookRepository.Delete( book );
            await _unitOfWork.CommitAsync();
        }
    }
}