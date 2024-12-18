namespace Application.Exceptions
{
    public class BadRequestt : Exception
    {
        public BadRequestt(string message) : base(message)
        {
        }

        public BadRequestt(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
