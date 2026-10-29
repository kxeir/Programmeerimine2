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

namespace KooliProjekt.Application.Features.Orders;

public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, OperationResult<PagedResult<OrderDto>>>
{
    private readonly ApplicationDbContext _context;

    public GetOrderListQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OperationResult<PagedResult<OrderDto>>> Handle(
        GetOrderListQuery request,
        CancellationToken cancellationToken)
    {
        var page = request.Page < 1 ? 1 : request.Page;
        var pageSize = request.PageSize < 1 ? 10 : request.PageSize;
        var query = _context.Orders.AsNoTracking();
        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(o => o.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(o => new OrderDto
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                OrderState = o.OrderState,
                UserId = o.UserId
            })
            .ToListAsync(cancellationToken);

        var result = new PagedResult<OrderDto>
        {
            Results = items,
            CurrentPage = page,
            PageSize = pageSize,
            RowCount = totalCount
        };

        return OperationResult<PagedResult<OrderDto>>.Success(result);
    }
}