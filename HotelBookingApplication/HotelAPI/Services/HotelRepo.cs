using HotelAPI.Interfaces;
using HotelAPI.Models;
using HotelAPI.Models.DTO;
using System.Diagnostics;

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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public Hotel Update(Hotel item)
        {
            throw new NotImplementedException();
        }
    }
}
