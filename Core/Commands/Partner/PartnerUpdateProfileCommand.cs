using Core.Dtos;
using Core.Dtos.Partner;
using Domain.Entities.Company.Partner;
using MediatR;

namespace Core.Commands.Partner;

public record PartnerUpdateProfileCommand(Guid partnerId, PartnerUpdateDto dto)
    : IRequest<Result>;