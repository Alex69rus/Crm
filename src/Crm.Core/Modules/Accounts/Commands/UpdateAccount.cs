using System.ComponentModel.DataAnnotations;
using MediatR;
using Crm.Data.Entities.Accounts;

namespace Crm.Core.Modules.Accounts.Commands;

public class UpdateAccount : IRequest
{
    [Required]
    public required string Id { get; init; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
    [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]
    public required string Name { get; init; }

    [StringLength(50, ErrorMessage = "Short name cannot be longer than 50 characters")]
    public string? ShortName { get; init; }

    [StringLength(50, ErrorMessage = "Registration type cannot be longer than 50 characters")]
    public string? RegistrationType { get; init; }

    [StringLength(50, ErrorMessage = "Type cannot be longer than 50 characters")]
    public string? Type { get; init; }

    public bool Managed { get; init; }

    public AccountDiscretion? Discretion { get; init; }

    public AccountStatus Status { get; init; }

    public DateOnly? ClosedDate { get; init; }
} 