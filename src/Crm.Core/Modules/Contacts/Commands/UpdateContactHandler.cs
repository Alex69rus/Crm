using Crm.Core.Infrastructure.Exceptions;
using Crm.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ValidationException = Crm.Core.Infrastructure.Exceptions.ValidationException;

namespace Crm.Core.Modules.Contacts.Commands;

internal class UpdateContactHandler(CrmDbContext dbContext) : IRequestHandler<UpdateContact>
{
    private readonly CrmDbContext _dbContext = dbContext;

    public async Task Handle(UpdateContact request, CancellationToken cancellationToken)
    {
        // Validate the request
        var validationContext = new ValidationContext(request);
        var validationResults = new List<ValidationResult>();

        if (!Validator.TryValidateObject(request, validationContext, validationResults, true))
        {
            throw new ValidationException(
                string.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage)));
        }

        var contact = await _dbContext.Contacts
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (contact == null)
            throw new NotFoundException($"Contact with id {request.Id} not found");

        contact.UpdateNames(
            firstName: request.FirstName,
            lastName: request.LastName,
            namePrefix: request.NamePrefix,
            middleName: request.MiddleName);

        contact.UpdatePersonalInfo(
            gender: request.Gender,
            dateOfBirth: request.DateOfBirth,
            dateOfDeath: request.DateOfDeath,
            taxId: request.TaxId,
            citizenship: request.Citizenship);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
} 