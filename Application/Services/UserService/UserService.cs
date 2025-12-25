using Application.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Foundation;
using Domain.Repositories;

namespace Application.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService( IUserRepository userRepository, IUnitOfWork unitOfWork )
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto> GetById( int id )
        {
            User? user = await _userRepository.TryGet( id );
            if ( user is null )
            {
                throw new DomainNotFoundException( $"User with ID {id} not found." );
            }

            return ToDto( user );
        }

        public async Task<IReadOnlyList<UserDto>> GetList()
        {
            IReadOnlyList<User> users = await _userRepository.GetReadOnlyList();

            return users.Select( ToDto ).ToList();
        }

        public async Task<int> Create( UserCreateDto dto )
        {
            if ( await _userRepository.Exists( dto.Login ) )
            {
                throw new DomainValidationException( $"Login {dto.Login} is already taken." );
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword( dto.Password );

            if (!Enum.TryParse(dto.Role, true, out UserRole role))
            {
                throw new ArgumentException($"Invalid role value: {dto.Role}");
            }
            
            User user = new( dto.Login, passwordHash, role );
            _userRepository.Add( user );
            await _unitOfWork.CommitAsync();

            return user.Id;
        }

        public async Task Update( int id, UserUpdateDto dto )
        {
            User? user = await _userRepository.TryGet( id );
            if ( user is null )
            {
                throw new DomainNotFoundException( $"User with ID {id} not found." );
            }
            
            if (!Enum.TryParse(dto.Role, true, out UserRole role))
            {
                throw new ArgumentException($"Invalid role value: {dto.Role}");
            }

            user.Update(dto.Login, role);
            await _unitOfWork.CommitAsync();
        }

        public async Task Remove( int id )
        {
            User? user = await _userRepository.TryGet( id );
            if ( user is null )
            {
                throw new DomainNotFoundException( $"User with ID {id} not found." );
            }

            _userRepository.Delete( user );
            await _unitOfWork.CommitAsync();
        }

        public async Task Block( int id )
        {
            User? user = await _userRepository.TryGet( id );
            if ( user is null )
            {
                throw new DomainNotFoundException( $"User with ID {id} not found." );
            }

            user.Block();
            await _unitOfWork.CommitAsync();
        }

        public async Task Unblock( int id )
        {
            User? user = await _userRepository.TryGet( id );
            if ( user is null )
            {
                throw new DomainNotFoundException( $"User with ID {id} not found." );
            }

            user.Unblock();
            await _unitOfWork.CommitAsync();
        }

        public async Task<User?> GetByLogin( string login )
        {
            return await _userRepository.TryGetByLogin( login );
        }

        private static UserDto ToDto( User user )
        {
            return new UserDto( user.Id, user.Login, user.Role.ToString(), user.IsBlocked );
        }
    }
}