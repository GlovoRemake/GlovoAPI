using Core.Dtos;
using Core.Dtos.Company.Category;
using Core.Interfaces;
using Core.Queries.Company.Category;
using MediatR;

namespace Core.Handlers.Company.Category;

public sealed class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result<List<CategoryDto>>>
{
    private readonly ICompanyCategoryService _companyCategoryService;

    public GetAllCategoriesQueryHandler(ICompanyCategoryService companyCategoryService)
    {
        _companyCategoryService = companyCategoryService;
    }
    public async Task<Result<List<CategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var res = await _companyCategoryService.GetAllCompanyCategoriesAsync(request.dto);
            return Result<List<CategoryDto>>.Success(res);
        }
        catch (Exception ex)
        {
            return Result<List<CategoryDto>>.Failure(new ErrorMessage("GetAllCategoriesError", ex.Message));
        }
        
    }
}
