using Core.Commands.Promocodes;
using Core.Dtos;
using Core.Dtos.Exceptions.Company;
using Core.Interfaces;
using MediatR;

namespace Core.Handlers.Promocodes;

public sealed class CreatePromocodeCommandHandler : IRequestHandler<CreatePromocodeCommand, Result>
{
    private readonly IPromocodeService _promocodeService;

    public CreatePromocodeCommandHandler(IPromocodeService promocodeService)
    {
        _promocodeService = promocodeService;
    }
    public async Task<Result> Handle(CreatePromocodeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _promocodeService.CreatePromocodeAsync(request.companyId, request.dto);
            return Result.Success();
        }
        catch (InvalidDataException ex)
        {
            return Result.Failure(new ErrorMessage("Invalid promocode data", ex.Message));
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
