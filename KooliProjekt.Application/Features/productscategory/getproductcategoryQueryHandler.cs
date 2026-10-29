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

namespace KooliProjekt.Application.Features.ProductCategories;

public class GetProductCategoryQueryHandler : IRequestHandler<GetProductCategoryQuery, OperationResult<ProductCategoryDto>>
{
    private readonly ApplicationDbContext _context;

    public GetProductCategoryQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OperationResult<ProductCategoryDto>> Handle(
        GetProductCategoryQuery request,
        CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
        {
            return OperationResult<ProductCategoryDto>.NotFound("Product category not found.");
        }

        var category = await _context.ProductCategories
            .AsNoTracking()
            .Where(c => c.Id == request.Id)
            .Select(c => new ProductCategoryDto
            {
                Id = c.Id,
                Name = c.Name
            })
            .FirstOrDefaultAsync(cancellationToken);

        return category == null
            ? OperationResult<ProductCategoryDto>.NotFound("Product category not found.")
            : OperationResult<ProductCategoryDto>.Success(category);
    }
}