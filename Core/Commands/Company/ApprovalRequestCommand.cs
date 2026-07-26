using Core.Dtos;
using Core.Dtos.Account;
using Core.Dtos.Company;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Commands.Company;

public record ApprovalRequestCommand(ApprovalCompanyDto dto)
    : IRequest<Result<CompanyDto?>>;