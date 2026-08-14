using Core.Dtos;
using Core.Dtos.Company.Product;
using Core.Dtos.Company.Product.Additional;
using Core.Dtos.Company.Product.AdditionalGroup;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Commands.Company.Product.Additional;

public record CreateAdditionalCommand(int productId, CreateAdditionalGroupDto dto)
    : IRequest<Result<AdditionalGroupDto>>;