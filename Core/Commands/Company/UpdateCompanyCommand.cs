using Core.Dtos;
using Core.Dtos.Company;
using MediatR;

namespace Core.Commands.Company;

public record UpdateCompanyCommand(UpdateCompanyDto dto)
    : IRequest<Result>;