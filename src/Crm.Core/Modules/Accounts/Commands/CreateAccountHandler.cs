using System.ComponentModel.DataAnnotations;
using Crm.Data;
using Crm.Data.Entities.Accounts;
using MediatR;
using ValidationException = Crm.Core.Infrastructure.Exceptions.ValidationException;

namespace Crm.Core.Modules.Accounts.Commands;

internal class CreateAccountHandler(CrmDbContext dbContext) : IRequestHandler<CreateAccount, CreateAccount.Response>
{
    private readonly CrmDbContext _dbContext = dbContext;

    public async Task<CreateAccount.Response> Handle(CreateAccount request, CancellationToken cancellationToken)
    {
        // Validate the request
        var validationContext = new ValidationContext(request);
        var validationResults = new List<ValidationResult>();
        
        if (!Validator.TryValidateObject(request, validationContext, validationResults, true))
        {
            throw new ValidationException(
                string.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage)));
        }

        var accountId = Guid.CreateVersion7().ToString();
        
        var account = new Account(
            id: accountId,
            name: request.Name,
            accountNumber: request.AccountNumber,
            sponsorCompany: request.SponsorCompany,
            shortName: request.ShortName,
            registrationType: request.RegistrationType,
            setupDate: request.SetupDate,
            managed: request.Managed,
            type: request.Type,
            status: request.Status,
            discretion: request.Discretion);

        _dbContext.Accounts.Add(account);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateAccount.Response { Id = accountId };
    }
} 