using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODO.APPLICATION.Interfaces;

namespace TODO.APPLICATION.Behaviors
{
    public class TransactionBehaviour<TReuest,TResponse>(IUnitOfWork _uow) : IPipelineBehavior<TReuest,TResponse> where TReuest: notnull
    {
        public async Task<TResponse> Handle(TReuest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            if(request is not ITransactionalRequest)
            {
                return await next();
            }

            await _uow.BeginAsync(cancellationToken);

            try
            {
                var response = await next();

                await _uow.CommitAsync(cancellationToken);

                return response;
            }
            catch
            {
                await _uow.RollbackAsync(cancellationToken);

                throw;
            }

        }
    }
}
