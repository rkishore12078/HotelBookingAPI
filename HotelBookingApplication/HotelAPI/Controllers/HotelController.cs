using HotelAPI.Exceptions;
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
            try
            {
                if (hotel.H_id != 0)
                    throw new InvalidPrimaryID();
                var myHotel = _hotelService.AddHotel(hotel);
                if (myHotel != null)
                    return Created("Hotel created Successfully", myHotel);
                return BadRequest(new Error(1, $"Hotel {hotel.H_id} is Present already"));
            }
            catch (InvalidPrimaryID ip)
            {
                return BadRequest(new Error(2, ip.Message));
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(25,ise.Message));
            }
        }

        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        [Authorize(Roles ="staff")]
        public ActionResult<Room> Add_Room(Room room)
        {
            try
            {
                if (room.R_id != 0)
                    throw new InvalidPrimaryID();
                var myroom = _hotelService.AddRoom(room);
                if (myroom != null)
                    return Created("Room created Successfully", myroom);
                return BadRequest(new Error(3, "Unable to add Room"));
            }
            catch (InvalidPrimaryID ip)
            {
                return BadRequest(new Error(2, ip.Message));
            }
            catch (HotelDoesn_tExist hde)
            {
                return BadRequest(new Error(24,hde.Message));
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(25, ise.Message));
            }
        }

        [ProducesResponseType(typeof(Hotel), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpDelete]
        [Authorize(Roles = "staff")]

        public ActionResult<Room> Delete_Hotel(IdDTO idDTO)
        {
            try
            {
                if (idDTO.ID <= 0)
                    return BadRequest(new Error(4, "Enter Valid Hotel ID"));
                var myHotel = _hotelService.DeleteHotel(idDTO);
                if (myHotel != null)
                    return Created("Hotel deleted Successfully", myHotel);
                return BadRequest(new Error(5, $"There is no hotel present for the id {idDTO.ID}"));
            }
            catch (FormatException fe)
            {
                return BadRequest(new Error(6, fe.Message));
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(25, ise.Message));
            }
        }

        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpDelete]
        [Authorize(Roles = "staff")]

        public ActionResult<Room> Delete_Room(IdDTO idDTO)
        {
            if (idDTO.ID <= 0)
                return BadRequest(new Error(4, "Enter Valid Room Number"));
            var myroom = _hotelService.DeleteRoom(idDTO);
            if (myroom != null)
                return Created("Room delete Successfully", myroom);
            return BadRequest(new Error(7, $"There is no Room present for the Number {idDTO.ID}"));
        }

        [ProducesResponseType(typeof(Hotel), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPut]
        [Authorize(Roles = "staff")]

        public ActionResult<Hotel> Update_Hotel(Hotel hotel)
        {
            try
            {
                if (hotel.H_id <= 0)
                    return BadRequest(new Error(4, "Enter Valid Hotel ID"));
                var myHotel = _hotelService.UpdateHotel(hotel);
                if (myHotel != null)
                    return Created("Hotel Updated Successfully", myHotel);
                return BadRequest(new Error(8, $"There is no hotel present for the id {hotel.H_id}"));
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(25, ise.Message));
            }
        }

        [ProducesResponseType(typeof(Hotel), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        [Authorize]

        public ActionResult<Hotel> View_Hotel(IdDTO idDTO)
        {
            try
            {
                if (idDTO.ID <= 0)
                    return BadRequest(new Error(4, "Enter Valid Hotel ID"));
                var myHotel = _hotelService.GetHotel(idDTO);
                if (myHotel != null)
                    return Created("Hotel", myHotel);
                return BadRequest(new Error(9, $"There is no hotel present for the id {idDTO.ID}"));
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(25, ise.Message));
            }
        }

        [ProducesResponseType(typeof(Hotel), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]
        [Authorize]

        public ActionResult<Hotel> View_All_Hotels()
        {
            try
            {
                var myHotels = _hotelService.GetAllHotels();
                if (myHotels.Count > 0)
                    return Ok(myHotels);
                return BadRequest(new Error(10, "No Hotels are Existing"));
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(25, ise.Message));
            }
        }

        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        [Authorize]
        public ActionResult<List<Room>> Rooms_By_Hotel(IdDTO idDTO)
        {
            if (idDTO.ID <= 0)
                return BadRequest(new Error(4, "Enter Valid Hotel ID"));
            var rooms = _hotelService.Room_By_Hotel(idDTO);
            if (rooms != null)
                return Ok(rooms);
            return NotFound(new Error(11,$"There is No rooms available for the hotel id {idDTO.ID}"));
        }

        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        [Authorize]
        public ActionResult<List<Room>> Hotel_By_City(HotelFilterDTO hotelFilterDTO)
        {
            if (hotelFilterDTO.city == null)
                return BadRequest(new Error(12, "City should not be Empty"));
            var rooms = _hotelService.Hotel_By_City(hotelFilterDTO);
            if (rooms != null)
                return Ok(rooms);
            return NotFound(new Error(13, $"There is No rooms available for the City {hotelFilterDTO.city}"));
        }

        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        [Authorize]
        public ActionResult<List<Room>> Hotel_By_Country(HotelFilterDTO hotelFilterDTO)
        {
            if (hotelFilterDTO.country == null)
                return BadRequest(new Error(14, "Country should not be Empty"));
            var rooms = _hotelService.Hotel_By_Country(hotelFilterDTO);
            if (rooms != null)
                return Ok(rooms);
            return NotFound(new Error(15, $"There is No rooms available for the Country {hotelFilterDTO.country}"));
        }

        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        [Authorize]
        public ActionResult<List<Room>> Hotel_By_Amenity(HotelFilterDTO hotelFilterDTO)
        {
            if (hotelFilterDTO.Amenities == null)
                return BadRequest(new Error(16, "Amenities should not be Empty"));
            var rooms = _hotelService.Hotel_By_Amenity(hotelFilterDTO);
            if (rooms != null)
                return Ok(rooms);
            return NotFound(new Error(17, $"There is No rooms available for the Amenity {hotelFilterDTO.Amenities}"));
        }

        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        [Authorize]
        public ActionResult<List<Room>> Room_By_Price(PriceDTO priceDTO)
        {
            try
            {
                if (priceDTO.H_Id <= 0)
                    return BadRequest(new Error(4, "Enter Valid Hotel Id"));
                if (priceDTO.MinValue <= 0 && priceDTO.MazValue <= 0)
                    return BadRequest(new Error(20,"Enter a valid Prices"));
                var rooms = _hotelService.Room_By_Price(priceDTO);
                if (rooms != null)
                    return Ok(rooms);
                return NotFound(new Error(19, $"There is no room for Hotel id {priceDTO.H_Id}"));
            }
            catch (InvalidPriceRange ip)
            {
                return BadRequest(new Error(18, ip.Message));
            }
        }

        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        [Authorize]
        public ActionResult<List<Room>> Room_By_Capacity(CapacityAndTypeDTO capacityAndTypeDTO)
        {
            if(capacityAndTypeDTO.H_id<=0)
                return BadRequest(new Error(4, "Enter Valid Hotel ID"));
            if (capacityAndTypeDTO.Capacity <= 0)
                return BadRequest(new Error(21,"Enter valid Capacity to filter"));
            var rooms = _hotelService.Room_By_Capacity(capacityAndTypeDTO);
            if (rooms != null)
                return Ok(rooms);
            return NotFound(new Error(11, $"There is No rooms available for the hotel id {capacityAndTypeDTO.H_id}"));
        }

        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        [Authorize]
        public ActionResult<List<Room>> Room_By_Type(CapacityAndTypeDTO capacityAndTypeDTO)
        {
            if (capacityAndTypeDTO.H_id <= 0)
                return BadRequest(new Error(4, "Enter Valid Hotel ID"));
            if (capacityAndTypeDTO.Type==null)
                return BadRequest(new Error(22, "Type should not be Empty"));
            var rooms = _hotelService.Room_By_Type(capacityAndTypeDTO);
            if (rooms != null)
                return Ok(rooms);
            return NotFound(new Error(11, $"There is No rooms available for the hotel id {capacityAndTypeDTO.H_id}"));
        }

        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        [Authorize]
        public ActionResult<List<Room>> Rooms_Count(IdDTO idDTO)
        {
            if (idDTO.ID <= 0)
                return BadRequest(new Error(4, "Enter Valid Hotel ID"));
            int roomsCount = _hotelService.Rooms_Count(idDTO);
            if(roomsCount > 0)
                return Ok(roomsCount);
            return NotFound(new Error(11, $"There is No rooms available for the hotel id {idDTO.ID}"));
        }

        [ProducesResponseType(typeof(List<HotelAndRooms>), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]
        [Authorize]
        public ActionResult<List<HotelAndRooms>> Total_Hotels_Rooms()
        {
            var hotelAndRooms=_hotelService.Total_Hotels_Rooms();
            if(hotelAndRooms!=null)
                return Ok(hotelAndRooms);
            return BadRequest(new Error(23,"Unable to fetch details"));
        }
    }
}
