using Core.Commands.Company.Category;
using Core.Dtos;
using Core.Interfaces;
using MediatR;

namespace Core.Handlers.Company.Category;

public sealed class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result>
{
    private readonly ICompanyCategoryService _companyCategoryService;

    public DeleteCategoryCommandHandler(ICompanyCategoryService companyCategoryService)
    {
        _companyCategoryService = companyCategoryService;
    }
    public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _companyCategoryService.DeleteCompanyCategoriesAsync(request.dto);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new ErrorMessage("DeleteCategoryError", ex.Message));
        }
        
    }
}
