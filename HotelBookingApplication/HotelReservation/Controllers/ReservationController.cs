using HotelReservation.Interfaces;
using HotelReservation.Models;
using HotelReservation.Models.DTO;
using HotelReservation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _reservationService;

        //Inject the Reservation Service into the controller to access the service methods
        public ReservationController(ReservationService reservationService)
        {
            _reservationService= reservationService;
        }


        [ProducesResponseType(typeof(Reservation), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        public ActionResult<Reservation> Room_Booking(Reservation reservation)
        {
            try
            {
                if (reservation.R_id != 0)//Identity attribute should be empty
                    throw new InvalidReservationID();
                if (reservation.CheckInDate.Date >= reservation.CheckOutDate.Date)//checkIn date must be earlier than checkOut date
                    return BadRequest(new Error(1,"Check in Date should be less than check out date"));
                var myReservation = _reservationService.Add(reservation);//Calling the service
                if (myReservation != null)
                    return Created("Room Booked Successfully", myReservation);
                return BadRequest(new Error(2, $"The Room Number {reservation.RoomNumber} is Already Booked"));
            }
            catch (FormatException Fe)
            {
                return BadRequest(new Error(3, Fe.Message));
            }
            catch (InvalidReservationID ir)//Catch the custom exception of Invalid Reservation id
            {
                return BadRequest(new Error(4,ir.Message));
            }
        }


        [ProducesResponseType(typeof(Reservation), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpDelete]
        public ActionResult<Reservation> Cancelling_Booking(IdDTO idDTO)
        {
            if(idDTO.ID<=0)//Only Positive reservation id is acceptable
                return BadRequest(new Error(5,"Enter the Valid Reservation ID"));
            var reservation = _reservationService.Delete(idDTO);//Calling the service
            if (reservation != null)
                return Ok(reservation);
            return BadRequest(new Error(6, $"There is No Bookings for the Resevation id: {idDTO.ID}"));
        }


        [ProducesResponseType(typeof(Reservation), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        public ActionResult<Reservation> Get_Booking(IdDTO idDTO)
        {
            if (idDTO.ID <= 0)//Only Positive reservation id is acceptable
                return BadRequest(new Error(5, "Enter the Valid Reservation ID"));
            var reservation = _reservationService.GetReservation(idDTO);//Calling the service
            if (reservation != null)
                return Ok(reservation);
            return NotFound(new Error(6, $"There is No Bookings for the Reservation id: {idDTO.ID}"));
        }


        [ProducesResponseType(typeof(List<Reservation>), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]
        public ActionResult<Reservation> View_Bookings()
        {
            var reservations = _reservationService.GetAll();
            if (reservations != null)
                return Ok(reservations);
            return NotFound(new Error(7, "Nobody books the Rooms till Now"));
        }

        [ProducesResponseType(typeof(Reservation), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        public ActionResult<Reservation> Update_Booking(Reservation reservation)
        {
            if (reservation.R_id <= 0)//Only Positive reservation id is acceptable
                return BadRequest(new Error(5, "Enter the Valid Reservation ID"));
            var newReservation = _reservationService.Update(reservation);
            if (newReservation != null)
                return Ok(newReservation);
            return BadRequest(new Error(6, $"There is No Bookings for the Reservation id: {reservation.R_id}"));
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        public ActionResult<int> Booked_Rooms_paricular_hotel(IdDTO idDTO)
        {
            if (idDTO.ID <= 0)//Only Positive Hotel id is acceptable
                return BadRequest(new Error(8, "Enter the Valid Hotel ID"));
            var count = _reservationService.BookedRoomsCount(idDTO);
            if (count > 0)
                return Ok(count);
            return BadRequest(new Error(9, $"No Rooms Booked for the hotel id: {idDTO.ID}"));
        }

        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        public ActionResult<string> Check_Availability(AvailabilityChecking availabilityChecking)
        {
            if (availabilityChecking.H_id <= 0 && availabilityChecking.RoomNumber <= 0)
                return BadRequest(new Error(10, "Enter valid Hotel ID and RoomNumber"));
            if (availabilityChecking.CheckInDate == default(DateTime))//check in date should not be empty
                return BadRequest(new Error(11, "CheckIn date should not be Empty"));
            bool flag = _reservationService.CheckAvailability(availabilityChecking);
            if (!flag)
                return Ok($"Room No {availabilityChecking.RoomNumber} was already Booked");
            return Ok($"Room No {availabilityChecking.RoomNumber} is Available for Booking");
        }
    }
}
