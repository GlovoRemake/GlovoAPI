using Core.Dtos;
using Core.Dtos.Account;
using MediatR;

namespace Core.Commands.Partner;

public record VerifyPartnerCodeCommand(VerifyCodeDto dto)
    : IRequest<Result<TokenResponseDto>>;