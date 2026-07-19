using Core.Dtos;
using Core.Dtos.Account;
using MediatR;

namespace Core.Queries.Account;

public record GetProfileQuery(string userId)
    : IRequest<Result<GetProfileDto>>;