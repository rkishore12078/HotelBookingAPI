using UserAPI.Models;
using UserAPI.Models.DTO;

namespace UserAPI.Interfaces
{
    public interface IUser
    {
        User Add(User user);
        User Update(User user);
        User Delete(UserDTO userDTO);
        User Get(UserDTO userDTO);
        List<User> GetAll();
    }
}
