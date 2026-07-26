using Core.Dtos;
using Core.Dtos.Account;
using MediatR;

namespace Core.Commands.Account;

public record ForgotPasswordCommand(ForgotPasswordDto dto)
    : IRequest<Result>;
