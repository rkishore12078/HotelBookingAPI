using HotelReservation.Interfaces;
using HotelReservation.Models;
using HotelReservation.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HotelReservation.Services
{
    public class ReservationRepo : IReservation<Reservation, IdDTO>
    {
        private readonly ReservationContext _context;

        public ReservationRepo(ReservationContext context)
        {
            _context=context;
        }
        public Reservation Add(Reservation item)
        {
            try
            {
                _context.Reservations.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }

        public Reservation Delete(IdDTO item)
        {
            var reservations=_context.Reservations.ToList();
            var reservation= reservations.FirstOrDefault(r=>r.R_id==item.ID);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                _context.SaveChanges();
                return reservation;
            }
            return null;
        }

        public ICollection<Reservation> GetAll()
        {
            return _context.Reservations.ToList();
        }

        public Reservation GetValue(IdDTO item)
        {
            var reservations= _context.Reservations.ToList();
            var reservation= reservations.FirstOrDefault(r=>r.R_id == item.ID);
            if(reservation!=null)
                return reservation;
            return null;
        }

        public Reservation Update(Reservation item)
        {
            var reservations = _context.Reservations.ToList();
            var reservation = reservations.FirstOrDefault(r => r.R_id == item.R_id);
            if (reservation != null)
            {
                reservation.H_id = item.H_id != 0 ? item.H_id : reservation.H_id;
                reservation.U_id=item.U_id!= 0 ? item.U_id : reservation.U_id;
                reservation.RoomNumber = item.RoomNumber != 0 ? item.RoomNumber : reservation.RoomNumber;
                reservation.CheckInDate = item.CheckInDate!=default(DateTime)?item.CheckInDate:reservation.CheckInDate;
                reservation.CheckOutDate = item.CheckOutDate!=default(DateTime)?item.CheckOutDate:reservation.CheckOutDate;
                _context.Reservations.Update(reservation);
                _context.SaveChanges();
                return reservation;
            }
            return null;
        }
    }
}
