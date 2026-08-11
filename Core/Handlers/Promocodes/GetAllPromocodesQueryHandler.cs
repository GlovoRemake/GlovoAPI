using Core.Commands.Promocodes;
using Core.Dtos;
using Core.Dtos.Exceptions.Company;
using Core.Dtos.Promocods;
using Core.Interfaces;
using Core.Queries.Promocodes;
using MediatR;

namespace Core.Handlers.Promocodes;

public sealed class GetAllPromocodesQueryHandler : IRequestHandler<GetAllPromocodesQuery, Result<List<PromocodeDto>>>
{
    private readonly IPromocodeService _promocodeService;

    public GetAllPromocodesQueryHandler(IPromocodeService promocodeService)
    {
        _promocodeService = promocodeService;
    }
    public async Task<Result<List<PromocodeDto>>> Handle(GetAllPromocodesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var promocodes = await _promocodeService.GetAllPromocodesAsync(request.companyId);
            return Result<List<PromocodeDto>>.Success(promocodes);
        }
        catch (CompanyNotFoundException ex)
        {
            return Result<List<PromocodeDto>>.Failure(new ErrorMessage("Company not found", ex.Message));
        }
        catch (Exception ex)
        {
            return Result<List<PromocodeDto>>.Failure(new ErrorMessage("Unexpected error", ex.Message));
        }
    }
}
