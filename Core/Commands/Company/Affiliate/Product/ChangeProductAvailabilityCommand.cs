using Core.Dtos;
using MediatR;

namespace Core.Commands.Company.Affiliate.Category;

public record ChangeProductAvailabilityCommand(Guid affiliateId, int productId, bool isAvailable)
    : IRequest<Result>;