using Core.Dtos;
using Core.Dtos.Partner;
using Core.Interfaces;
using Core.Queries.Company.Affiliate;
using MediatR;

namespace Core.Handlers.Company.Affiliate;

public sealed class GetAffiliateEmployeesQueryHandler
    : IRequestHandler<GetAffiliateEmployeesQuery, Result<List<GetPartnerProfileDto>>>
{
    private readonly IAffiliateService _affiliateService;

    public GetAffiliateEmployeesQueryHandler(IAffiliateService affiliateService)
    {
        _affiliateService = affiliateService;
    }

    public async Task<Result<List<GetPartnerProfileDto>>> Handle(
        GetAffiliateEmployeesQuery request,
        CancellationToken cancellationToken)
    {
        return Result<List<GetPartnerProfileDto>>.Success(await _affiliateService.GetEmployees(request.affiliateId));
    }
}