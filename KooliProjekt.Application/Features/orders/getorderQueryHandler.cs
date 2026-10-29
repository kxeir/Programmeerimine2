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

namespace KooliProjekt.Application.Features.Orders
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OperationResult<OrderDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetOrderQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<OrderDto>> Handle(
            GetOrderQuery request,
            CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
            {
                return OperationResult<OrderDto>.NotFound("Order not found.");
            }

            var order = await _context.Orders
                .AsNoTracking()
                .Where(o => o.Id == request.Id)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    OrderState = o.OrderState,
                    UserId = o.UserId
                })
                .FirstOrDefaultAsync(cancellationToken);

            return order == null
                ? OperationResult<OrderDto>.NotFound("Order not found.")
                : OperationResult<OrderDto>.Success(order);
        }
    }
}