using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.Interfaces;
using TODO.APPLICATION.DTOs;

namespace TODO.APPLICATION.Features.Tags.Queries
{
    public class GetAllTagsQueryHandler(ITagRepository _repository): IRequestHandler<GetAllTagsQuery,IEnumerable<TagDto>>
    {
        public async Task<IEnumerable<TagDto>> Handle(GetAllTagsQuery query, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }
    }
}
