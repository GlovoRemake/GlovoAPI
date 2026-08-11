using Core.Dtos.Promocods;

namespace Core.Interfaces;

public interface IPromocodeService
{
    Task CreatePromocodeAsync(CreatePromocodeDto dto);
    Task<PromocodeDto> GetPromocodeAsync(int id);
    Task<List<PromocodeDto>> GetAllPromocodesAsync(Guid companyId);
    Task UpdatePromocodeAsync(UpdatePromocodeDto dto);
    Task DeletePromocodeAsync(int id);
}
