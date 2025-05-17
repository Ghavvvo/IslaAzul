using API.Data.Entidades.IslaAzul;
using API.Data.Entidades.Seguridad;
using API.Data.IUnitOfWorks.Interfaces;
using FluentValidation;

namespace API.Domain.Validators.Seguridad
{
    /// <summary>
    /// Valida que los datos de la entidad esten correctos antes de ser insertados a la BD
    /// </summary>
    public class HabitacionValidator : AbstractValidator<Habitacion>
    {
        private readonly IUnitOfWork<Habitacion> _repositorios;

        public HabitacionValidator(IUnitOfWork<Habitacion> repositorios)
        {
            _repositorios = repositorios;

           /* RuleFor(h => h.Numero).NotEmpty().WithMessage("El número de la habitación no puede estar vacío.")
                .MaximumLength(10).WithMessage($"El número de la habitación debe tener como máximo 10 caracteres.")
                .NotNull().WithMessage("El número de la habitación es obligatorio."); */

            RuleFor(h => h).MustAsync(async (habitacion, cancelacion) => 
                    !await _repositorios.Habitaciones.AnyAsync(e => e.Numero == habitacion.Numero && e.Id != habitacion.Id))
                .WithMessage("Ya existe una habitación con el mismo número.");
        }
    }
}
