using Expendio.DTO;
using Expendio.Models;

namespace Expendio.Mappers
{
    public static class UserMapper
    {
        public static User ToUser(this SignupUserDto user)
        {
            return new User
            {
                Email = user.Email,
                Password = user.Password,
                Name = user.Name,
            };
        }
    }
}
