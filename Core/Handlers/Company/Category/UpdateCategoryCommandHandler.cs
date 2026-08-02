using Core.Commands.Company.Category;
using Core.Dtos;
using Core.Dtos.Exceptions.Company.Category;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Company.Category;

public sealed class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result>
{
    private readonly ICompanyCategoryService _companyCategoryService;

    public UpdateCategoryCommandHandler(ICompanyCategoryService companyCategoryService)
    {
        _companyCategoryService = companyCategoryService;
    }
    public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _companyCategoryService.UpdateCompanyCategoriesAsync(request.Dto);
            return Result.Success();
        }
        catch (CategoryNotFoundException ex)
        {
            return Result.Failure(new ErrorMessage("CategoryNotFound", ex.Message));
        }
        catch (Exception ex)
        {
            return Result.Failure(new ErrorMessage("UpdateCategoryError", ex.Message));
        }
        
    }
}
