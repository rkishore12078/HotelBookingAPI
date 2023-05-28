namespace HotelReservation.Exceptions
{
    public class InvalidDateException:Exception
    {
        string message;
        public InvalidDateException()
        {
            message = "Date you request is already booked";
        }
        public InvalidDateException(string message)
        {
            this.message = message;
        }
        public override string Message
        {
            get { return message; }
        }
    }
}
