using Core.Dtos;
using Core.Dtos.Partner;
using Core.Interfaces;
using Core.Queries.Company.Affiliate;
using MediatR;

namespace Core.Handlers.Company.Affiliate;

public sealed class GetAffiliateManagersQueryHandler
    : IRequestHandler<GetAffiliateManagersQuery, Result<List<GetPartnerProfileDto>>>
{
    private readonly IAffiliateService _affiliateService;

    public GetAffiliateManagersQueryHandler(IAffiliateService affiliateService)
    {
        _affiliateService = affiliateService;
    }

    public async Task<Result<List<GetPartnerProfileDto>>> Handle(
        GetAffiliateManagersQuery request,
        CancellationToken cancellationToken)
    {
        return Result<List<GetPartnerProfileDto>>.Success(await _affiliateService.GetManagers(request.affiliateId));
    }
}