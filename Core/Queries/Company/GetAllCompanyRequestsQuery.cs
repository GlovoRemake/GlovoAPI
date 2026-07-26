using Core.Dtos;
using Core.Dtos.Account;
using Core.Dtos.Company;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Queries.Company;

public record GetAllCompanyRequestsQuery(RequestsPagedDto dto)
    : IRequest<Result<PagedRequestCompanyDto>>;