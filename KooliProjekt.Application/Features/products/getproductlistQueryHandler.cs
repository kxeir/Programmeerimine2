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

public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, OperationResult<PagedResult<ProductDto>>>
{
    private readonly ApplicationDbContext _context;

    public GetProductListQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OperationResult<PagedResult<ProductDto>>> Handle(
        GetProductListQuery request,
        CancellationToken cancellationToken)
    {
        var page = request.Page < 1 ? 1 : request.Page;
        var pageSize = request.PageSize < 1 ? 10 : request.PageSize;
        var query = _context.Products.AsNoTracking();
        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(p => p.id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new ProductDto
            {
                Id = p.id,
                Name = p.Name,
                Description = p.description,
                Price = p.price,
                ProductCategoryId = p.productCategoryId
            })
            .ToListAsync(cancellationToken);

        var result = new PagedResult<ProductDto>
        {
            Results = items,
            CurrentPage = page,
            PageSize = pageSize,
            RowCount = totalCount
        };

        return OperationResult<PagedResult<ProductDto>>.Success(result);
    }
}