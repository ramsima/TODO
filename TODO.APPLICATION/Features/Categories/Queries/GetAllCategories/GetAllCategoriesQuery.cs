using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TODO.APPLICATION.DTOs;
using TODO.APPLICATION.Interfaces;

namespace TODO.APPLICATION.Features.Categories.Queries.GetAllCategories
{
    public record GetAllCategoriesQuery(

        ) : ICacheableQuery<IEnumerable<CategoryDto>>
    {
        public string CacheKey => "categories:all";

        public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
    }

}
