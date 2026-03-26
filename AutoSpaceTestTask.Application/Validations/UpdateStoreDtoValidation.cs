using AutoSpaceTestTask.Application.Extensions;
using AutoSpaceTestTask.Application.Models.Dtos.StoreDtos;
using FluentValidation;

namespace AutoSpaceTestTask.Application.Validations
{
    internal class UpdateStoreDtoValidation : AbstractValidator<UpdateStoreDto>
    {
        public UpdateStoreDtoValidation()
        {
            RuleFor(usd => usd.StoreId)
                .GreaterThan(0);

            RuleFor(usd => usd.Name)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(usd => usd.Address)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(usd => usd.StoreSchedulesDto)
                .NotNull().WithMessage("Schedules collection cannot be null")
                .Must(ssd => ssd.Count == 7).WithMessage("All days of week schedules must be provided")
                .Must(HaveUniqueDays).WithMessage("Duplicate days of week are not allowed");

            RuleForEach(x => x.StoreSchedulesDto)
                .SetValidator(new StoreScheduleDtoValidator());

            RuleFor(x => x.StoreProductIds)
                .NotNull().WithMessage("Product id collection cannot be null")
                .Must(HaveUniqueIds)
                .WithMessage("Duplicate product IDs are not allowed");

            RuleForEach(x => x.StoreProductIds)
                .GreaterThan(0);
        }

        private bool HaveUniqueDays(List<StoreScheduleDto> schedules)
        {
            return schedules.Select(s => s.DayOfWeek).Distinct().Count() == schedules.Count;
        }

        private bool HaveUniqueIds(List<long> ids)
        {
            return ids.Distinct().Count() == ids.Count;
        }
    }
}
