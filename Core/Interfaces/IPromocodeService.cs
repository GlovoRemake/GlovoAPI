using Core.Dtos.Promocods;

namespace Core.Interfaces;

public interface IPromocodeService
{
    Task CreatePromocodeAsync(Guid? companyId, CreatePromocodeDto dto);
    Task<PromocodeDto> GetPromocodeAsync(int id);
    Task<List<PromocodeDto>> GetAllPromocodesAsync(Guid companyId);
    Task UpdatePromocodeAsync(Guid? companyId, UpdatePromocodeDto dto);
    Task DeletePromocodeAsync(int id);
}
