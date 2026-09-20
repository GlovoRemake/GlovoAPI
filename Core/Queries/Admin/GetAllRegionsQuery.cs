using Core.Dtos;
using MediatR;

namespace Core.Queries.Admin;

public record GetAllRegionsQuery
    : IRequest<Result<List<RegionDto>>> { }
