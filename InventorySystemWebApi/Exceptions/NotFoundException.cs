namespace InventorySystemWebApi.Exceptions
{
    public class NotFoundException : Exception
    {
        protected NotFoundException() : base()
        {
        }

        public NotFoundException(string? message) : base(message)
        {
        }

        protected NotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
