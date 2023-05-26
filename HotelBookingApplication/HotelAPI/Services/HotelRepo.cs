﻿using HotelAPI.Interfaces;
using HotelAPI.Models;
using HotelAPI.Models.DTO;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace HotelAPI.Services
{
    public class HotelRepo : ICrud<Hotel, IdDTO>
    {
        private readonly HotelContext _context;

        public HotelRepo(HotelContext context)
        {
            _context = context;
        }
        public Hotel Add(Hotel item)
        {
            try
            {
                _context.Hotels.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }

        public Hotel Delete(IdDTO item)
        {
            try
            {
                var rooms = _context.Rooms.ToList();
                var myRooms = rooms.Where(r => r.H_id == item.ID);
                foreach (var value in myRooms)
                {
                    _context.Rooms.Remove(value);
                    _context.SaveChanges();
                }
                var hotels = _context.Hotels.ToList();
                var myHotel = hotels.FirstOrDefault(h => h.H_id == item.ID);
                if (myHotel != null)
                {
                    _context.Hotels.Remove(myHotel);
                    _context.SaveChanges();
                    return myHotel;
                }
                else
                    return null;
            }
            catch (Exception e)
            { 
                Debug.WriteLine(e.Message);
            }
            return null;
        }

        public ICollection<Hotel> GetAll()
        {
            var hotels= _context.Hotels.ToList();
            if(hotels.Count>0)
                return hotels;
            return null;
        }

        public Hotel GetValue(IdDTO item)
        {
            var hotels=_context.Hotels.ToList();
            var hotel = hotels.SingleOrDefault(h=>h.H_id==item.ID);
            if(hotel!=null)
                return hotel;
            return null;
        }

        public Hotel Update(Hotel item)
        {
            var hotels = _context.Hotels.ToList();
            var hotel = hotels.SingleOrDefault(h => h.H_id == item.H_id);
            if (hotel != null)
            {
                hotel.City=item.City;
                hotel.Country=item.Country;
                hotel.Amenities=item.Amenities;
                _context.Hotels.Update(hotel);
                _context.SaveChanges();
                return hotel;
            }
            return null;
        }
    }
}
