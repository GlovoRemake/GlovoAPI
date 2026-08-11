using Core.Commands.Promocodes;
using Core.Dtos;
using Core.Dtos.Exceptions.Company;
using Core.Interfaces;
using MediatR;

namespace Core.Handlers.Promocodes;

public sealed class UpdatePromocodeCommandHandler : IRequestHandler<UpdatePromocodeCommand, Result>
{
    private readonly IPromocodeService _promocodeService;

    public UpdatePromocodeCommandHandler(IPromocodeService promocodeService)
    {
        _promocodeService = promocodeService;
    }
    public async Task<Result> Handle(UpdatePromocodeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _promocodeService.UpdatePromocodeAsync(request.companyId, request.dto);
            return Result.Success();
        }
        catch (CompanyNotFoundException ex)
        {
            return Result.Failure(new ErrorMessage("Company not found", ex.Message));
        }
        catch (Exception ex)
        {
            return Result.Failure(new ErrorMessage("Unexpected error", ex.Message));
        }
    }
}
