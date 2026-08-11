using Core.Dtos;
using Core.Dtos.Promocods;
using MediatR;

namespace Core.Queries.Promocodes;

public record GetPromocodeQuery(int id)
    : IRequest<Result<PromocodeDto>>;