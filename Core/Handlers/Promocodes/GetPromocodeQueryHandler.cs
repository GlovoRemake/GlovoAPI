using Core.Commands.Promocodes;
using Core.Dtos;
using Core.Dtos.Promocods;
using Core.Interfaces;
using Core.Queries.Promocodes;
using MediatR;

namespace Core.Handlers.Promocodes;

public sealed class GetPromocodeQueryHandler : IRequestHandler<GetPromocodeQuery, Result<PromocodeDto>>
{
    private readonly IPromocodeService _promocodeService;

    public GetPromocodeQueryHandler(IPromocodeService promocodeService)
    {
        _promocodeService = promocodeService;
    }
    public async Task<Result<PromocodeDto>> Handle(GetPromocodeQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var promocode = await _promocodeService.GetPromocodeAsync(request.id);
            return Result<PromocodeDto>.Success(promocode);
        }
        catch (InvalidOperationException ex)
        {
            return Result<PromocodeDto>.Failure(new ErrorMessage("Promocode not found", ex.Message));
        }
        catch (Exception ex)
        {
            return Result<PromocodeDto>.Failure(new ErrorMessage("Unexpected error", ex.Message));
        }
    }
}
