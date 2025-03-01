using System.ComponentModel.DataAnnotations;
using Crm.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ValidationException = Crm.Core.Infrastructure.Exceptions.ValidationException;

namespace Crm.Core.Modules.Accounts.Queries;

internal class SearchAccountsHandler(CrmDbContext dbContext) : IRequestHandler<SearchAccounts, SearchAccounts.Response>
{
    private readonly CrmDbContext _dbContext = dbContext;

    public async Task<SearchAccounts.Response> Handle(SearchAccounts request, CancellationToken cancellationToken)
    {
        // Validate the request
        var validationContext = new ValidationContext(request);
        var validationResults = new List<ValidationResult>();
        
        if (!Validator.TryValidateObject(request, validationContext, validationResults, true))
        {
            throw new ValidationException(
                string.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage)));
        }

        var query = _dbContext.Accounts
            .Include(a => a.Balance)
            .AsNoTracking();

        // Apply search filter if search term is provided
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchTerm = request.SearchTerm.ToLower();
            query = query.Where(a => a.Name.ToLower().Contains(searchTerm));
        }

        // Get total count for pagination
        var totalCount = await query.CountAsync(cancellationToken);

        // Apply pagination and get results
        var accounts = await query
            .OrderByDescending(a => a.CreatedAt)
            .Skip(request.Skip)
            .Take(request.Take)
            .Select(a => new SearchAccounts.AccountDto
            {
                Id = a.Id,
                Name = a.Name,
                Balance = a.Balance.Balance,
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt
            })
            .ToListAsync(cancellationToken);

        return new SearchAccounts.Response
        {
            Items = accounts,
            TotalCount = totalCount
        };
    }
} 