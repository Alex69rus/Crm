using System.ComponentModel.DataAnnotations;
using Crm.Core.Infrastructure.Exceptions;
using Crm.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ValidationException = Crm.Core.Infrastructure.Exceptions.ValidationException;

namespace Crm.Core.Modules.Accounts.Commands;

internal class UpdateAccountHandler(CrmDbContext dbContext) : IRequestHandler<UpdateAccount>
{
    private readonly CrmDbContext _dbContext = dbContext;

    public async Task Handle(UpdateAccount request, CancellationToken cancellationToken)
    {
        // Validate the request
        var validationContext = new ValidationContext(request);
        var validationResults = new List<ValidationResult>();

        if (!Validator.TryValidateObject(request, validationContext, validationResults, true))
        {
            throw new ValidationException(
                string.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage)));
        }

        var account = await _dbContext.Accounts
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (account == null)
            throw new NotFoundException($"Account with id {request.Id} not found");

        account.UpdateDetails(
            name: request.Name,
            shortName: request.ShortName,
            registrationType: request.RegistrationType,
            type: request.Type,
            managed: request.Managed,
            discretion: request.Discretion);

        account.UpdateStatus(request.Status, request.ClosedDate);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}