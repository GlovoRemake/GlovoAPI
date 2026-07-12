using Core.Dtos.Account;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Commands.Account;

public record LoginCommand(string email, string password)
    : IRequest<TokenResponseDto>;
