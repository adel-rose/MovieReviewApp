using FluentValidation;
using MovieApp.Domain.DTOs;

namespace MovieApp.Application.Validators;

public sealed class MovieCreateDTOValidator : AbstractValidator<MovieCreateDTO>
{
    public MovieCreateDTOValidator()
    {
        RuleFor(movie => movie.Price)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Prices lower than 5 are not allowed");
    }
}