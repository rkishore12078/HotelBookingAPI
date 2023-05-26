using HotelAPI.Interfaces;
using HotelAPI.Models;
using HotelAPI.Models.DTO;

namespace HotelAPI.Services
{
    public class HotelService
    {
        private readonly ICrud<Hotel, IdDTO> _hotelRepo;
        private ICrud<Room, IdDTO> _roomRepo;

        public HotelService(ICrud<Hotel,IdDTO> hotelRepo,ICrud<Room,IdDTO> roomRepo)
        {
            _hotelRepo=hotelRepo;
            _roomRepo=roomRepo;
        }

        public Hotel AddHotel(Hotel hotel)
        {
            var myHotel = _hotelRepo.Add(hotel);
            if (myHotel != null)
                return myHotel;
            return null;
        }

        public Room AddRoom(Room room)
        {
            var hotels = _hotelRepo.GetAll();
            var hotel = hotels.FirstOrDefault(h=>h.H_id==room.H_id);
            var myRoom = _roomRepo.Add(room);
            if (myRoom != null && hotel!=null)
                return myRoom;
            return null;
        }

        public List<Hotel> GetAllHotels()
        {
            var hotels = _hotelRepo.GetAll().ToList();
            if (hotels.Count > 0)
                return hotels;
            return null;
        }

        public List<Room> GetAllRooms()
        {
            var rooms = _roomRepo.GetAll().ToList();
            if (rooms.Count > 0)
                return rooms;
            return null;
        }
    }
}
