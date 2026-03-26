using AutoSpaceTestTask.Application.Models;
using AutoSpaceTestTask.Application.Models.Dtos;
using AutoSpaceTestTask.Application.Models.Dtos.ProductDtos;
using AutoSpaceTestTask.Application.Models.Dtos.StoreDtos;

namespace AutoSpaceTestTask.Application.Services.Interfaces
{
    public interface IStoreManagementService
    {
        Task<ListResponseDto<ProductDetailsResponseDto>> GetStoreProductListAsync(long storeId);
        Task<ListResponseDto<ProductPreviewResponseDto>> GetProductPreviewListAsync();
        Task<ListResponseDto<StoreDetailsResponseDto>> GetStoreListAsync();
        Task<ListResponseDto<StorePreviewResponseDto>> GetOpenStoreListAsync();
        Task<OperationResult<StoreDetailsResponseDto>> GetStoreDetailsAsync(long storeId);
        Task<OperationResult<StoreDetailsForUpdateResponseDto>> GetStoreDetailsForUpdateAsync(long storeId);
        Task<OperationResult> UpdateStoreAsync(UpdateStoreDto updateDto);
    }
}
