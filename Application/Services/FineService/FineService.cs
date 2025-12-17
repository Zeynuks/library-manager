using Application.DTOs;
using Application.Mappers;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Foundation;
using Domain.Repositories;

namespace Application.Services.FineService
{
    public class FineService : IFineService
    {
        private readonly IFineRepository _fineRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FineService( IFineRepository fineRepository, IUnitOfWork unitOfWork )
        {
            _fineRepository = fineRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<FineReadDto> GetById( int id )
        {
            Fine? fine = await _fineRepository.TryGet( id );
            if ( fine is null )
            {
                throw new DomainNotFoundException( $"Fine with ID {id} could not be found." );
            }

            return FineMapper.ToReadDto( fine );
        }

        public async Task<IReadOnlyList<FineDto>> GetList()
        {
            IReadOnlyList<Fine> fines = await _fineRepository.GetReadOnlyList();

            return fines
                .Select( FineMapper.ToDto )
                .ToList();
        }

        public async Task<int> Create( FineCreateDto dto )
        {
            Fine fine = new( dto.RentalId, dto.Description, dto.Amount );

            _fineRepository.Add( fine );
            await _unitOfWork.CommitAsync();

            return fine.Id;
        }

        public async Task Update( int id, FineUpdateDto dto )
        {
            Fine? fine = await _fineRepository.TryGet( id );
            if ( fine is null )
            {
                throw new DomainNotFoundException( $"Fine with ID {id} could not be found." );
            }

            fine.Update( dto.Description, dto.Amount );
            await _unitOfWork.CommitAsync();
        }

        public async Task Remove( int id )
        {
            Fine? fine = await _fineRepository.TryGet( id );
            if ( fine is null )
            {
                throw new DomainNotFoundException( $"Fine with ID {id} could not be found." );
            }

            _fineRepository.Delete( fine );
            await _unitOfWork.CommitAsync();
        }
    }
}