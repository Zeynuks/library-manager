using Application.DTOs;
using Application.Mappers;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Foundation;
using Domain.Repositories;

namespace Application.Services.TariffService
{
    public class TariffService : ITariffService
    {
        private readonly ITariffRepository _tariffRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TariffService(
            ITariffRepository tariffRepository,
            IUnitOfWork unitOfWork )
        {
            _tariffRepository = tariffRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TariffReadDto> GetById( int id )
        {
            Tariff? tariff = await _tariffRepository.TryGet( id );
            if ( tariff is null )
            {
                throw new DomainNotFoundException( $"Tariff with ID {id} could not be found." );
            }

            return TariffMapper.ToReadDto( tariff );
        }

        public async Task<IReadOnlyList<TariffReadDto>> GetList()
        {
            IReadOnlyList<Tariff> tariffs = await _tariffRepository.GetReadOnlyList();

            return tariffs
                .Select( TariffMapper.ToReadDto )
                .ToList();
        }

        public async Task<int> Create( TariffCreateDto dto )
        {
            Tariff tariff = new( dto.Name, dto.DailyRate );

            _tariffRepository.Add( tariff );
            await _unitOfWork.CommitAsync();

            return tariff.Id;
        }

        public async Task Update( int id, TariffUpdateDto dto )
        {
            Tariff? tariff = await _tariffRepository.TryGet( id );
            if ( tariff is null )
            {
                throw new DomainNotFoundException( $"Tariff with ID {id} could not be found." );
            }

            tariff.Update( dto.Name, dto.DailyRate );
            await _unitOfWork.CommitAsync();
        }

        public async Task Remove( int id )
        {
            Tariff? tariff = await _tariffRepository.TryGet( id );
            if ( tariff is null )
            {
                throw new DomainNotFoundException( $"Tariff with ID {id} could not be found." );
            }

            _tariffRepository.Delete( tariff );
            await _unitOfWork.CommitAsync();
        }
    }
}