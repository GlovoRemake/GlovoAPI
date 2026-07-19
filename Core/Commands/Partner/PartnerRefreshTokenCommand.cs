using Core.Dtos;
using Core.Dtos.Account;
using MediatR;

namespace Core.Commands.Partner;

public record PartnerRefreshTokenCommand(string refreshToken)
    : IRequest<Result<TokenResponseDto>>;