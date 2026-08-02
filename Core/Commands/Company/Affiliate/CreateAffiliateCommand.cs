using Core.Dtos;
using Core.Dtos.Company;
using Core.Dtos.Company.Affiliate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Commands.Company.Affiliate;
public record CreateAffiliateCommand(Guid companyId, CreateAffiliateDto dto)
    : IRequest<Result<AffiliateDto>>;