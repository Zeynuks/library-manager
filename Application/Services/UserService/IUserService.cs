using Application.DTOs;
using Domain.Entities;

namespace Application.Services.UserService
{
    public interface IUserService
    {
        Task<UserDto> GetById( int id );
        Task<IReadOnlyList<UserDto>> GetList();
        Task<int> Create( UserCreateDto dto );
        Task Update( int id, UserUpdateDto dto );
        Task Remove( int id );
        Task Block( int id );
        Task Unblock( int id );
        Task<User?> GetByLogin( string login );
    }
}