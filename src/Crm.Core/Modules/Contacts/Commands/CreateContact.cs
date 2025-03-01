using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Crm.Core.Modules.Contacts.Commands;

public class CreateContact : IRequest<CreateContact.Response>
{
    [Required(ErrorMessage = "First name is required")]
    [StringLength(100, ErrorMessage = "First name cannot be longer than 100 characters")]
    [MinLength(2, ErrorMessage = "First name must be at least 2 characters long")]
    public required string FirstName { get; init; }

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(100, ErrorMessage = "Last name cannot be longer than 100 characters")]
    [MinLength(2, ErrorMessage = "Last name must be at least 2 characters long")]
    public required string LastName { get; init; }

    [StringLength(100, ErrorMessage = "Middle name cannot be longer than 100 characters")]
    public string? MiddleName { get; init; }

    [StringLength(20, ErrorMessage = "Name prefix cannot be longer than 20 characters")]
    public string? NamePrefix { get; init; }

    [StringLength(50, ErrorMessage = "Gender cannot be longer than 50 characters")]
    public string? Gender { get; init; }

    public DateOnly? DateOfBirth { get; init; }
    public DateOnly? DateOfDeath { get; init; }

    [StringLength(50, ErrorMessage = "Tax ID cannot be longer than 50 characters")]
    public string? TaxId { get; init; }

    [StringLength(100, ErrorMessage = "Citizenship cannot be longer than 100 characters")]
    public string? Citizenship { get; init; }

    public class Response
    {
        public required string Id { get; init; }
    }
} 