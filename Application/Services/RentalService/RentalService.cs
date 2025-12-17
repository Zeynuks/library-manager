using Application.DTOs;
using Application.Mappers;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Foundation;
using Domain.Repositories;

namespace Application.Services.RentalService
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IReaderRepository _readerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RentalService(
            IRentalRepository rentalRepository,
            IBookRepository bookRepository,
            IReaderRepository readerRepository,
            IUnitOfWork unitOfWork )
        {
            _rentalRepository = rentalRepository;
            _bookRepository = bookRepository;
            _readerRepository = readerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RentalFullDto> GetById( int id )
        {
            Rental? rental = await _rentalRepository.TryGet( id );
            if ( rental is null )
            {
                throw new DomainNotFoundException( $"Rental with ID {id} could not be found." );
            }

            return RentalMapper.ToFullDto( rental );
        }

        public async Task<IReadOnlyList<RentalFullDto>> GetList()
        {
            IReadOnlyList<Rental> rentals =
                await _rentalRepository.GetReadOnlyList();

            return rentals
                .Select( RentalMapper.ToFullDto )
                .ToList();
        }

        public async Task<int> Create( RentalCreateDto dto )
        {
            Book? book = await _bookRepository.TryGet( dto.BookId );
            if ( book is null )
            {
                throw new DomainNotFoundException( $"Book with ID {dto.BookId} not found." );
            }

            Reader? reader = await _readerRepository.TryGet( dto.ReaderId );
            if ( reader is null )
            {
                throw new DomainNotFoundException( $"Reader with ID {dto.ReaderId} not found." );
            }

            IReadOnlyList<Rental> rentals = await _rentalRepository.GetByBook( dto.BookId );
            if ( rentals.Any( r => r.ActualReturnDate == null ) )
            {
                throw new DomainValidationException( "Book is already rented by another reader." );
            }

            Rental rental = new(
                dto.BookId,
                dto.ReaderId,
                dto.IssueDate,
                dto.ExpectedReturnDate,
                Rental.CalculateRentalAmount(
                    dto.IssueDate,
                    dto.ExpectedReturnDate,
                    reader.Category.DiscountRate,
                    book.Tariff.DailyRate ) );

            _rentalRepository.Add( rental );
            await _unitOfWork.CommitAsync();

            return rental.Id;
        }

        public async Task Update( int id, RentalUpdateDto dto )
        {
            Book? book = await _bookRepository.TryGet( dto.BookId );
            if ( book is null )
            {
                throw new DomainNotFoundException( $"Book with ID {dto.BookId} not found." );
            }

            Reader? reader = await _readerRepository.TryGet( dto.ReaderId );
            if ( reader is null )
            {
                throw new DomainNotFoundException( $"Reader with ID {dto.ReaderId} not found." );
            }

            Rental? rental = await _rentalRepository.TryGet( id );
            if ( rental is null )
            {
                throw new DomainNotFoundException( $"Rental with ID {id} could not be found." );
            }

            rental.Update(
                dto.IssueDate,
                dto.ExpectedReturnDate,
                Rental.CalculateRentalAmount(
                    dto.IssueDate,
                    dto.ExpectedReturnDate,
                    reader.Category.DiscountRate,
                    book.Tariff.DailyRate ) );

            await _unitOfWork.CommitAsync();
        }

        public async Task ReturnBook( int id, DateOnly actualReturnDate )
        {
            Rental? rental = await _rentalRepository.TryGet( id );
            if ( rental is null )
            {
                throw new DomainNotFoundException( $"Rental with ID {id} could not be found." );
            }

            rental.ReturnBook( actualReturnDate );
            await _unitOfWork.CommitAsync();
        }

        public async Task Remove( int id )
        {
            Rental? rental = await _rentalRepository.TryGet( id );
            if ( rental is null )
            {
                throw new DomainNotFoundException( $"Rental with ID {id} could not be found." );
            }

            _rentalRepository.Delete( rental );
            await _unitOfWork.CommitAsync();
        }
    }
}