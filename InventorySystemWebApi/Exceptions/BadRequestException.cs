namespace InventorySystemWebApi.Exceptions
{
    public class BadRequestException : Exception
    {
        protected BadRequestException() : base()
        {
        }

        public BadRequestException(string? message) : base(message)
        {
        }

        protected BadRequestException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
