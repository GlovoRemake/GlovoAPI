using Core.Dtos;
using Core.Dtos.Account;
using MediatR;

namespace Core.Commands.Partner;

public record PartnerLoginCommand(string email, string password)
    : IRequest<Result<TokenResponseDto>>;