using Core.Commands.Company.Affiliate.Category;
using Core.Dtos;
using Core.Dtos.Exceptions.Company.Affiliate;
using Core.Dtos.Exceptions.Company.Product;
using Core.Interfaces;
using MediatR;

namespace Core.Handlers.Company.Affiliate.Category;


public sealed class RemoveAffiliateCategoryCommandHandler
    : IRequestHandler<RemoveAffiliateCategoryCommand, Result>
{
    private readonly IAffiliateService _affiliateService;

    public RemoveAffiliateCategoryCommandHandler(IAffiliateService affiliateService)
    {
        _affiliateService = affiliateService;
    }

    public async Task<Result> Handle(
        RemoveAffiliateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            await _affiliateService.RemoveCategory(request.affiliateId, request.categoryId);
            return Result.Success();
        }
        catch (ProductNotFoundException)
        {
            return Result.Failure(ErrorMessage.Create(
                "ProductNotFound",
                $"Product not found"
            ));
        }
        catch (Exception ex)
        {
            return Result.Failure(ErrorMessage.Create(
                "ServerError",
                $"An error occurred during registration: {ex.Message}"
            ));
        }
    }
}