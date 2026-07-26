using Core.Dtos;
using Core.Dtos.Company;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Commands.Partner;

public record SendRequestCompanyCommand(string email, AddRequestCompanyDto dto)
    : IRequest<Result>;