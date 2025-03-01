using Crm.Core.Infrastructure.Exceptions;
using Crm.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Crm.Core.Modules.Accounts.Commands;

internal class DeleteAccountHandler(CrmDbContext dbContext) : IRequestHandler<DeleteAccount>
{
    public async Task Handle(DeleteAccount request, CancellationToken cancellationToken)
    {
        var account = await dbContext.Accounts
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException($"Account with id {request.Id} not found");

        dbContext.Accounts.Remove(account);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}