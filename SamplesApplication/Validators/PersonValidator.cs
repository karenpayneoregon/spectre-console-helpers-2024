using FluentValidation;
using SamplesApplication.Models;

namespace SamplesApplication.Validators;

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(person => person.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.")
            .Length(1, 50)
            .WithMessage("First name must be between 2 and 50 characters.");

        RuleFor(person => person.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.")
            .Length(2, 50)
            .WithMessage("Last name must be between 2 and 50 characters.");

        RuleFor(person => person.BirthDate)
            .LessThan(DateOnly.FromDateTime(DateTime.Now))
            .WithMessage("Birth date must be in the past.");
    }
}
