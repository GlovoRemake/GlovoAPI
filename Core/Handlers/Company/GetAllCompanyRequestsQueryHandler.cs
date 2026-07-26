using Core.Commands.Company;
using Core.Dtos;
using Core.Dtos.Company;
using Core.Dtos.Exceptions.Company;
using Core.Interfaces;
using Core.Queries.Company;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Company;

public sealed class GetAllCompanyRequestsQueryHandler
    : IRequestHandler<GetAllCompanyRequestsQuery, Result<PagedRequestCompanyDto>>
{
    private readonly ICompanyService _companyService;

    public GetAllCompanyRequestsQueryHandler(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    public async Task<Result<PagedRequestCompanyDto>> Handle(
        GetAllCompanyRequestsQuery request,
        CancellationToken cancellationToken)
    {
        

        return Result<PagedRequestCompanyDto>.Success(await _companyService.GetAllRequests(request.dto));
    }
}