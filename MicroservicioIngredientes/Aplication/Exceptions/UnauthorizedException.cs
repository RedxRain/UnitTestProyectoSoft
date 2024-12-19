namespace Application.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string mensaje) : base(mensaje) { }
    }
}