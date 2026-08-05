using Core.Dtos;
using Core.Dtos.Company.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Commands.Company.Product;

public record UpdateProductCommand(int ProductId, UpdateProductDto Dto)
    : IRequest<Result<ProductDto>>;