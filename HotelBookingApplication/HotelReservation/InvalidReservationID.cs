namespace HotelReservation
{
    public class InvalidReservationID:Exception
    {
        string message;
        public InvalidReservationID()
        {
            message = "Reservation ID should be Empty";
        }
        public InvalidReservationID(string message)
        {
            this.message = message;
        }
        public override string Message
        {
            get { return message; }
        }
    }
}
