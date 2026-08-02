using Core.Dtos;
using Core.Dtos.Company.Affiliate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Commands.Company.Affiliate;

public record DeleteAffiliateCommand(Guid affiliateId)
    : IRequest<Result<bool>>;