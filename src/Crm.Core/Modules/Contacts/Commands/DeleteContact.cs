using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Crm.Core.Modules.Contacts.Commands;

public class DeleteContact : IRequest
{
    [Required]
    public required string Id { get; init; }
} 