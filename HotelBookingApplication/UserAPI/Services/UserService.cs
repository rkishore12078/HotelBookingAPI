using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using UserAPI.Interfaces;
using UserAPI.Models;
using UserAPI.Models.DTO;

namespace UserAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUser _userRepo;
        private readonly ITokenGenerate _tokenService;

        public UserService(IUser userRepo,ITokenGenerate tokenService)
        {
            _userRepo = userRepo;
            _tokenService= tokenService;
        }
        public UserDTO LogIN(UserDTO userDTO)
        {
            UserDTO user = null;
            var userData = _userRepo.Get(userDTO);
            if (userData != null)
            {
                var hmac = new HMACSHA512(userData.HashKey);
                var userPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
                for (int i = 0; i < userPass.Length; i++)
                {
                    if (userPass[i] != userData.Password[i])
                        return null;
                }
                user = new UserDTO();
                user.Username = userData.Username;
                user.Role = userData.Role;
                user.Token = _tokenService.GenerateToken(user);
            }
            return user;
        }

        public UserDTO Register(UserRegisterDTO userRegisterDTO)
        {
            UserDTO user = null;
            var hmac = new HMACSHA512();
            userRegisterDTO.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(userRegisterDTO.UserPassword));
            userRegisterDTO.HashKey = hmac.Key;
            var resultUser = _userRepo.Add(userRegisterDTO);
            if (resultUser != null)
            {
                user = new UserDTO();
                user.Username = resultUser.Username;
                user.Role = resultUser.Role;
                user.Token = _tokenService.GenerateToken(user);
            }
            return user;
        }

        public UserDTO Update(UserRegisterDTO user)
        {
            var users=_userRepo.GetAll();
            User myUser=users.SingleOrDefault(u=>u.Username==user.Username);
            if (myUser != null)
            {
                myUser.Name = user.Name;
                myUser.PhoneNumber = user.PhoneNumber;
                myUser.Age = user.Age;
                var hmac = new HMACSHA512();
                myUser.Password= hmac.ComputeHash(Encoding.UTF8.GetBytes(user.UserPassword));
                myUser.HashKey = hmac.Key;
                myUser.Role=user.Role;
                UserDTO userDTO = new UserDTO();
                userDTO.Username = myUser.Username;
                userDTO.Role = myUser.Role;
                userDTO.Token= _tokenService.GenerateToken(userDTO);
                var newUser=_userRepo.Update(myUser);
                if (newUser != null)
                {
                    return userDTO;
                }
                return null;
            }
            return null;
        }

        public bool Update_Password(UserDTO userDTO)
        {
            User user = new User();
            var users = _userRepo.GetAll();
            var myUser = users.SingleOrDefault(u=>u.Username==userDTO.Username);
            if (myUser != null)
            {
                user.Username=userDTO.Username;
                var hmac = new HMACSHA512();
                user.Password= hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
                user.HashKey = hmac.Key;
                user.Name = myUser.Name;
                user.Role = myUser.Role;
                user.Age = myUser.Age;
                user.PhoneNumber = myUser.PhoneNumber;
                var newUser=_userRepo.Update(user);
                if (newUser != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
