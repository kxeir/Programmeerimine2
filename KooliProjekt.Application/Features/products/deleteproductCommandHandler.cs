using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.products
{
    public class DeleteProductCommandHandler : IRequestHandler<deleteproductCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteProductCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(deleteproductCommand request, CancellationToken cancellationToken)
        {
            await _dbContext.Products
                .Where(p => p.id == request.Id)
                .ExecuteDeleteAsync(cancellationToken);

            return new OperationResult();
        }
    }
}