using Core.Dtos;
using Core.Dtos.Account.Order;
using Core.Interfaces;
using Core.Queries.Account.Order;
using MediatR;

namespace Core.Handlers.Account.Order;

public class GetUserOrderHistoryQueryHandler : IRequestHandler<GetUserOrderHistoryQuery, Result<List<UserOrderDto>>>
{
    private readonly IOrderService _orderService;

    public GetUserOrderHistoryQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }
    public async Task<Result<List<UserOrderDto>>> Handle(GetUserOrderHistoryQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var orders = await _orderService.UserOrderHistory(request.userId);
            return Result<List<UserOrderDto>>.Success(orders);
        }
        catch (Exception ex)
        {
            return Result<List<UserOrderDto>>.Failure(new ErrorMessage("UNDEFINED ERROR", ex.Message));
        }
    }
}