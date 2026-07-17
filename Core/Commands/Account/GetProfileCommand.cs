using Core.Dtos;
using Core.Dtos.Account;
using MediatR;

namespace Core.Commands.Account;

public record GetProfileCommand(string userId)
    : IRequest<Result<GetProfileDto>>;