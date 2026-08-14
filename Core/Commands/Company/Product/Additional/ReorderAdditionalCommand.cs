using Core.Dtos;
using Core.Dtos.Company.Product.AdditionalGroup;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Commands.Company.Product.Additional;

public record ReorderAdditionalCommand(int productId, ReorderAdditionalGroupDto dto)
    : IRequest<Result>;