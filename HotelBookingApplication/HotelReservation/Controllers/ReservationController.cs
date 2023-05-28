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
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _reservationService;

        public ReservationController(ReservationService reservationService)
        {
            _reservationService= reservationService;
        }
        [ProducesResponseType(typeof(Reservation), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        [Authorize]
        public ActionResult<Reservation> Room_Booking(Reservation reservation)
        {
            var myReservation = _reservationService.Add(reservation);
            if (myReservation != null)
                return Created("Room Booked Successfully", myReservation);
            return BadRequest(new Error(1, "Unable to Book Room"));
        }


        [ProducesResponseType(typeof(Reservation), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpDelete]
        //[Authorize]
        public ActionResult<Reservation> Cancelling_Booking(IdDTO idDTO)
        {
            var reservation = _reservationService.Delete(idDTO);
            if (reservation != null)
                return Ok(reservation);
            return BadRequest(new Error(3, $"There is No Bookings for the id: {idDTO.ID}"));
        }

        [ProducesResponseType(typeof(Reservation), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        [Authorize]
        public ActionResult<Reservation> Get_Booking(IdDTO idDTO)
        {
            var reservation = _reservationService.GetReservation(idDTO);
            if (reservation != null)
                return Ok(reservation);
            return NotFound(new Error(3, $"There is No Bookings for the id: {idDTO.ID}"));
        }

        [ProducesResponseType(typeof(List<Reservation>), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]
        //[Authorize]
        public ActionResult<Reservation> Get_All_Bookings()
        {
            var reservations = _reservationService.GetAll();
            if (reservations != null)
                return Ok(reservations);
            return NotFound(new Error(4, "No Bookings Available"));
        }

        [ProducesResponseType(typeof(Reservation), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        [Authorize]
        public ActionResult<Reservation> Update_Booking(Reservation reservation)
        {
            var newReservation = _reservationService.Update(reservation);
            if (newReservation != null)
                return Ok(newReservation);
            return BadRequest(new Error(3, $"There is No Bookings for the id: {reservation.R_id}"));
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        [Authorize]
        public ActionResult<int> Booked_Rooms_paricular_hotel(IdDTO idDTO)
        {
            var count = _reservationService.BookedRoomsCount(idDTO);
            if (count > 0)
                return Ok(count);
            return BadRequest(new Error(2, $"No Rooms Booked for the hotel id: {idDTO.ID}"));
        }

        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        //[Authorize]
        public ActionResult<string> Check_Availability(AvailabilityChecking availabilityChecking)
        {
            bool flag = _reservationService.CheckAvailability(availabilityChecking);
            if (!flag)
                return Ok($"Room No {availabilityChecking.RoomNumber} was already Booked");
            return Ok($"Room No {availabilityChecking.RoomNumber} is Available for Booking");
        }
    }
}
