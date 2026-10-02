using Core.Dtos;
using Core.Dtos.Account.Order;
using Core.Dtos.Partner;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Queries.Company.Affiliate;

public record GetAffiliateOrdersQuery(Guid affiliateId)
    : IRequest<Result<List<UserOrderDto>>>;