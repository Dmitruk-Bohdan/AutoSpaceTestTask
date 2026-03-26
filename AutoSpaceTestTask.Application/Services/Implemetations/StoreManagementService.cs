using AutoSpaceTestTask.Application.Models;
using AutoSpaceTestTask.Application.Models.Dtos;
using AutoSpaceTestTask.Application.Models.Dtos.ProductDtos;
using AutoSpaceTestTask.Application.Models.Dtos.StoreDtos;
using AutoSpaceTestTask.Application.Services.Interfaces;
using AutoSpaceTestTask.Database.Context;
using AutoSpaceTestTask.Database.Entities;
using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AutoSpaceTestTask.Application.Services.Implemetations
{
    public class StoreManagementService : IStoreManagementService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<StoreManagementService> _logger;

        public StoreManagementService(AppDbContext context, ILogger<StoreManagementService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ListResponseDto<StoreDetailsResponseDto>> GetStoreListAsync()
        {
            var stores = await _context.Stores
                .AsNoTracking()
                .AsSplitQuery()
                .Select(s => new StoreDetailsResponseDto
                {
                    StoreId = s.Id,
                    Name = s.Name,
                    Code = s.Code,
                    Address = s.Address,
                    ProductsCount = s.StoreProducts.Count,
                    StoreSchedulesDto = s.Schedules
                    .Select(schedule => new StoreScheduleDto
                    {
                        OpenTime = schedule.OpenTime,
                        CloseTime = schedule.CloseTime,
                        DayOfWeek = schedule.DayOfWeek,
                        IsDayOff = schedule.IsDayOff
                    }).ToList()
                })
                .ToListAsync();
            var reponse = new ListResponseDto<StoreDetailsResponseDto>()
            {
                Items = stores
            };
            return reponse;
        }

        public async Task<OperationResult<StoreDetailsResponseDto>> GetStoreDetailsAsync(long storeId)
        {
            var storeDetails = await _context.Stores
                .AsNoTracking()
                .AsSplitQuery()
                .Where(s => s.Id == storeId)
                .Select(s => new StoreDetailsResponseDto
                {
                    StoreId = s.Id,
                    Name = s.Name,
                    Code = s.Code,
                    Address = s.Address,

                    ProductsCount = s.StoreProducts.Count,

                    StoreSchedulesDto = s.Schedules
                    .Select(schedule => new StoreScheduleDto
                        {
                            OpenTime = schedule.OpenTime,
                            CloseTime = schedule.CloseTime,
                            DayOfWeek = schedule.DayOfWeek,
                            IsDayOff = schedule.IsDayOff
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            var response = new OperationResult<StoreDetailsResponseDto>();
            if(storeDetails == null)
            {
                var errorMessage = $"Store with specified id '{storeId}' didn't found";
                response.ErrorMessage = errorMessage;
                _logger.LogError(errorMessage);
            }
            else
            {
                response.Payload = storeDetails;
            }

            return response;
        }

        public async Task<ListResponseDto<StorePreviewResponseDto>> GetOpenStoreListAsync()
        {
            var currentDayOfWeek = DateTime.Now.DayOfWeek;
            var currentTime = TimeOnly.FromDateTime(DateTime.Now);



            var openStores = await _context.StoreSchedules
                .AsNoTracking()
                .AsSplitQuery()
                .Where(ss =>
                    ss.DayOfWeek == currentDayOfWeek &&
                    !ss.IsDayOff &&
                    (
                        (ss.OpenTime <= currentTime && ss.CloseTime >= currentTime)
                        || (ss.OpenTime > ss.CloseTime &&
                            (currentTime >= ss.OpenTime || currentTime <= ss.CloseTime))
                    )
                )
                .Select(s => new StorePreviewResponseDto
                {
                    StoreId = s.Store.Id,
                    Name = s.Store.Name,
                    Address = s.Store.Address,
                    TodaySchedule = new StoreScheduleDto
                    {
                        OpenTime = s.OpenTime,
                        CloseTime = s.CloseTime,
                        DayOfWeek = s.DayOfWeek,
                        IsDayOff = s.IsDayOff
                    }
                })
                .ToListAsync();

            var response = new ListResponseDto<StorePreviewResponseDto>()
            {
                Items = openStores
            };

            return response;
        }

        public async Task<ListResponseDto<ProductDetailsResponseDto>> GetStoreProductListAsync(long storeId)
        {
            var storeProducts = await _context.StoreProducts
                .AsNoTracking()
                .AsSplitQuery()
                .Where(sp => sp.StoreId == storeId)
                .Select(sp => new ProductDetailsResponseDto
                {
                    Code = sp.Product.Code,
                    Article = sp.Product.Article,
                    Name = sp.Product.Name,
                    Brand = sp.Product.Brand,
                    GroupName = sp.Product.Group.Name
                })
                .ToListAsync();

            var response = new ListResponseDto<ProductDetailsResponseDto>()
            {
                Items = storeProducts
            };

            return response;
        }

        public async Task<OperationResult> UpdateStoreAsync(UpdateStoreDto updateDto)
        {
            _logger.LogInformation("Updating store {StoreId} started", updateDto.StoreId);

            var updatedStore = await _context.Stores
                .Where(s => s.Id == updateDto.StoreId)
                .FirstOrDefaultAsync();

            if(updatedStore == null)
            {
                var errorMessage = $"Store with specified id '{updateDto.StoreId}' didn't found";
                _logger.LogError(errorMessage);

                return new OperationResult()
                {
                    ErrorMessage = errorMessage
                };
            }

            _logger.LogInformation("Updating store {StoreId} started", updateDto.StoreId);

            updatedStore.Name = updateDto.Name;
            updatedStore.Address = updateDto.Address;


            var oldStoreProducts = await _context.StoreProducts
                .Where(sp => sp.StoreId == updateDto.StoreId)
                .ToListAsync();
            _context.StoreProducts.RemoveRange(oldStoreProducts);
            _logger.LogInformation("Removed {Count} old product relations for Store {StoreId}", oldStoreProducts.Count, updateDto.StoreId);

            var newStoreProducts = updateDto.StoreProductIds
                .Select(productId => new StoreProduct
                {
                    StoreId = updateDto.StoreId,
                    ProductId = productId
                }) ?? Enumerable.Empty<StoreProduct>();
            _context.StoreProducts.AddRange(newStoreProducts);
            _logger.LogInformation("Added {Count} new product relations for Store {StoreId}", updateDto.StoreProductIds.Count, updateDto.StoreId);

            var oldSchedules = await _context.StoreSchedules
                .Where(os => os.StoreId == updateDto.StoreId)
                .ToListAsync();
            _context.StoreSchedules.RemoveRange(oldSchedules);

            var schedules = updateDto.StoreSchedulesDto.Select(ssd => new StoreSchedule
            {
                StoreId = updateDto.StoreId,
                OpenTime = ssd.OpenTime,
                CloseTime = ssd.CloseTime,
                DayOfWeek = ssd.DayOfWeek,
                IsDayOff = ssd.IsDayOff
            });
            await _context.StoreSchedules.AddRangeAsync(schedules);
            
            await _context.SaveChangesAsync();

            return new OperationResult();
        }

        public async Task<ListResponseDto<ProductPreviewResponseDto>> GetProductPreviewListAsync()
        {
            var products = await _context.Products.Select(p => new ProductPreviewResponseDto()
            {
                ProductId = p.Id,
                Article = p.Article,
                Name = p.Name,
            }).ToListAsync();

            var response = new ListResponseDto<ProductPreviewResponseDto>()
            {
                Items = products
            };
            return response;
        }

        public async Task<OperationResult<StoreDetailsForUpdateResponseDto>> GetStoreDetailsForUpdateAsync(long storeId)
        {
            var storeDetailsResponse = await GetStoreDetailsAsync(storeId);
            if(storeDetailsResponse.IsSucceess)
            {
                var storeProductIds = await _context.StoreProducts
                    .Where(sp => sp.StoreId == storeId)
                    .Select(sp => sp.ProductId)
                    .ToListAsync();

                var updateDto = new StoreDetailsForUpdateResponseDto
                {
                    StoreId = storeDetailsResponse.Payload!.StoreId,
                    Code = storeDetailsResponse.Payload.Code,
                    Name = storeDetailsResponse.Payload.Name,
                    Address = storeDetailsResponse.Payload.Address,
                    ProductsCount = storeDetailsResponse.Payload.ProductsCount,
                    StoreSchedulesDto = storeDetailsResponse.Payload.StoreSchedulesDto,
                    StoreProductIds = storeProductIds
                };
                return new OperationResult<StoreDetailsForUpdateResponseDto>()
                {
                    Payload = updateDto
                };
            }
            else
            {
                return new OperationResult<StoreDetailsForUpdateResponseDto>()
                {
                    ErrorMessage = storeDetailsResponse.ErrorMessage
                };
            }
        }
    }
}
