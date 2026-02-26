using Backend.DTOs;
using FluentValidation;

namespace Backend.Validators
{
    public class BeerInsertValidator : AbstractValidator<BeerInsertDto>
    {
        public BeerInsertValidator() { 
        
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("El nombre de la cerveza es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre de la cerveza no puede exceder los 100 caracteres.");
        }

    }
}
