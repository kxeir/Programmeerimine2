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

    [HttpPost]
    public async Task<IActionResult> Save([FromBody] SaveProductCommand command)
    {
        var result = await _mediator.Send(command);
        return Result(result);
    }

    [HttpPost("{id:int}/files")]
    public async Task<IActionResult> AddFile(int id, [FromForm] AddFileCommand command)
    {
        command.ProductId = id;
        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }

    [HttpGet("{id:int}/files")]
    public async Task<IActionResult> ListFiles(int id)
    {
        var result = await _mediator.Send(new ListFilesQuery
        {
            ProductId = id
        });

        return Result(result);
    }

    [HttpGet("{id:int}/files/{fileName}")]
    public async Task<IActionResult> GetFile(int id, string fileName)
    {
        var result = await _mediator.Send(new GetFileQuery
        {
            ProductId = id,
            FileName = fileName
        });

        if (result is null || result.HasErrors || result.Value is null)
        {
            return NotFound();
        }

        return File(result.Value.Content, result.Value.ContentType, result.Value.FileName);
    }

    [HttpDelete("{id:int}/files/{fileName}")]
    public async Task<IActionResult> DeleteFile(int id, string fileName)
    {
        var result = await _mediator.Send(new DeleteFileCommand
        {
            ProductId = id,
            FileName = fileName
        });

        return Result(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
        {
            return NotFound();
        }

        var result = await _mediator.Send(new deleteproductCommand { Id = id });
        return Result(result);
    }
}