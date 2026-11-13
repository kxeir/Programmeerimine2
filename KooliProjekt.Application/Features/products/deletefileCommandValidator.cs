using FluentValidation;

namespace KooliProjekt.Application.Features.products;

public sealed class DeleteFileCommandValidator : AbstractValidator<DeleteFileCommand>
{
    public DeleteFileCommandValidator()
    {
        RuleFor(command => command.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is required.");

        RuleFor(command => command.FileName)
            .NotEmpty()
            .WithMessage("File name is required.");
    }
}
