namespace ATM.Application.Contracts.Operations;

public static class Deposit
{
    public readonly record struct Request(Guid SessionId, decimal Amount);

    public abstract record Response
    {
        private Response() { }

        public sealed record Success() : Response;

        public sealed record Unauthorized(string Message) : Response;

        public sealed record BadRequest(string Message) : Response;
    }
}