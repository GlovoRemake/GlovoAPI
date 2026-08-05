using Core.Dtos;
using MediatR;

namespace Core.Commands.Company;

public record DeleteCompanyIconCommand(Guid CompanyId)
    : IRequest<Result>;