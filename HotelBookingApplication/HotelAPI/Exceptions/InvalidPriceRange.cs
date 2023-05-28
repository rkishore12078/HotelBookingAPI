namespace HotelAPI.Exceptions
{
    public class InvalidPriceRange:Exception
    {
        string message;
        public InvalidPriceRange()
        {
            message = "Lower Range Price should be less than Upper Range Price";
        }
        public InvalidPriceRange(string message)
        {
            this.message = message;
        }
        public override string Message
        {
            get { return message; }
        }
    }
}
