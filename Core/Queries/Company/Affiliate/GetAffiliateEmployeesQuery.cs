using Core.Dtos;
using Core.Dtos.Partner;
using MediatR;

namespace Core.Queries.Company.Affiliate;


public record GetAffiliateEmployeesQuery(Guid affiliateId)
    : IRequest<Result<List<GetPartnerProfileDto>>>;