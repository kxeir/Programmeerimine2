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

public class GetProductCategoryListQueryHandler : IRequestHandler<GetProductCategoryListQuery, OperationResult<PagedResult<ProductCategoryDto>>>
{
    private readonly ApplicationDbContext _context;

    public GetProductCategoryListQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OperationResult<PagedResult<ProductCategoryDto>>> Handle(
        GetProductCategoryListQuery request,
        CancellationToken cancellationToken)
    {
        var page = request.Page < 1 ? 1 : request.Page;
        var pageSize = request.PageSize < 1 ? 10 : request.PageSize;
        var query = _context.ProductCategories.AsNoTracking();
        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(c => c.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(c => new ProductCategoryDto
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync(cancellationToken);

        var result = new PagedResult<ProductCategoryDto>
        {
            Results = items,
            CurrentPage = page,
            PageSize = pageSize,
            RowCount = totalCount
        };

        return OperationResult<PagedResult<ProductCategoryDto>>.Success(result);
    }
}