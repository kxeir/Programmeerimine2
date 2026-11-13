using FluentValidation;
using KooliProjekt.Application.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.products;

public sealed class AddFileCommandValidator : AbstractValidator<AddFileCommand>
{
    private const long MaximumFileSize = 5 * 1024 * 1024;

    public AddFileCommandValidator(ApplicationDbContext dbContext)
    {
        RuleFor(command => command.ProductId).GreaterThan(0)
            .MustAsync((productId, cancellationToken) => dbContext.Products.AnyAsync(product => product.id == productId, cancellationToken))
            .WithMessage("Product does not exist.");
        RuleFor(command => command.File).NotNull()
            .Must(file => file is not null && file.Length > 0).WithMessage("File must not be empty.")
            .Must(file => file is null || file.Length <= MaximumFileSize).WithMessage("File size must not exceed 5 MB.");
    }
}
