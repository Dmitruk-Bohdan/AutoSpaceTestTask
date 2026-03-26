using AutoSpaceTestTask.Application.Models;
using AutoSpaceTestTask.Application.Models.Dtos;

namespace AutoSpaceTestTask.Application.Services.Interfaces
{
    public interface IStoreManagementService
    {
        Task<ProductListResponseDto> GetStoreProductListAsync(long storeId);
        Task<ProductPreviewListResponseDto> GetProductPreviewListAsync();
        Task<StoreListResponseDto> GetStoreListAsync();
        Task<OpenStoreListResponseDto> GetOpenStoreListAsync();
        Task<OperationResult<StoreDetailsResponseDto>> GetStoreDetailsAsync(long storeId);
        Task<OperationResult<StoreDetailsForUpdateResponseDto>> GetStoreDetailsForUpdateAsync(long storeId);
        Task<OperationResult> UpdateStoreAsync(UpdateStoreDto updateDto);
    }
}
