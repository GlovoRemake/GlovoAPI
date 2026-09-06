using Core.Commands.Partner;
using Core.Dtos;
using Core.Dtos.Exceptions.Account;
using Core.Interfaces;
using MediatR;

namespace Core.Handlers.Partner;

public class PartnerUpdateProfileCommandHandler : IRequestHandler<PartnerUpdateProfileCommand, Result>
{
    private readonly IPartnerService _partnerService;

    public PartnerUpdateProfileCommandHandler(IPartnerService partnerService)
    {
        _partnerService = partnerService;
    }
    public async Task<Result> Handle(PartnerUpdateProfileCommand request, CancellationToken cancellationToken)
    {

        try
        {
            await _partnerService.UpdatePartnerProfile(request.partnerId, request.dto);
            return Result.Success();
        }
        catch (UserNotFoundException e)
        {
            return Result.Failure(new ErrorMessage("UserNotFoundException", e.Message));
        }
        catch (Exception e)
        {
            return Result.Failure(new ErrorMessage("INTERNALERROR", e.Message));
        }
    }
}