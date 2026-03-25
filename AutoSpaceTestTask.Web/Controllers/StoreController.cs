using AutoSpaceTestTask.Application.Models.Dtos;
using AutoSpaceTestTask.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
            var storeDetails = await _storeService.GetStoreDetailsAsync(storeId);

            var products = await _storeService.GetStoreProductListAsync(storeId);

            return Json(new { store = storeDetails});
        }

        [HttpGet]
        [Route("{storeId:long}/products")]
        public async Task<IActionResult> Products([FromRoute] long storeId)
        {
            var productList = await _storeService.GetStoreProductListAsync(storeId);
            return PartialView("_ProductList", productList);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> UpdateStore([FromBody] UpdateStoreDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _storeService.UpdateStoreAsync(updateDto);
                var updatedStore = await _storeService.GetStoreDetailsAsync(updateDto.StoreId);
                return Json(new { success = true, store = updatedStore });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
