using Core.Dtos;
using Core.Dtos.Account;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Commands.Account;

public record LoginCommand(UserLoginDto dto)
    : IRequest<Result<TokenResponseDto>>;
