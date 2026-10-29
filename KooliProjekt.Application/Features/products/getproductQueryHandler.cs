using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.products;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, OperationResult<ProductDto>>
{
    private readonly ApplicationDbContext _context;

    public GetProductQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OperationResult<ProductDto>> Handle(
        GetProductQuery request,
        CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
        {
            return OperationResult<ProductDto>.NotFound("Product not found.");
        }

        var product = await _context.Products
            .AsNoTracking()
            .Where(p => p.id == request.Id)
            .Select(p => new ProductDto
            {
                Id = p.id,
                Name = p.Name,
                Description = p.description,
                Price = p.price,
                ProductCategoryId = p.productCategoryId
            })
            .FirstOrDefaultAsync(cancellationToken);

        return product == null
            ? OperationResult<ProductDto>.NotFound("Product not found.")
            : OperationResult<ProductDto>.Success(product);
    }
}