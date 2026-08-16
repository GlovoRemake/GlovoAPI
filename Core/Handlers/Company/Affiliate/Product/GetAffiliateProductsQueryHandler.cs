using Core.Dtos;
using Core.Dtos.Company.Product;
using Core.Interfaces;
using Core.Queries.Company.Affiliate.Category;
using Core.Queries.Company.Affiliate.Product;
using MediatR;

namespace Core.Handlers.Company.Affiliate.Product;


public sealed class GetAffiliateProductsQueryHandler
    : IRequestHandler<GetAffiliateProductsQuery, Result<List<ProductDto>>>
{
    private readonly IAffiliateService _affiliateService;

    public GetAffiliateProductsQueryHandler(IAffiliateService affiliateService)
    {
        _affiliateService = affiliateService;
    }

    public async Task<Result<List<ProductDto>>> Handle(
        GetAffiliateProductsQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            return Result<List<ProductDto>>.Success(await _affiliateService.GetAffiliateProducts(request.affiliateId));
        }
        catch (Exception ex)
        {
            return Result<List<ProductDto>>.Failure(ErrorMessage.Create(
                "ServerError",
                $"An error occurred during registration: {ex.Message}"
            ));
        }
    }
}