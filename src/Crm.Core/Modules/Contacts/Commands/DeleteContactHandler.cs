using Crm.Core.Infrastructure.Exceptions;
using Crm.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Crm.Core.Modules.Contacts.Commands;

internal class DeleteContactHandler(CrmDbContext dbContext) : IRequestHandler<DeleteContact>
{
    private readonly CrmDbContext _dbContext = dbContext;

    public async Task Handle(DeleteContact request, CancellationToken cancellationToken)
    {
        var contact = await _dbContext.Contacts
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (contact == null)
            throw new NotFoundException($"Contact with id {request.Id} not found");

        _dbContext.Contacts.Remove(contact);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
} 