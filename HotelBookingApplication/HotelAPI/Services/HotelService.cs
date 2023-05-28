using HotelAPI.Exceptions;
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
            var hotels = _hotelRepo.GetAll().ToList();
            var newHotel = hotels.SingleOrDefault(h=>h.H_id==hotel.H_id);
            if (newHotel == null)
            {
                var myHotel = _hotelRepo.Add(hotel);
                if (myHotel != null)
                    return myHotel;
            }
            return null;
        }

        public List<Hotel> GetAllHotels()
        {
            var hotels = _hotelRepo.GetAll().ToList();
            if (hotels!=null)
                return hotels;
            return null;
        }

        //Get a Particular Hotel
        public Hotel GetHotel(IdDTO idDTO)
        {
            var hotel = _hotelRepo.GetValue(idDTO);
            if (hotel != null)
                return hotel;
            return null;
        }

        //Delete a Hotel
        public Hotel DeleteHotel(IdDTO idDTO)
        {
            var hotel=_hotelRepo.Delete(idDTO);
            if(hotel!=null)
                return hotel;
            return null;
        }

        //Update a Hotel
        public Hotel UpdateHotel(Hotel hotel)
        {
            var myHotel = _hotelRepo.Update(hotel);
            if (myHotel != null)
                return myHotel;
            return null;
        }

        //Adding a Room
        public Room AddRoom(Room room)
        {
            var hotels = _hotelRepo.GetAll();
            if (hotels != null)
            {
                var hotel = hotels.FirstOrDefault(h => h.H_id == room.H_id);
                if (hotel == null)
                    throw new HotelDoesn_tExist();
                var rooms = _roomRepo.GetAll();
                var newRoom = rooms.SingleOrDefault(r => r.R_id == room.R_id);
                if (newRoom == null)
                {
                    var myRoom = _roomRepo.Add(room);
                    if (myRoom != null)
                       return myRoom;
                }
                return null;
            }
            return null;
        }

        //Getting All Rooms
        public List<Room> GetAllRooms()
        {
            var rooms = _roomRepo.GetAll().ToList();
            if (rooms.Count > 0)
                return rooms;
            return null;
        }

        //Get a Particular Room
        public Room GetRoom(IdDTO idDTO)
        {
            var room = _roomRepo.GetValue(idDTO);
            if (room != null)
                return room;
            return null;
        }

        //Delete a Room
        public Room DeleteRoom(IdDTO idDTO)
        {
            var room = _roomRepo.Delete(idDTO);
            if (room != null)
                return room;
            return null;
        }

        //Update a Room
        public Room UpdateRoom(Room room)
        {
            var myRoom = _roomRepo.Update(room);
            if (myRoom != null)
                return myRoom;
            return null;
        }

        //Filter the Rooms by Hotel
        public List<Room> Room_By_Hotel(IdDTO idDTO)
        {
            var rooms=_roomRepo.GetAll().ToList();
            var myRooms = rooms.Where(r => r.H_id == idDTO.ID).ToList();
            if (myRooms.Count > 0)
                return myRooms;
            return null;
        }

        //Filter the Hotels by City
        public List<Hotel> Hotel_By_City(HotelFilterDTO hotelFilterDTO)
        {
            var hotels = _hotelRepo.GetAll().ToList();
            var myHotels = hotels.Where(h=>h.City==hotelFilterDTO.city).ToList();
            if (myHotels.Count > 0)
                return myHotels;
            return null;
        }

        //Filter the Hotels by Country
        public List<Hotel> Hotel_By_Country(HotelFilterDTO hotelFilterDTO)
        {
            var hotels = _hotelRepo.GetAll().ToList();
            var myHotels = hotels.Where(h => h.Country == hotelFilterDTO.country).ToList();
            if (myHotels.Count > 0)
                return myHotels;
            return null;
        }

        //Filter the Hotels by Amenities
        public List<Hotel> Hotel_By_Amenity(HotelFilterDTO hotelFilterDTO)
        {
            var hotels = _hotelRepo.GetAll().ToList();
            var myHotels = hotels.Where(h => h.Amenities == hotelFilterDTO.Amenities).ToList();
            if (myHotels.Count > 0)
                return myHotels;
            return null;
        }

        //Filter the Rooms by Price Range
        public List<Room> Room_By_Price(PriceDTO priceDTO)
        {
            var rooms = _roomRepo.GetAll().ToList();
            if (rooms.FirstOrDefault(r => r.H_id == priceDTO.H_Id) == null)
                return null;
            if(priceDTO.MinValue>=priceDTO.MazValue)
                throw new InvalidPriceRange();
            var myRooms = rooms.Where(r => r.H_id == priceDTO.H_Id && r.Price >= priceDTO.MinValue && r.Price <= priceDTO.MazValue);
            if (myRooms != null)
                return myRooms.ToList();
            return null;
                
        }

        //Filter the Rooms by Capacity
        public List<Room> Room_By_Capacity(CapacityAndTypeDTO capacityAndTypeDTO)
        {
            var rooms = _roomRepo.GetAll().ToList();
            var myRooms = rooms.Where(r => r.H_id == capacityAndTypeDTO.H_id && r.Capacity == capacityAndTypeDTO.Capacity).ToList();
            if (myRooms.Count > 0)
                return myRooms;
            return null;
        }

        //Filter the Rooms by Capacity
        public List<Room> Room_By_Type(CapacityAndTypeDTO capacityAndTypeDTO)
        {
            var rooms = _roomRepo.GetAll().ToList();
            var myRooms = rooms.Where(r => r.H_id == capacityAndTypeDTO.H_id && r.Type == capacityAndTypeDTO.Type).ToList();
            if (myRooms.Count > 0)
                return myRooms;
            return null;
        }

        //Rooms available for every Hotel
        public int Rooms_Count(IdDTO idDTO)
        {
            var hotels = _hotelRepo.GetAll().ToList();
            int roomsCount = hotels.Where(h => h.H_id==idDTO.ID).Count();
            return roomsCount;
        }


        public List<HotelAndRooms> Total_Hotels_Rooms()
        {
            List<HotelAndRooms> hotelAndRooms1 = new List<HotelAndRooms>();
            var rooms=_roomRepo.GetAll().ToList();
            if (rooms != null)
            {
                var myRooms = rooms.GroupBy(r=>r.H_id);
                foreach (var room in myRooms)
                {
                    HotelAndRooms hotelAndRooms = new HotelAndRooms();
                    hotelAndRooms.HotelId = room.Key;
                    hotelAndRooms.RoomsCount = room.Count();
                    hotelAndRooms1.Add(hotelAndRooms);
                }
                return hotelAndRooms1;
            }
            return null;
        }
    }

}
