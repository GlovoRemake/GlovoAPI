using Core.Dtos;
using Core.Dtos.Account;
using MediatR;

namespace Core.Commands.Account;

public record VerifyResetCodeCommand(VerifyCodeDto dto)
    : IRequest<Result<TokenResponseDto>>;
