using ATM.Application.Contracts.Models;

namespace ATM.Application.Contracts.Operations;

public static class ViewingBalance
{
    public readonly record struct Request(Guid SessionId);

    public abstract record Response
    {
        private Response() { }

        public sealed record Success(BalanceDto BalanceDto) : Response;

        public sealed record Unauthorized(string Message) : Response;

        public sealed record BadRequest(string Message) : Response;
    }
}