using ATM.Application.Contracts.Models;

namespace ATM.Application.Contracts.Operations;

public static class CreateAccount
{
    public readonly record struct Request(Guid SessionId, long AccountNumber, long Pincode);

    public abstract record Response
    {
        private Response() { }

        public sealed record Success(AccountDto AccountDto) : Response;

        public sealed record Unauthorized(string Message) : Response;

        public sealed record BadRequest(string Message) : Response;
    }
}