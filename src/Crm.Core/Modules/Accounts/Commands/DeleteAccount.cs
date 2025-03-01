using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Crm.Core.Modules.Accounts.Commands;

public class DeleteAccount : IRequest
{
    [Required]
    public required string Id { get; init; }
} 