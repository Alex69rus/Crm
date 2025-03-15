using System;
using Crm.Data;
using Crm.Data.Entities.Accounts;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Sorting;
using Microsoft.EntityFrameworkCore;

namespace Crm.Api.Modules.Accounts.GraphQl;

public class AccountObjectType : ObjectType<Account>
{
    protected override void Configure(IObjectTypeDescriptor<Account> descriptor)
        => descriptor.Ignore(x => x.Id);
}

public class AccountSortType : SortInputType<Account>
{
    protected override void Configure(ISortInputTypeDescriptor<Account> descriptor)
        => descriptor.Ignore(x => x.Id);
}

public class AccountFilterType : FilterInputType<Account>
{
    protected override void Configure(IFilterInputTypeDescriptor<Account> descriptor)
        => descriptor.Ignore(x => x.Id);
}


public class AccountQuery
{
    public Account Account(string id)
    {
        return new Account("1", "Account 1", "Description 1", "Type 1", null, null, null, false, null, AccountStatus.Active, null);
    }

    [UseFiltering]
    public IQueryable<Account> GetAccounts([Service] CrmDbContext dbContext)
        => dbContext.Accounts
            .AsNoTracking()
            .Include(x => x.Balance)
            .Include(x => x.AccountOwners)
            .ThenInclude(x => x.Contact)
            .ThenInclude(x=>x.Addresses)
            ;

}
