using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.DTOs;
using Azure.Core.Pipeline;
using Backend.Controllers;
namespace Backend.Validators
{
    public class BeerUpdateValidator : AbstractValidator<BeerUpdateDto>
    {
      public BeerUpdateValidator() {

            RuleFor(x => x.BeerID).NotNull().WithMessage("El ID  es obligatorio.");
            RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("El nombre de la cerveza es obligatorio.")
                    .MaximumLength(100).WithMessage("El nombre de la cerveza no puede exceder los 100 caracteres.");
            RuleFor(x => x.Name).Length(2, 20).WithMessage("El nombre de la cerveza no puede exceder los 100 caracteres.");
            RuleFor(x => x.BrandID).NotNull().WithMessage("El ID de la marca es obligatorio.");

            RuleFor(x => x.BrandID).GreaterThan(0).WithMessage("Error con el valor enviado de marca");

            RuleFor(x => x.Alcohol).GreaterThan(0).WithMessage("El {PropertyName} de la marca es obligatorio.");


        }

    }
}
