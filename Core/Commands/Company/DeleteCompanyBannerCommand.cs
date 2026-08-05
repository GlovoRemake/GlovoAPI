using Core.Dtos;
using MediatR;

namespace Core.Commands.Company;

public record DeleteCompanyBannerCommand(Guid CompanyId)
    : IRequest<Result>;