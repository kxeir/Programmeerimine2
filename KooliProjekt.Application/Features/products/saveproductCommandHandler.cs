using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.products
{
    public class SaveProductCommandHandler : IRequestHandler<SaveProductCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveProductCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveProductCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            Product product;

            if (request.Id == 0)
            {
                product = new Product();
                _dbContext.Products.Add(product);
            }
            else
            {
                product = await _dbContext.Products.FindAsync(new object[] { request.Id }, cancellationToken);
                if (product == null)
                {
                    return result.AddError("Toodet ei leitud.");
                }
            }

            product.Name = request.Name;
            product.description = request.Description;
            product.price = request.Price;
            product.productCategoryId = request.ProductCategoryId;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}