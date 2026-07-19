using Core.Dtos;
using Core.Dtos.Partner;
using MediatR;

namespace Core.Commands.Partner;

public record PartnerRegisterCommand(PartnerRegisterDto dto)
    : IRequest<Result>;