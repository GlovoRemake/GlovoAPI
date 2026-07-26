using Core.Dtos;
using Core.Dtos.Account;
using MediatR;

namespace Core.Commands.Account;

public record SetNewPasswordCommand(string email, SetNewPasswordDto dto)
    : IRequest<Result>;
