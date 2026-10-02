using Core.Dtos;
using Core.Dtos.Account;
using Core.Dtos.Account.Payment;
using Core.Dtos.Exceptions.Account;
using Core.Interfaces;
using Core.Queries.Account;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Account;

public sealed class GetUserPaymentsQueryHandler
    : IRequestHandler<GetUserPaymentsQuery, Result<UserPaymentsDto>>
{
    private readonly IAccountService _accountService;

    public GetUserPaymentsQueryHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }
    public async Task<Result<UserPaymentsDto>> Handle(GetUserPaymentsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            UserPaymentsDto res = await _accountService.GetUserPaymentsAsync(request.userId);
            return Result<UserPaymentsDto>.Success(res);
        }
        catch (Exception ex)
        {
            return Result<UserPaymentsDto>.Failure(ErrorMessage.Create(
                "Exception",
                ex.Message
            ));
        }
    }
}
