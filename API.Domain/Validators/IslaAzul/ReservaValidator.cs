using API.Data.Entidades.IslaAzul;
using API.Data.Entidades.Seguridad;
using API.Data.IUnitOfWorks.Interfaces;
using FluentValidation;

namespace API.Domain.Validators.Seguridad
{
    /// <summary>
    /// Valida que los datos de la entidad esten correctos antes de ser insertados a la BD
    /// </summary>
    public class ReservaValidator : AbstractValidator<Reserva>
    {
        private readonly IUnitOfWork<Reserva> _repositorios;

        public ReservaValidator(IUnitOfWork<Reserva> repositorios)
        {
            _repositorios = repositorios;

            RuleFor(r => r.FechaEntrada)
                .GreaterThanOrEqualTo(DateTime.Now.Date)
                .WithMessage("La fecha de entrada debe ser igual o posterior a la fecha actual.");

            RuleFor(r => r.FechaSalida)
                .GreaterThanOrEqualTo(r => r.FechaEntrada.AddDays(2))
                .WithMessage("El periodo mínimo de reserva debe ser de tres días.");

       RuleFor(r => r.ClienteId)
                .NotEmpty().WithMessage("El cliente es obligatorio.");


            RuleFor(r => r.HabitacionId)
                .NotEmpty().WithMessage("La habitación es obligatoria.");

         
            RuleFor(r => r).MustAsync(async (reserva, cancelacion) =>
                {
                    return !await _repositorios.Habitaciones.AnyAsync(h => h.Id == reserva.HabitacionId && h.EstaFueraDeServicio);
                })
                .WithMessage("La habitación seleccionada está fuera de servicio.");
            
    }
}
}
