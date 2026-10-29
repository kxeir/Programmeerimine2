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

public class OrderLineQueryHandler : IRequestHandler<OrderLineQuery, OperationResult<OrderLineDto>>
{
    private readonly ApplicationDbContext _context;

    public OrderLineQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OperationResult<OrderLineDto>> Handle(
        OrderLineQuery request,
        CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
        {
            return OperationResult<OrderLineDto>.NotFound("Order line not found.");
        }

        var orderLine = await _context.OrderLines
            .AsNoTracking()
            .Where(ol => ol.Id == request.Id)
            .Select(ol => new OrderLineDto
            {
                Id = ol.Id,
                Price = ol.Price,
                Quantity = ol.Quantity,
                TotalPrice = ol.Price * ol.Quantity,
                ProductId = ol.ProductId,
                OrderId = ol.OrderId
            })
            .FirstOrDefaultAsync(cancellationToken);

        return orderLine == null
            ? OperationResult<OrderLineDto>.NotFound("Order line not found.")
            : OperationResult<OrderLineDto>.Success(orderLine);
    }
}
