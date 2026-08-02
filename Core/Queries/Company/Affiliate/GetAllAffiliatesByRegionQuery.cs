using Core.Dtos;
using Core.Dtos.Company.Affiliate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Queries.Company.Affiliate;

public record GetAllAffiliatesByRegionQuery(Guid companyId, int cityId)
    : IRequest<Result<AffiliateDto[]>>;