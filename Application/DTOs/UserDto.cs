using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.DTOs
{
    public class UserDto
    {
        [Required]
        public int Id { get; init; }

        [Required]
        [StringLength( 100 )]
        public string Login { get; init; }

        [Required]
        public UserRole Role { get; init; }

        [Required]
        public bool IsBlocked { get; init; }

        public UserDto( int id, string login, UserRole role, bool isBlocked )
        {
            Id = id;
            Login = login;
            Role = role;
            IsBlocked = isBlocked;
        }
    }

    public class UserCreateDto
    {
        [Required]
        [StringLength( 100 )]
        public string Login { get; init; }

        [Required]
        [StringLength( 200, MinimumLength = 6 )]
        public string Password { get; init; }

        [Required]
        public UserRole Role { get; init; }

        public UserCreateDto( string login, string password, UserRole role )
        {
            Login = login;
            Password = password;
            Role = role;
        }
    }

    public class UserUpdateDto
    {
        [Required]
        [StringLength( 100 )]
        public string Login { get; init; }

        [Required]
        public UserRole Role { get; init; }

        public UserUpdateDto( string login, UserRole role )
        {
            Login = login;
            Role = role;
        }
    }

    public class LoginDto
    {
        [Required]
        [StringLength( 100 )]
        public string Login { get; init; }

        [Required]
        [StringLength( 200, MinimumLength = 6 )]
        public string Password { get; init; }

        public LoginDto( string login, string password )
        {
            Login = login;
            Password = password;
        }
    }
}