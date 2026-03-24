using AutoSpaceTestTask.Application.Models.Dtos;
using AutoSpaceTestTask.Application.Services.Interfaces;

namespace AutoSpaceTestTask.Application.Services.Implemetations
{
    public class StoreManagementService : IStoreManagementService
    {
        public Task<StoreDetailsResponseDto> GetOpenStoreDetailsAsync(long storeId)
        {
            throw new NotImplementedException();
        }

        public Task<OpenStoreListResponseDto> GetOpenStoreListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<StoreListResponseDto> GetStoreListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductListResponseDto> GetStoreProductListAsync(long storeId)
        {
            throw new NotImplementedException();
        }

        public Task<StoreDetailsResponseDto> UpdateStoreAsync(UpdateStoreDto updateDto)
        {
            throw new NotImplementedException();
        }
    }
}
