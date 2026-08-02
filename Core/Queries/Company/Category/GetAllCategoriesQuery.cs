using Core.Dtos;
using Core.Dtos.Company.Category;
using MediatR;

namespace Core.Queries.Company.Category;

public record GetAllCategoriesQuery(GetAllCategoriesDto dto)
    : IRequest<Result<List<CategoryDto>>>;
