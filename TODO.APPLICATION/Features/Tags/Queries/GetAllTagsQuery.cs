using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.DTOs;

namespace TODO.APPLICATION.Features.Tags.Queries
{
    public record GetAllTagsQuery(
        ):IRequest<IEnumerable<TagDto>>;
    
}
