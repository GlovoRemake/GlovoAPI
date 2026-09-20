using Core.Dtos;
using Core.Interfaces;
using Core.Queries.Admin;
using MediatR;

namespace Core.Handlers.Admin;

public class GetAllRegionsQueryHandler : IRequestHandler<GetAllRegionsQuery, Result<List<RegionDto>>>
{
    private readonly IAdminService _adminService;

    public GetAllRegionsQueryHandler(IAdminService adminService)
    {
        _adminService = adminService;
    }
    public async Task<Result<List<RegionDto>>> Handle(GetAllRegionsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var regions = await _adminService.GetAllRegionsAsync();
            return Result<List<RegionDto>>.Success(regions);
        }
        catch (Exception ex)
        {
            return Result<List<RegionDto>>.Failure(new ErrorMessage("REGIONEXCEPTION", ex.Message));
        }
    }
}
