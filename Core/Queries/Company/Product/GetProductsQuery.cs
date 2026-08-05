using Core.Dtos;
using Core.Dtos.Company.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Queries.Company.Product;

public record GetProductsQuery(Guid CompanyId, int CategoryId)
    : IRequest<Result<List<ProductDto>>>;