using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Orders;

public class GetOrderListQuery : IRequest<OperationResult<PagedResult<OrderDto>>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}