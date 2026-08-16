using Core.Dtos;
using MediatR;

namespace Core.Commands.Company.Affiliate.Category;

public record AddAffiliateProductCommand(Guid affiliateId, int productId)
    : IRequest<Result>;