using Core.Dtos;
using Core.Dtos.Account.Order;
using Core.Interfaces;
using Core.Queries.Company.Affiliate;
using Core.Queries.Courier;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Company.Affiliate;
public class GetAffiliateOrdersQueryHandler : IRequestHandler<GetAffiliateOrdersQuery, Result<List<UserOrderDto>>>
{
    private readonly IOrderService _orderService;

    public GetAffiliateOrdersQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }
    public async Task<Result<List<UserOrderDto>>> Handle(GetAffiliateOrdersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var orders = await _orderService.GetOrdersForPartners(request.affiliateId);
            return Result<List<UserOrderDto>>.Success(orders);
        }
        catch (Exception ex)
        {
            return Result<List<UserOrderDto>>.Failure(new ErrorMessage("ServerError", ex.Message));
        }
    }
}