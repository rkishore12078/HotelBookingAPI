using HotelReservation.Interfaces;
using HotelReservation.Models;
using HotelReservation.Models.DTO;

namespace HotelReservation.Services
{
    public class ReservationService
    {
        private readonly IReservation<Reservation, IdDTO> _reservationRepo;

        public ReservationService(IReservation<Reservation,IdDTO> productRepo)
        {
            _reservationRepo = productRepo;
        }
        public Reservation Add(Reservation reservation)
        {
            var reservations= _reservationRepo.GetAll();
            var myReservations = reservations.Where(R=>R.H_id==reservation.H_id && R.RoomNumber==reservation.RoomNumber).ToList();
            if (myReservations.Count()>0)
            {
                var newReservation = myReservations.SingleOrDefault(R=>R.CheckInDate<=reservation.CheckInDate && R.CheckOutDate>=reservation.CheckInDate);
                if (newReservation != null)
                    return null;
            }
            var reservationObj= _reservationRepo.Add(reservation);
            if(reservationObj!=null)
                return reservationObj;
            return null;
        }

        public Reservation Delete(IdDTO idDTO)
        {
            var reservation=_reservationRepo.Delete(idDTO);
            if (reservation != null)
                return reservation;
            return null;
        }

        public Reservation GetReservation(IdDTO idDTO)
        {
            var reservation = _reservationRepo.GetValue(idDTO);
            if (reservation != null)
                return reservation;
            return null;
        }

        public Reservation Update(Reservation reservation)
        {
            var newReservation = _reservationRepo.Update(reservation);
            if (newReservation != null)
                return newReservation;
            return null;
        }

        public List<Reservation> GetAll()
        {
            var reservartions= _reservationRepo.GetAll();
            if (reservartions != null)
                return reservartions.ToList();
            return null;
        }

        public int BookedRoomsCount(IdDTO idDTO)
        {
            var reservations = _reservationRepo.GetAll().ToList();
            var bookedRooms = reservations.Where(R => R.H_id == idDTO.ID).ToList();
            int count = bookedRooms.Count();
            if (count > 0)
                return count;
            return 0;
        }

        public bool CheckAvailability(AvailabilityChecking availabilityChecking)
        {
            var reservations= _reservationRepo.GetAll().ToList();
            var reservation = reservations.SingleOrDefault(R=>R.H_id== availabilityChecking.H_id
                                                   && R.RoomNumber==availabilityChecking.RoomNumber
                                                   && R.CheckInDate<=availabilityChecking.CheckInDate
                                                   && R.CheckOutDate>=availabilityChecking.CheckInDate);
            if(reservations!=null)
                return false;
            return true;
        }
    }
}
