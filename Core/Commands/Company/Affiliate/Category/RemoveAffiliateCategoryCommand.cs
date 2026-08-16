using Core.Dtos;
using MediatR;

namespace Core.Commands.Company.Affiliate.Category;

public record RemoveAffiliateCategoryCommand(Guid affiliateId, int categoryId)
    : IRequest<Result>;