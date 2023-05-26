using HotelAPI.Models;
using HotelAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly HotelService _hotelService;

        public HotelController(HotelService hotelService)
        {
            _hotelService=hotelService;
        }
        [ProducesResponseType(typeof(Hotel), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        public ActionResult<Hotel> Add_Hotel(Hotel hotel)
        {
            var myHotel = _hotelService.AddHotel(hotel);
            if (myHotel != null)
                return Created("Hotel created Successfully",myHotel);
            return BadRequest(new Error(1,"Unable to add Hotel"));
        }

        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        public ActionResult<Room> Add_Room(Room room)
        {
            var myroom = _hotelService.AddRoom(room);
            if (myroom != null)
                return Created("Room created Successfully", myroom);
            return BadRequest(new Error(2, "Unable to add Room"));
        }
    }
}
