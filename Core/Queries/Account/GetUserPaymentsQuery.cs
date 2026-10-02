using Core.Dtos;
using Core.Dtos.Account;
using Core.Dtos.Account.Payment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Queries.Account;

public record GetUserPaymentsQuery(Guid userId)
    : IRequest<Result<UserPaymentsDto>>;