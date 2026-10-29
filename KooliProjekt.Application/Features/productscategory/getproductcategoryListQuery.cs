using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.ProductCategories;

public class GetProductCategoryListQuery : IRequest<OperationResult<PagedResult<ProductCategoryDto>>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}