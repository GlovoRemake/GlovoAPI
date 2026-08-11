using Core.Commands.Promocodes;
using Core.Dtos;
using Core.Interfaces;
using MediatR;

namespace Core.Handlers.Promocodes;

public sealed class DeletePromocodeCommandHandler : IRequestHandler<DeletePromocodeCommand, Result>
{
    private readonly IPromocodeService _promocodeService;

    public DeletePromocodeCommandHandler(IPromocodeService promocodeService)
    {
        _promocodeService = promocodeService;
    }
    public async Task<Result> Handle(DeletePromocodeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _promocodeService.DeletePromocodeAsync(request.id);
            return Result.Success();
        }
        catch (InvalidOperationException ex)
        {
            return Result.Failure(new ErrorMessage("Promocode not found", ex.Message));
        }
        catch (Exception ex)
        {
            return Result.Failure(new ErrorMessage("Unexpected error", ex.Message));
        }
    }
}
