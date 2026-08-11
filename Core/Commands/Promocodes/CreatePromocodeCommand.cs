using Core.Dtos;
using Core.Dtos.Promocods;
using MediatR;

namespace Core.Commands.Promocodes;

public record CreatePromocodeCommand(CreatePromocodeDto dto)
    : IRequest<Result>;
