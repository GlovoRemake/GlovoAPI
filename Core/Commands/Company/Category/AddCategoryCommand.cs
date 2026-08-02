using Core.Dtos;
using Core.Dtos.Company.Category;
using MediatR;

namespace Core.Commands.Company.Category;

public record AddCategoryCommand(AddCategoryDto Dto)
    : IRequest<Result>;