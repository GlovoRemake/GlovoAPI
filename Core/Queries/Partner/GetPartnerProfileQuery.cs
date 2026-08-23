using Core.Dtos;
using Core.Dtos.Partner;
using MediatR;

namespace Core.Queries.Partner;

public record GetPartnerProfileQuery(string partnerUserId)
    : IRequest<Result<GetPartnerProfileDto>>;