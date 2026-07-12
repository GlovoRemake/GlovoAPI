using Core.Dtos.Account;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Commands.Account;

public record VerifyCodeCommand(VerifyCodeDto dto)
    : IRequest<TokenResponseDto>;
