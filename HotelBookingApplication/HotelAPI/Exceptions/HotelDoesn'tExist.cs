namespace HotelAPI.Exceptions
{
    public class HotelDoesn_tExist:Exception
    {
        string message;
        public HotelDoesn_tExist()
        {
            message = "No Hotel exist you can't able to add room";
        }
        public HotelDoesn_tExist(string message)
        {
            this.message = message;
        }
        public override string Message
        {
            get { return message; }
        }
    }
}
