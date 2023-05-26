using HotelAPI.Interfaces;
using HotelAPI.Models;
using HotelAPI.Models.DTO;
using System.Diagnostics;

namespace HotelAPI.Services
{
    public class RoomRepo : ICrud<Room, IdDTO>
    {
        private readonly HotelContext _context;

        public RoomRepo(HotelContext context)
        {
            _context = context;
        }
        public Room Add(Room item)
        {
            try
            {
                _context.Rooms.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }

        public Room Delete(IdDTO item)
        {
            throw new NotImplementedException();
        }

        public ICollection<Room> GetAll()
        {
            var rooms = _context.Rooms.ToList();
            if (rooms.Count > 0)
                return rooms;
            return null;
        }

        public Room GetValue(IdDTO item)
        {
            throw new NotImplementedException();
        }

        public Room Update(Room item)
        {
            throw new NotImplementedException();
        }
    }
}
