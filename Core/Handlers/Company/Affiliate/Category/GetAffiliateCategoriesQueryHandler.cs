using Core.Dtos;
using Core.Dtos.Company.Category;
using Core.Dtos.Company.Product;
using Core.Interfaces;
using Core.Queries.Company.Affiliate.Category;
using MediatR;

namespace Core.Handlers.Company.Affiliate.Category;


public sealed class GetAffiliateCategoriesQueryHandler
    : IRequestHandler<GetAffiliateCategoriesQuery, Result<List<CategoryDto>>>
{
    private readonly IAffiliateService _affiliateService;

    public GetAffiliateCategoriesQueryHandler(IAffiliateService affiliateService)
    {
        _affiliateService = affiliateService;
    }

    public async Task<Result<List<CategoryDto>>> Handle(
        GetAffiliateCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            return Result<List<CategoryDto>>.Success(await _affiliateService.GetAffiliateCategories(request.affiliateId));
        }
        catch (Exception ex)
        {
            return Result<List<CategoryDto>>.Failure(ErrorMessage.Create(
                "ServerError",
                $"An error occurred during registration: {ex.Message}"
            ));
        }
    }
}