using Core.Dtos;
using MediatR;

namespace Core.Commands.Promocodes;

public record DeletePromocodeCommand(int id)
    : IRequest<Result>;
