using System.ComponentModel.DataAnnotations;
using Crm.Data;
using Crm.Data.Entities.Contacts;
using MediatR;
using ValidationException = Crm.Core.Infrastructure.Exceptions.ValidationException;

namespace Crm.Core.Modules.Contacts.Commands;

internal class CreateContactHandler(CrmDbContext dbContext) : IRequestHandler<CreateContact, CreateContact.Response>
{
    private readonly CrmDbContext _dbContext = dbContext;

    public async Task<CreateContact.Response> Handle(CreateContact request, CancellationToken cancellationToken)
    {
        // Validate the request
        var validationContext = new ValidationContext(request);
        var validationResults = new List<ValidationResult>();
        
        if (!Validator.TryValidateObject(request, validationContext, validationResults, true))
        {
            throw new ValidationException(
                string.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage)));
        }

        var contactId = Guid.CreateVersion7().ToString();
        
        var contact = new Contact(
            id: contactId,
            firstName: request.FirstName,
            lastName: request.LastName,
            namePrefix: request.NamePrefix,
            middleName: request.MiddleName,
            gender: request.Gender,
            dateOfBirth: request.DateOfBirth,
            dateOfDeath: request.DateOfDeath,
            taxId: request.TaxId,
            citizenship: request.Citizenship);

        _dbContext.Contacts.Add(contact);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateContact.Response { Id = contactId };
    }
} 