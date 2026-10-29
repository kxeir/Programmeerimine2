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

namespace KooliProjekt.Application.Features.orderlines;

public class OrderLineListQueryHandler : IRequestHandler<OrderLineListQuery, OperationResult<PagedResult<OrderLineDto>>>
{
    private readonly ApplicationDbContext _context;

    public OrderLineListQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OperationResult<PagedResult<OrderLineDto>>> Handle(
        OrderLineListQuery request,
        CancellationToken cancellationToken)
    {
        var page = request.Page < 1 ? 1 : request.Page;
        var pageSize = request.PageSize < 1 ? 10 : request.PageSize;
        var query = _context.OrderLines.AsNoTracking();
        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(ol => ol.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(ol => new OrderLineDto
            {
                Id = ol.Id,
                Price = ol.Price,
                Quantity = ol.Quantity,
                TotalPrice = ol.Price * ol.Quantity,
                ProductId = ol.ProductId,
                OrderId = ol.OrderId
            })
            .ToListAsync(cancellationToken);

        var result = new PagedResult<OrderLineDto>
        {
            Results = items,
            CurrentPage = page,
            PageSize = pageSize,
            RowCount = totalCount
        };

        return OperationResult<PagedResult<OrderLineDto>>.Success(result);
    }
}
