using Core.Dtos;
using Core.Dtos.Company;
using Core.Dtos.Company.Affiliate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Queries.Company.Affiliate;

public record GetAllAffiliatesQuery(Guid companyId, int pageNumber, int pageSize, Guid partnerId)
    : IRequest<Result<PagedAffiliatesDto>>;