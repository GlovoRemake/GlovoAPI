using Core.Commands.Company;
using Core.Commands.Partner;
using Core.Dtos;
using Core.Dtos.Company;
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
    : IRequestHandler<ApprovalRequestCommand, Result<CompanyDto?>>
{
    private readonly ICompanyService _companyService;

    public ApprovalRequestCommandHandler(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    public async Task<Result<CompanyDto?>> Handle(
        ApprovalRequestCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            return Result<CompanyDto?>.Success(await _companyService.ApprovalRequest(request.dto));
        }
        catch (RequestNotFoundException)
        {
            return Result<CompanyDto?>.Failure(ErrorMessage.Create(
                "RequestNotFound",
                $"Запит не знайдено"
            ));
        }
        catch (Exception ex)
        {
            return Result<CompanyDto?>.Failure(ErrorMessage.Create(
                "ServerError",
                $"An error occurred during registration: {ex.Message}"
            ));
        }
    }
}