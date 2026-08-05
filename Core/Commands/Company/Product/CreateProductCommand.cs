using Core.Dtos;
using Core.Dtos.Company.Category;
using Core.Dtos.Company.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Commands.Company.Product;

public record CreateProductCommand(Guid CompanyId, CreateProductDto Dto)
    : IRequest<Result<ProductDto>>;
