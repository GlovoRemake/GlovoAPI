using Core.Dtos;
using Core.Dtos.Company.Product.AdditionalGroup;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Commands.Company.Product.Additional;

public record UpdateAdditionalCommand(int additionalGroupId, UpdateAdditionalGroupDto dto)
    : IRequest<Result<AdditionalGroupDto>>;