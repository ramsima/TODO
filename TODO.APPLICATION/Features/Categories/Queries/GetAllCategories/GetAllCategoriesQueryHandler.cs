using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.Interfaces;
using TODO.APPLICATION.DTOs;

namespace TODO.APPLICATION.Features.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQueryHandler(ICategoryRepository _repository) : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
    {
        public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }
        
    }
}
