using API.Data.Entidades.IslaAzul;
using API.Data.Entidades.Seguridad;
using API.Data.IUnitOfWorks.Interfaces;
using FluentValidation;

namespace API.Domain.Validators.Seguridad
{

    public class HabitacionAmaDeLlavesValidator : AbstractValidator<HabitacionAmaDeLLaves>
    {
        private readonly IUnitOfWork<HabitacionAmaDeLLaves> _repositorios;

        public HabitacionAmaDeLlavesValidator(IUnitOfWork<HabitacionAmaDeLLaves> repositorios)
        {
            _repositorios = repositorios;

            RuleFor(h => h.HabitacionId)
                .NotEmpty().WithMessage("El ID de la habitación es obligatorio.");

            RuleFor(h => h.AmaDeLlavesId)
                .NotEmpty().WithMessage("El ID del ama de llaves es obligatorio.");

        }
    }
}
