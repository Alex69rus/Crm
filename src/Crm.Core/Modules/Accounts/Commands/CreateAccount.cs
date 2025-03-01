using System.ComponentModel.DataAnnotations;
using MediatR;
using Crm.Data.Entities.Accounts;

namespace Crm.Core.Modules.Accounts.Commands;

public class CreateAccount : IRequest<CreateAccount.Response>
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
    [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]
    public required string Name { get; init; }

    [Required(ErrorMessage = "Account number is required")]
    [StringLength(50, ErrorMessage = "Account number cannot be longer than 50 characters")]
    public required string AccountNumber { get; init; }

    [StringLength(50, ErrorMessage = "Short name cannot be longer than 50 characters")]
    public string? ShortName { get; init; }

    [StringLength(50, ErrorMessage = "Registration type cannot be longer than 50 characters")]
    public string? RegistrationType { get; init; }

    public DateOnly? SetupDate { get; init; }

    [Required(ErrorMessage = "Sponsor company is required")]
    [StringLength(100, ErrorMessage = "Sponsor company cannot be longer than 100 characters")]
    public required string SponsorCompany { get; init; }

    public bool Managed { get; init; }

    [StringLength(50, ErrorMessage = "Type cannot be longer than 50 characters")]
    public string? Type { get; init; }

    public AccountStatus Status { get; init; } = AccountStatus.PendingApproval;

    public AccountDiscretion? Discretion { get; init; }

    public class Response
    {
        public required string Id { get; init; }
    }
}
