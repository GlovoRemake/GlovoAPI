using Core.Dtos;
using Core.Dtos.Promocods;
using MediatR;

namespace Core.Commands.Promocodes;

public record UpdatePromocodeCommand(Guid companyId, UpdatePromocodeDto dto)
    : IRequest<Result>;
