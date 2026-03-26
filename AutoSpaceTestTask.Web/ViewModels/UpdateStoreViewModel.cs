using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AutoSpaceTestTask.Web.ViewModels
{
    public class UpdateStoreViewModel
    {
        public long StoreId { get; set; }

        [Required(ErrorMessage = "Name required")]
        [StringLength(200, ErrorMessage = "Name should be max 200 characters")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "Address required")]
        [StringLength(300, ErrorMessage = "Address should be max 300 characters")]
        public string Address { get; set; } = default!;

        public List<ScheduleItemViewModel> ScheduleItems { get; set; } = default!;
        public List<SelectListItem> AvailableProducts { get; set; } = new();
        public List<long> SelectedProductIds { get; set; } = new(); 
    }

    public class ScheduleItemViewModel
    {
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }
        public bool IsWorkingDay { get; set; } = true;
    }
}