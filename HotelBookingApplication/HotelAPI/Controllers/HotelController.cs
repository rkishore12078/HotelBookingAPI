using HotelAPI.Models;
using HotelAPI.Models.DTO;
using HotelAPI.Services;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles ="staff")]
        public ActionResult<Hotel> Add_Hotel(Hotel hotel)
        {
            var myHotel = _hotelService.AddHotel(hotel);
            if (myHotel != null)
                return Created("Hotel created Successfully",myHotel);
            return BadRequest(new Error(5,"Unable to add Hotel"));
        }

        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        [Authorize(Roles ="staff")]
        public ActionResult<Room> Add_Room(Room room)
        {
            var myroom = _hotelService.AddRoom(room);
            if (myroom != null)
                return Created("Room created Successfully", myroom);
            return BadRequest(new Error(6, "Unable to add Room"));
        }

        [ProducesResponseType(typeof(Hotel), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpDelete]
        [Authorize(Roles = "staff")]

        public ActionResult<Room> Delete_Hotel([FromQuery] IdDTO idDTO)
        {
            var myHotel = _hotelService.DeleteHotel(idDTO);
            if (myHotel != null)
                return Created("Hotel deleted Successfully", myHotel);
            return BadRequest(new Error(7, "Unable to delete Hotel"));
        }

        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpDelete]
        [Authorize(Roles = "staff")]

        public ActionResult<Room> Delete_Room(IdDTO idDTO)
        {
            var myroom = _hotelService.DeleteRoom(idDTO);
            if (myroom != null)
                return Created("Room delete Successfully", myroom);
            return BadRequest(new Error(8, "Unable to delete Room"));
        }

        [ProducesResponseType(typeof(Hotel), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPut]
        [Authorize(Roles = "staff")]

        public ActionResult<Hotel> Update_Hotel(Hotel hotel)
        {
            var myHotel = _hotelService.UpdateHotel(hotel);
            if (myHotel != null)
                return Created("Hotel Updated Successfully", myHotel);
            return BadRequest(new Error(9, "Unable to Update Hotel"));
        }

        [ProducesResponseType(typeof(Hotel), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]
        [Authorize]

        public ActionResult<Hotel> Get_Hotel([FromQuery] IdDTO idDTO)
        {
            var myHotel = _hotelService.GetHotel(idDTO);
            if (myHotel != null)
                return Created("Hotel", myHotel);
            return BadRequest(new Error(10, "Unable to Get Hotel"));
        }

        [ProducesResponseType(typeof(Hotel), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]
        [Authorize]

        public ActionResult<Hotel> GetAll_Hotels()
        {
            var myHotels = _hotelService.GetAllHotels();
            if (myHotels.Count>0)
                return Ok(myHotels);
            return BadRequest(new Error(11, "Unable to Get Hotels"));
        }

        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]
        [Authorize]
        public ActionResult<List<Room>> Rooms_By_Hotel([FromQuery] IdDTO idDTO)
        {
            var rooms = _hotelService.Room_By_Hotel(idDTO);
            if (rooms != null)
                return Ok(rooms);
            return NotFound(new Error(4,"No Rooms available"));
        }

        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]
        [Authorize]
        public ActionResult<List<Room>> Hotel_By_City([FromQuery] HotelFilterDTO hotelFilterDTO)
        {
            var rooms = _hotelService.Hotel_By_City(hotelFilterDTO);
            if (rooms != null)
                return Ok(rooms);
            return NotFound(new Error(4, "No Rooms available"));
        }

        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]
        [Authorize]
        public ActionResult<List<Room>> Hotel_By_Country([FromQuery] HotelFilterDTO hotelFilterDTO)
        {
            var rooms = _hotelService.Hotel_By_Country(hotelFilterDTO);
            if (rooms != null)
                return Ok(rooms);
            return NotFound(new Error(4, "No Rooms available"));
        }

        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]
        [Authorize]
        public ActionResult<List<Room>> Hotel_By_Amenity([FromQuery] HotelFilterDTO hotelFilterDTO)
        {
            var rooms = _hotelService.Hotel_By_Amenity(hotelFilterDTO);
            if (rooms != null)
                return Ok(rooms);
            return NotFound(new Error(4, "No Rooms available"));
        }

        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]
        [Authorize]
        public ActionResult<List<Room>> Room_By_Price([FromQuery] PriceDTO priceDTO)
        {
            var rooms = _hotelService.Room_By_Price(priceDTO);
            if (rooms != null)
                return Ok(rooms);
            return NotFound(new Error(4, "No Rooms available"));
        }

        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]
        [Authorize]
        public ActionResult<List<Room>> Room_By_Capacity([FromQuery] CapacityAndTypeDTO capacityAndTypeDTO)
        {
            var rooms = _hotelService.Room_By_Capacity(capacityAndTypeDTO);
            if (rooms != null)
                return Ok(rooms);
            return NotFound(new Error(4, "No Rooms available"));
        }

        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]
        [Authorize]
        public ActionResult<List<Room>> Room_By_Type([FromQuery] CapacityAndTypeDTO capacityAndTypeDTO)
        {
            var rooms = _hotelService.Room_By_Type(capacityAndTypeDTO);
            if (rooms != null)
                return Ok(rooms);
            return NotFound(new Error(4, "No Rooms available"));
        }

        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]
        [Authorize]
        public ActionResult<List<Room>> Rooms_Count([FromQuery] IdDTO idDTO)
        {
            int roomsCount = _hotelService.Rooms_Count(idDTO);
            if(roomsCount > 0)
                return Ok(roomsCount);
            return NotFound(new Error(4, "No Rooms available"));
        }

        [ProducesResponseType(typeof(List<HotelAndRooms>), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]
        //[Authorize]
        public ActionResult<List<HotelAndRooms>> Total_Hotels_Rooms()
        {
            var hotelAndRooms=_hotelService.Total_Hotels_Rooms();
            if(hotelAndRooms!=null)
                return Ok(hotelAndRooms);
            return BadRequest(new Error(5,"Unable to fetch details"));
        }
    }
}
