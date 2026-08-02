using Core.Commands.Company.Category;
using Core.Dtos;
using Core.Dtos.Exceptions.Company.Category;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Company.Category;

public sealed class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, Result>
{
    private readonly ICompanyCategoryService _companyCategoryService;

    public AddCategoryCommandHandler(ICompanyCategoryService companyCategoryService)
    {
        _companyCategoryService = companyCategoryService;
    }
    public async Task<Result> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _companyCategoryService.AddCompanyCategoriesAsync(request.Dto);
            return Result.Success();
        }
        catch (CategoryAlreadyExistsException ex)
        {
            return Result.Failure(new ErrorMessage("CategoryAlreadyExists", ex.Message));
        }
        catch (Exception ex)
        {
            return Result.Failure(new ErrorMessage("AddCategoryError", ex.Message));
        }
    }
}
