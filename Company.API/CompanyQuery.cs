using Company.API.Persistence;
using Company.API.Persistence.Entities;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Company.API
{
    public class CompanyQuery
    {
        public IQueryable<Persistence.Entities.Company> GetCompanies([ScopedService] CompanyDbContext context) => context
           .Companies
            .Include(c => c.History);
    }

    public class QueryType : ObjectType<CompanyQuery>
    {
        protected override void Configure(IObjectTypeDescriptor<CompanyQuery> descriptor)
        {
            descriptor
                .Field(f => f.GetCompanies(default))
                .UseDbContext<CompanyDbContext>()
                .Type<ListType<CompanyType>>();
                // .UseProjection();
        }
    }

    public class CompanyType : ObjectType<Persistence.Entities.Company>
    {
        protected override void Configure(IObjectTypeDescriptor<Persistence.Entities.Company> descriptor)
        {
            descriptor
                .Field(f => f.CompanyId)
                .Type<IntType>();

            descriptor
                .Field(f => f.History)
                .Type<ListType<HistoryUnionType>>();
        }
    }

    public class HistoryUnionType : UnionType<History>
    {
        protected override void Configure(IUnionTypeDescriptor descriptor)
        {
            descriptor.Name("History");
            descriptor.Type<HistoryTypeOneType>();
            descriptor.Type<HistoryTypeTwoType>();
        }
    }

    public class HistoryTypeOneType : ObjectType<HistoryTypeTwo>
    {
    }

    public class HistoryTypeTwoType : ObjectType<HistoryTypeOne>
    {
    }
}
