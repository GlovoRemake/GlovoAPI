using Core.Dtos;
using Core.Dtos.Account;
using MediatR;

namespace Core.Commands.Account;

public record UpdateProfileCommand(string email, UpdateProfileDto dto)
    : IRequest<Result>;