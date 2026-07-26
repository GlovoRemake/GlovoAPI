using Core.Commands.Company;
using Core.Commands.Partner;
using Core.Dtos;
using Core.Dtos.Exceptions.Account;
using Core.Dtos.Exceptions.Company;
using Core.Interfaces;
using Core.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Company;

public sealed class ApprovalRequestCommandHandler
    : IRequestHandler<ApprovalRequestCommand, Result>
{
    private readonly ICompanyService _companyService;

    public ApprovalRequestCommandHandler(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    public async Task<Result> Handle(
        ApprovalRequestCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            await _companyService.ApprovalRequest(request.dto);
        }
        catch (RequestNotFoundException)
        {
            return Result.Failure(ErrorMessage.Create(
                "RequestNotFound",
                $"Запит не знайдено"
            ));
        }
        catch (Exception ex)
        {
            return Result.Failure(ErrorMessage.Create(
                "ServerError",
                $"An error occurred during registration: {ex.Message}"
            ));
        }

        return Result.Success();
    }
}