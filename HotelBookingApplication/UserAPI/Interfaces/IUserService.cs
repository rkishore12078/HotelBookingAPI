using UserAPI.Models;
using UserAPI.Models.DTO;

namespace UserAPI.Interfaces
{
    public interface IUserService
    {
        UserDTO Register(UserRegisterDTO userRegisterDTO);
        UserDTO LogIN(UserDTO userDTO);
        User Update(User user);
    }
}
