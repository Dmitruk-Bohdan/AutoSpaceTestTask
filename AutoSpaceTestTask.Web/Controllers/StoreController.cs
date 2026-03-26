using AutoSpaceTestTask.Application.Models.Dtos.StoreDtos;
using AutoSpaceTestTask.Application.Services.Interfaces;
using AutoSpaceTestTask.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoSpaceTestTask.Web.Controllers
{
    [Route("store")]
    public class StoreController : Controller
    {
        private readonly IStoreManagementService _storeService;
        private readonly ILogger<StoreController> _logger;

        public StoreController(IStoreManagementService storeService, ILogger<StoreController> logger)
        {
            _storeService = storeService;
            _logger = logger;
        }

        [HttpGet]
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            var storeList = await _storeService.GetStoreListAsync();
            return View(storeList);
        }

        [HttpGet]
        [Route("open")]
        public async Task<IActionResult> GetOpenStores()
        {
            var openStores = await _storeService.GetOpenStoreListAsync();
            return PartialView("_OpenStoresList", openStores);
        }

        [HttpGet]
        [Route("{storeId:long}/details")]
        public async Task<IActionResult> GetStoreDetails([FromRoute] long storeId)
        {
            var storeResult = await _storeService.GetStoreDetailsAsync(storeId);

            if (storeResult.Payload == null)
            {
                return NotFound();
            }

            var singleStoreList = new List<StoreDetailsResponseDto> { storeResult.Payload };
            return PartialView("_StoresTable", singleStoreList);
        }

        [HttpGet]
        [Route("{storeId:long}/products")]
        public async Task<IActionResult> Products([FromRoute] long storeId)
        {
            var productList = await _storeService.GetStoreProductListAsync(storeId);
            return PartialView("_ProductsTable", productList);
        }

        [HttpGet]
        [Route("{storeId:long}/edit")]
        public async Task<IActionResult> GetStoreEditInfo(long storeId)
        {
            var storeResult = await _storeService.GetStoreDetailsForUpdateAsync(storeId);
            if (!storeResult.IsSucceess)
                return NotFound();

            var scheduleItems = storeResult.Payload!.StoreSchedulesDto
                .Select(s => new ScheduleItemViewModel
                {
                    DayOfWeek = s.DayOfWeek,
                    Start = s.OpenTime,
                    End = s.CloseTime,
                    IsWorkingDay = !s.IsDayOff
                }).ToList();

            var allProducts = await _storeService.GetProductPreviewListAsync();
            var availableProducts = allProducts.Items
                .Select(p => new SelectListItem { Value = p.ProductId.ToString(), Text = $"{p.Article} -- {p.Name}"})
                .ToList();

            var selectedProductIds = storeResult.Payload.StoreProductIds ?? new List<long>();

            var vm = new UpdateStoreViewModel
            {
                StoreId = storeResult.Payload.StoreId,
                Name = storeResult.Payload.Name,
                Address = storeResult.Payload.Address,
                ScheduleItems = scheduleItems,
                AvailableProducts = availableProducts,
                SelectedProductIds = selectedProductIds
            };

            return PartialView("_EditStoreModal", vm);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateStore([FromBody] UpdateStoreDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                });
            }

            try
            {
                await _storeService.UpdateStoreAsync(dto);

                var updatedStoreResult = await _storeService.GetStoreDetailsAsync(dto.StoreId);
                
                if (!updatedStoreResult.IsSucceess)
                {
                    return NotFound();
                }

                return Ok(new
                {
                    success = true,
                    store = updatedStoreResult.Payload
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Internal server error"
                });
            }
        }
    }
}
