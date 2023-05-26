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
            var rooms = _context.Rooms.ToList();
            var myRoom = rooms.SingleOrDefault(r=>r.R_id==item.ID);
            if (myRoom != null)
            {
                _context.Rooms.Remove(myRoom);
                _context.SaveChanges();
                return myRoom;
            }
            else
                return null;
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
            var rooms=_context.Rooms.ToList();
            var room = rooms.SingleOrDefault(r=>r.R_id==item.ID);
            if(room!=null)
                return room;
            return null;
        }

        public Room Update(Room item)
        {
            var rooms = _context.Rooms.ToList();
            var room = rooms.SingleOrDefault(r => r.R_id == item.R_id);
            if (room != null)
            {
                _context.Rooms.Update(item);
                _context.SaveChanges();
                return room;
            }
            return null;
        }
    }
}
