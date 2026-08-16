using Core.Dtos;
using Core.Dtos.Company.Product;
using MediatR;

namespace Core.Queries.Company.Affiliate.Product;


public record GetAffiliateProductsQuery(Guid affiliateId)
    : IRequest<Result<List<ProductDto>>>;