using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using UserAPI.Exceptions;
using UserAPI.Interfaces;
using UserAPI.Models;
using UserAPI.Models.DTO;

namespace UserAPI.Services
{
    public class UserRepo : IUser
    {
        private readonly UserContext _context;
        public UserRepo(UserContext context)
        {
            _context = context;
        }
        public User? Add(User user)
        {
            try
            {
                var users= _context.Users;
                var myUser = users.SingleOrDefault(u=>u.Username==user.Username);
                if (myUser == null)
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    return user;
                }
                return null;
            }
            catch(SqlException se) { throw new InvalidSqlException(se.Message); }
        }

        public User? Delete(UserDTO userDTO)
        {
            try
            {
                var users = _context.Users;
                var myUser = users.SingleOrDefault(u => u.Username == userDTO.Username);
                if (myUser != null)
                {
                    _context.Users.Remove(myUser);
                    _context.SaveChanges();
                    return myUser;
                }
                return null;
            }
            catch (SqlException se) { throw new InvalidSqlException(se.Message); }
        }

        public User? Get(UserDTO userDTO)
        {
            try
            {
                var users = GetAll();
                var user = users.FirstOrDefault(u => u.Username == userDTO.Username);
                if (user != null)
                {
                    return user;
                }
                return null;
            }
            catch (SqlException se) { throw new InvalidSqlException(se.Message); }
        }

        public List<User>? GetAll()
        {
            try
            {
                var users = _context.Users.ToList();
                if (users != null)
                    return users;
                return null;
            }
            catch (SqlException se) { throw new InvalidSqlException(se.Message); }
        }

        public User Update(User user)
        {
            try
            {
                var users = GetAll();
                var Newuser = users.FirstOrDefault(u => u.Username == user.Username);
                if (Newuser != null)
                {
                    _context.Users.Update(Newuser);
                    _context.SaveChanges();
                    return Newuser;
                }
                else
                    return null;
            }
            catch (SqlException se) { throw new InvalidSqlException(se.Message); }
        }
    }
}
