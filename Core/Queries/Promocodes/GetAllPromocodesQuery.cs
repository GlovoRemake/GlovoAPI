using Core.Dtos;
using Core.Dtos.Promocods;
using MediatR;

namespace Core.Queries.Promocodes;

public record GetAllPromocodesQuery(Guid companyId)
    : IRequest<Result<List<PromocodeDto>>>;