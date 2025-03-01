using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Crm.Core.Modules.Accounts.Queries;

public class SearchAccounts : IRequest<SearchAccounts.Response>
{
    [StringLength(100, ErrorMessage = "Search term cannot be longer than 100 characters")]
    public string? SearchTerm { get; init; }

    public int Skip { get; init; } = 0;

    [Range(1, 100, ErrorMessage = "Take must be between 1 and 100")]
    public int Take { get; init; } = 10;

    public class Response
    {
        public required IReadOnlyList<AccountDto> Items { get; init; }
        public required int TotalCount { get; init; }
    }

    public class AccountDto
    {
        public required string Id { get; init; }
        public required string Name { get; init; }
        public required decimal Balance { get; init; }
        public required DateTime CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
} 