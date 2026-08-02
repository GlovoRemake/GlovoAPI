using Core.Dtos;
using Core.Dtos.Company.Category;
using MediatR;

namespace Core.Commands.Company.Category;

public record DeleteCategoryCommand(DeleteCategoryDto dto)
    : IRequest<Result>;
