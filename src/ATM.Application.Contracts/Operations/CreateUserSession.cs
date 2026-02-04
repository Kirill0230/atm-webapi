using ATM.Application.Contracts.Models;

namespace ATM.Application.Contracts.Operations;

public static class CreateUserSession
{
    public readonly record struct Request(long AccountNumber, long Pincode);

    public abstract record Response
    {
        private Response() { }

        public sealed record Success(SessionDto Session) : Response;

        public sealed record Failure(string Message) : Response;
    }
}