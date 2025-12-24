using Application.DTOs;
using Application.Mappers;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Foundation;
using Domain.Repositories;

namespace Application.Services.RentalService
{
    //TODO Добавить логику расчета и отдельный метод предрасчёта
    //TODO Добавить логику расчёта для штрафов + просрочка
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
                CalculateRentalAmount(
                    issueDate: dto.IssueDate,
                    expectedReturnDate: dto.ExpectedReturnDate,
                    dailyRate: book.Tariff.DailyRate,
                    discountRate: reader.Category.DiscountRate
                ) );

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

            rental.Update( dto.ExpectedReturnDate, CalculateRentalAmount(
                    issueDate: dto.IssueDate,
                    expectedReturnDate: dto.ExpectedReturnDate,
                    dailyRate: book.Tariff.DailyRate,
                    discountRate: reader.Category.DiscountRate
                )
            );

            await _unitOfWork.CommitAsync();
        }

        public async Task<decimal> ReturnBook( int id, DateOnly actualReturnDate )
        {
            Rental? rental = await _rentalRepository.TryGet( id );
            if ( rental is null )
            {
                throw new DomainNotFoundException( $"Rental with ID {id} could not be found." );
            }

            Book? book = await _bookRepository.TryGet( rental.BookId );
            if ( book is null )
            {
                throw new DomainNotFoundException( $"Book with ID {rental.BookId} not found." );
            }

            decimal overdueAmount = CalculateOverdueAmount(
                rental.ExpectedReturnDate,
                actualReturnDate,
                book.Tariff.DailyRate );

            decimal finesAmount = rental.Fines?.Sum( f => f.Amount ) ?? 0m;

            rental.ReturnBook( actualReturnDate, overdueAmount + finesAmount );
            await _unitOfWork.CommitAsync();

            return overdueAmount + finesAmount;
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

        public static decimal CalculateRentalAmount(
            DateOnly issueDate,
            DateOnly expectedReturnDate,
            decimal dailyRate,
            decimal discountRate )
        {
            int rentalDays = Math.Max( 1, expectedReturnDate.DayNumber - issueDate.DayNumber + 1 );

            return rentalDays * dailyRate * ( 1 - discountRate );
        }

        public decimal CalculateOverdueAmount(
            DateOnly expectedReturnDate,
            DateOnly actualDate,
            decimal dailyRate )
        {
            if ( actualDate <= expectedReturnDate )
            {
                return 0m;
            }

            int overdueDays = actualDate.DayNumber - expectedReturnDate.DayNumber;

            if ( overdueDays <= 0 )
            {
                return 0m;
            }

            return overdueDays * dailyRate;
        }
    }
}