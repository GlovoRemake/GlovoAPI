using Core.Dtos;
using Core.Dtos.Company.Category;
using MediatR;

namespace Core.Queries.Company.Affiliate.Category;

public record GetAffiliateCategoriesQuery(Guid affiliateId)
    : IRequest<Result<List<CategoryDto>>>;