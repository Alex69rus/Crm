using Crm.Core.Modules.Accounts.Queries;
using MediatR;

namespace Crm.Api.GraphQL.Queries;

[ExtendObjectType("Query")]
public class AccountQueries()
{
    public async Task<SearchAccounts.Response> SearchAccounts(
        [Service(ServiceKind.Synchronized)] IMediator mediator,
        string? searchTerm = null,
        int skip = 0,
        int take = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new SearchAccounts
        {
            SearchTerm = searchTerm,
            Skip = skip,
            Take = take
        };

        return await mediator.Send(query, cancellationToken);
    }
}