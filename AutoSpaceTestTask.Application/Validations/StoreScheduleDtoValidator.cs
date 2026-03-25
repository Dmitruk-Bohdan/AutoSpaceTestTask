using AutoSpaceTestTask.Application.Models.Dtos;
using FluentValidation;

namespace AutoSpaceTestTask.Application.Validations
{
    public class StoreScheduleDtoValidator : AbstractValidator<StoreScheduleDto>
    {
        public StoreScheduleDtoValidator()
        {
            RuleFor(x => x.DayOfWeek)
                .IsInEnum().WithMessage("Invalid day of week");

            When(x => x.IsDayOff, () =>
            {
                RuleFor(x => x.OpenTime)
                    .Equal(TimeOnly.MinValue)
                    .WithMessage("Open time should be default for day off");

                RuleFor(x => x.CloseTime)
                    .Equal(TimeOnly.MinValue)
                    .WithMessage("Close time should be default for day off");
            });

            When(x => !x.IsDayOff, () =>
            {
                RuleFor(x => x.OpenTime)
                    .NotEqual(TimeOnly.MinValue)
                    .WithMessage("Open time is required for working day");

                RuleFor(x => x.CloseTime)
                    .NotEqual(TimeOnly.MinValue)
                    .WithMessage("Close time is required for working day");

                RuleFor(x => x.CloseTime)
                    .GreaterThan(x => x.OpenTime)
                    .WithMessage("Close time must be greater than open time");

            });
        }
    }
}