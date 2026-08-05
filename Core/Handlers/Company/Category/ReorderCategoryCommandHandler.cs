using Core.Commands.Company.Category;
using Core.Dtos;
using Core.Interfaces;
using MediatR;

namespace Core.Handlers.Company.Category;

public class ReorderCategoryCommandHandler : IRequestHandler<ReorderCategoryCommand, Result>
{
    private readonly ICompanyCategoryService _companyCategoryService;

    public ReorderCategoryCommandHandler(ICompanyCategoryService companyCategoryService)
    {
        _companyCategoryService = companyCategoryService;
    }
    public async Task<Result> Handle(ReorderCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _companyCategoryService.ReorderCompanyCategoriesAsync(request.dto);
            return Result.Success();
        }
        catch (InvalidDataException ex)
        {
            return Result.Failure(new ErrorMessage("InvalidData", ex.Message));
        }
        catch (Exception ex)
        {
            return Result.Failure(new ErrorMessage("ServerError", ex.Message));
        }
    }
}
