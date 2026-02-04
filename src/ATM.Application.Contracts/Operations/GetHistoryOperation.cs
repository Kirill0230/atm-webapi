using ATM.Application.Contracts.Models;

namespace ATM.Application.Contracts.Operations;

public static class GetHistoryOperation
{
    public readonly record struct Request(Guid SessionId);

    public abstract record Response
    {
        private Response() { }

        public sealed record Success(IEnumerable<OperationDto> OperationDtos) : Response;

        public sealed record Unauthorized(string Message) : Response;
    }
}