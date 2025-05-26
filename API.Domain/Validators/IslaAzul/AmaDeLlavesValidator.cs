using System.Data;
using API.Data.Entidades.IslaAzul;
using API.Data.Entidades.Seguridad;
using API.Data.IUnitOfWorks.Interfaces;
using FluentValidation;

namespace API.Domain.Validators.Seguridad
{
    
    public class AmaDeLlavesValidator : AbstractValidator<AmaDeLlaves>
    {
        private readonly IUnitOfWork<AmaDeLlaves> _repositorios;

        public AmaDeLlavesValidator(IUnitOfWork<AmaDeLlaves> repositorios)
        {
            _repositorios = repositorios;

            RuleFor(a => a.Nombre).NotEmpty().WithMessage("El nombre no puede estar vacío.")
                .MaximumLength(50).WithMessage("El nombre debe tener como máximo 50 caracteres.")
                .NotNull().WithMessage("El nombre es obligatorio.");

            RuleFor(a => a.Apellidos).NotEmpty().WithMessage("Los apellidos no pueden estar vacíos.")
                .MaximumLength(50).WithMessage("Los apellidos deben tener como máximo 50 caracteres.")
                .NotNull().WithMessage("Los apellidos son obligatorios.");

            RuleFor(a => a.Ci)
                .NotNull().WithMessage("El CI es obligatorio.")
                .MaximumLength(11).WithMessage("El CI debe tener como máximo 11 caracteres.")
                .Matches("^[0-9]+$").WithMessage("El CI solo puede contener números.");


            RuleFor(a => a.Telefono).NotNull().WithMessage("El teléfono es obligatorio.").MaximumLength(25)
                .WithMessage("El teléfono debe tener como máximo 25 caracteres.")
                .Matches("^[0-9]+$").WithMessage("El teléfono solo puede contener números.");
            
            RuleFor(a => a).MustAsync(async (amaDeLlaves, cancelacion) =>
                    !await _repositorios.AmaDeLlaves.AnyAsync(e => e.Telefono == amaDeLlaves.Telefono && e.Id != amaDeLlaves.Id))
                .WithMessage("Ya existe un ama de llaves con el mismo telefono.");
            
            RuleFor(a => a).MustAsync(async (amaDeLlaves, cancelacion) =>
                    !await _repositorios.AmaDeLlaves.AnyAsync(e => e.Ci == amaDeLlaves.Ci && e.Id != amaDeLlaves.Id))
                .WithMessage("Ya existe un ama de llaves con el mismo CI.");
            
            RuleFor(c => c).MustAsync(async (amaDeLlaves, cancelacion) =>
                    !await _repositorios.AmaDeLlaves.AnyAsync(e => e.Nombre == amaDeLlaves.Nombre && e.Apellidos == amaDeLlaves.Apellidos && e.Id != amaDeLlaves.Id))
                .WithMessage("Ya existe una ama de llaves con el mismo nombre y apellidos.");
        }
    }       
}

