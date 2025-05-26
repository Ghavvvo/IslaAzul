using API.Data.Entidades.IslaAzul;
using API.Data.IUnitOfWorks.Interfaces;
using FluentValidation;

namespace API.Domain.Validators.Seguridad
{
    
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        private readonly IUnitOfWork<Cliente> _repositorios;

        public ClienteValidator(IUnitOfWork<Cliente> repositorios)
        {
            _repositorios = repositorios;

            RuleFor(c => c.Nombre).NotEmpty().WithMessage("El nombre no puede estar vacío.")
                .MaximumLength(50).WithMessage("El nombre debe tener como máximo 50 caracteres.")
                .NotNull().WithMessage("El nombre es obligatorio.");

            RuleFor(c => c.Apellidos).NotEmpty().WithMessage("Los apellidos no pueden estar vacíos.")
                .MaximumLength(50).WithMessage("Los apellidos deben tener como máximo 50 caracteres.")
                .NotNull().WithMessage("Los apellidos son obligatorios.");

            RuleFor(c => c.Ci).NotNull().WithMessage("El CI es obligatorio.").MaximumLength(11)
                .WithMessage("El CI debe tener como máximo 11 caracteres.")
                .Matches("^[0-9]+$").WithMessage("El CI solo puede contener números.");;
            
                        
            RuleFor(c => c.Telefono).NotNull().WithMessage("El teléfono es obligatorio.").MaximumLength(25)
                .WithMessage("El teléfono debe tener como máximo 25 caracteres.")
                .Matches("^[0-9]+$").WithMessage("El teléfono solo puede contener números.");
           
            
            RuleFor(e => e).MustAsync(async (amaDeLlaves, cancelacion) =>
                    !await _repositorios.Clientes.AnyAsync(e => e.Telefono == amaDeLlaves.Telefono && e.Id != amaDeLlaves.Id))
                .WithMessage("Ya existe un cliente con el mismo telefono.");


            RuleFor(c => c).MustAsync(async (cliente, cancelacion) =>
                    !await _repositorios.Clientes.AnyAsync(e => e.Ci == cliente.Ci && e.Id != cliente.Id))
                .WithMessage("Ya existe un cliente con el mismo CI.");
            
            RuleFor(c => c).MustAsync(async (cliente, cancelacion) =>
                    !await _repositorios.Clientes.AnyAsync(e => e.Nombre == cliente.Nombre && e.Apellidos == cliente.Apellidos && e.Id != cliente.Id))
                .WithMessage("Ya existe un cliente con el mismo nombre y apellidos.");
            
        }
    }
}