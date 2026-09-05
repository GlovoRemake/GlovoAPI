using Core.Dtos;
using Core.Dtos.Company.Affiliate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Queries.Company.Affiliate;

public record GetAffiliateByIdQuery(Guid affiliateId)
    : IRequest<Result<AffiliateDto>>;