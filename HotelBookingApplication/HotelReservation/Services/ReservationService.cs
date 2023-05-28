using HotelReservation.Interfaces;
using HotelReservation.Models;
using HotelReservation.Models.DTO;

namespace HotelReservation.Services
{
    public class ReservationService
    {
        private readonly IReservation<Reservation, IdDTO> _reservationRepo;

        //Inject the Reservation Repo into the service
        public ReservationService(IReservation<Reservation,IdDTO> productRepo)
        {
            _reservationRepo = productRepo;
        }
        public Reservation Add(Reservation reservation)
        {
            var reservations= _reservationRepo.GetAll();//Take all objects in the database
            var myReservations = reservations.Where(R=>R.H_id==reservation.H_id && R.RoomNumber==reservation.RoomNumber).ToList();//fetch objects if hotel id and room number is matched
            if (myReservations!=null)
            {
                var newReservation = myReservations.SingleOrDefault(R=>R.CheckInDate.Date <=reservation.CheckInDate.Date && R.CheckOutDate.Date >=reservation.CheckInDate.Date);//check if the entered check in date id lie between old checkin and check out date
                if (newReservation != null)
                    return null;
            }
            reservation.CheckInDate = reservation.CheckInDate.Date;//isolate only date from datetime
            reservation.CheckOutDate= reservation.CheckOutDate.Date;//isolate only date from datetime
            var reservationObj= _reservationRepo.Add(reservation);//Calling Reservation Repo
            if(reservationObj!=null)
                return reservationObj;
            return null;
        }

        public Reservation Delete(IdDTO idDTO)
        {
            var reservations= _reservationRepo.GetAll();//Take all objects in the database
            var myReservation = reservations.SingleOrDefault(R=>R.R_id==idDTO.ID);//Check if the given reservation id is present in the database
            if (myReservation != null)
            {
                var reservation = _reservationRepo.Delete(idDTO);
                if (reservation != null)
                    return reservation;
            }
            return null;
        }

        public Reservation GetReservation(IdDTO idDTO)
        {
            var reservations = _reservationRepo.GetAll();//Take all objects in the database
            var myReservation = reservations.SingleOrDefault(R => R.R_id == idDTO.ID);//Check if the given reservation id is present in the database
            if (myReservation != null)
            {
                var reservation = _reservationRepo.GetValue(idDTO);
                if (reservation != null)
                    return reservation;
            }
            return null;
        }

        public Reservation Update(Reservation reservation)
        {
            var reservations = _reservationRepo.GetAll();//Take all objects in the database
            var myReservation = reservations.SingleOrDefault(R => R.R_id == reservation.R_id);//Check if the given reservation id is present in the database
            if (myReservation != null)
            {
                var newReservation = _reservationRepo.Update(reservation);
                if (newReservation != null)
                    return newReservation;
            }
            return null;
        }

        public List<Reservation> GetAll()
        {
            var reservartions= _reservationRepo.GetAll();//Take all objects in the database
            if (reservartions != null)
                return reservartions.ToList();
            return null;
        }

        public int BookedRoomsCount(IdDTO idDTO)
        {
            var reservations = _reservationRepo.GetAll().ToList();//Take all objects in the database
            var bookedRooms = reservations.Where(R => R.H_id == idDTO.ID).ToList();//take all rooms included the given hotel id
            int count = bookedRooms.Count();
            if (count > 0)
                return count;
            return 0;
        }

        public bool CheckAvailability(AvailabilityChecking availabilityChecking)
        {
            var reservations= _reservationRepo.GetAll().ToList();
            //check if the hotel id and roomNumber are match with any of the records and check if the given check in date is infered between the old checkin and check out date
            var reservation = reservations.SingleOrDefault(R=>R.H_id== availabilityChecking.H_id
                                                   && R.RoomNumber==availabilityChecking.RoomNumber
                                                   &&R.CheckInDate.Date<=availabilityChecking.CheckInDate.Date
                                                   && R.CheckOutDate.Date>=availabilityChecking.CheckInDate.Date);
            if(reservations!=null)
                return false;
            return true;
        }
    }
}
