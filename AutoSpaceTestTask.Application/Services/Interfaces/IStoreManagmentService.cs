using AutoSpaceTestTask.Application.Models.Dtos;

namespace AutoSpaceTestTask.Application.Services.Interfaces
{
    public interface IStoreManagementService
    {
        Task<ProductListResponseDto> GetStoreProductListAsync(long storeId);
        Task<StoreListResponseDto> GetStoreListAsync();
        Task<OpenStoreListResponseDto> GetOpenStoreListAsync();
        Task<StoreDetailsResponseDto> GetOpenStoreDetailsAsync(long storeId);
        Task<StoreDetailsResponseDto> UpdateStoreAsync(UpdateStoreDto updateDto);
    }
}
