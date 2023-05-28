using HotelReservation.Interfaces;
using HotelReservation.Models;
using HotelReservation.Models.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HotelReservation.Services
{
    public class ReservationRepo : IReservation<Reservation, IdDTO>
    {
        private readonly ReservationContext _context;

        //Inject the context into the Reservation repo to communicate with the database
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
            catch (SqlException se)//catch the unexpected sql exception
            {
                Debug.WriteLine(se.Message);
            }
            return null;
        }

        public Reservation Delete(IdDTO item)
        {
            try
            {
                var reservations = _context.Reservations.ToList();
                var reservation = reservations.FirstOrDefault(r => r.R_id == item.ID);
                if (reservation != null)
                {
                    _context.Reservations.Remove(reservation);
                    _context.SaveChanges();
                    return reservation;
                }
            }
            catch (SqlException se)//catch the unexpected sql exception
            {
                Debug.WriteLine(se.Message);
            }
            return null;
        }

        public ICollection<Reservation> GetAll()
        {
            try
            {
                return _context.Reservations.ToList();
            }
            catch (SqlException se)//catch the unexpected sql exception
            {
                Debug.WriteLine(se.Message);
            }
            return null;
        }

        public Reservation GetValue(IdDTO item)
        {
            try
            {
                var reservations = _context.Reservations.ToList();
                var reservation = reservations.FirstOrDefault(r => r.R_id == item.ID);
                if (reservation != null)
                    return reservation;
            }
            catch (SqlException se)//catch the unexpected sql exception
            {
                Debug.WriteLine(se.Message);
            }
            return null;
        }

        public Reservation Update(Reservation item)
        {
            try
            {
                var reservations = _context.Reservations.ToList();
                var reservation = reservations.FirstOrDefault(r => r.R_id == item.R_id);
                if (reservation != null)
                {
                    reservation.H_id = item.H_id != 0 ? item.H_id : reservation.H_id;//If the new object's hotel id is present then it will be updated else old hotel id is assigned
                    reservation.U_id = item.U_id != 0 ? item.U_id : reservation.U_id;//If the new object's User id is present then it will be updated else old User id is assigned
                    reservation.RoomNumber = item.RoomNumber != 0 ? item.RoomNumber : reservation.RoomNumber;//If the new object's RoomNumber is present then it will be updated else old RoomNumber is assigned
                    reservation.CheckInDate = DateTime.Compare(item.CheckInDate,DateTime.Now)!=0 ? item.CheckInDate.Date : reservation.CheckInDate.Date;//If the new object's CheckIn date is present then it will be updated else old checkin date is assigned
                    reservation.CheckOutDate = DateTime.Compare(item.CheckOutDate,DateTime.Now)!=0 ? item.CheckOutDate.Date : reservation.CheckOutDate.Date;//If the new object's CheckOut date is present then it will be updated else old checkout date is assigned
                    _context.Reservations.Update(reservation);
                    _context.SaveChanges();
                    return reservation;
                }
            }
            catch (SqlException se)//catch the unexpected sql exception
            {
                Debug.WriteLine(se.Message);
            }
            return null;
        }
    }
}
