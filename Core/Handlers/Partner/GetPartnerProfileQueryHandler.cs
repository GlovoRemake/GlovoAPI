using Core.Dtos;
using Core.Dtos.Account;
using Core.Dtos.Exceptions.Account;
using Core.Dtos.Partner;
using Core.Interfaces;
using Core.Queries.Partner;
using MediatR;

namespace Core.Handlers.Partner;

public class GetPartnerProfileQueryHandler : IRequestHandler<GetPartnerProfileQuery, Result<GetPartnerProfileDto>>
{
    private readonly IPartnerService _partnerService;

    public GetPartnerProfileQueryHandler(IPartnerService partnerService)
    {
        _partnerService = partnerService;
    }
    public async Task<Result<GetPartnerProfileDto>> Handle(GetPartnerProfileQuery request, CancellationToken cancellationToken)
    {
        try
        {
            GetPartnerProfileDto res = await _partnerService.GetPartnerProfile(request.partnerUserId);
            return Result<GetPartnerProfileDto>.Success(res);
        }
        catch (UserNotFoundException ex)
        {
            return Result<GetPartnerProfileDto>.Failure(ErrorMessage.Create(
                "PartnerUserNotFound",
                ex.Message
            ));
        }
        catch (InvalidJwtTokenException ex)
        {
            return Result<GetPartnerProfileDto>.Failure(ErrorMessage.Create(
                "InvalidJwtToken",
                ex.Message
            ));
        }
        catch (Exception ex)
        {
            return Result<GetPartnerProfileDto>.Failure(ErrorMessage.Create(
                "Exception",
                ex.Message
            ));
        }
    }
}