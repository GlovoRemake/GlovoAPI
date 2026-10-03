using Core.Dtos;
using Core.Dtos.Account.Order;
using MediatR;

namespace Core.Queries.Account.Order;

public record GetUserOrderHistoryQuery(Guid userId)
    : IRequest<Result<List<UserOrderDto>>>;