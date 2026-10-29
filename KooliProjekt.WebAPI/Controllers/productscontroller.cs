using KooliProjekt.Application.Features.products;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace KooliProjekt.WebAPI.Controllers;

public class ProductsController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        if (id <= 0)
        {
            return NotFound();
        }

        var result = await _mediator.Send(new GetProductQuery
        {
            Id = id
        });

        if (result == null || result.Value == null)
        {
            return NotFound();
        }

        return Result(result);
    }

    [HttpGet]
    public async Task<IActionResult> List(int page = 1, int pageSize = 10)
    {
        var result = await _mediator.Send(new GetProductListQuery
        {
            Page = page,
            PageSize = pageSize
        });

        if (result == null || result.Value == null)
        {
            return NotFound();
        }

        return Result(result);
    }
}
