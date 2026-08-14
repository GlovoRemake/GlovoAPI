using Core.Dtos;
using Core.Dtos.Company.Product;
using Core.Dtos.Company.Product.AdditionalGroup;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Queries.Company.Product.Additional;

public record GetAdditionalsQuery(int productId)
    : IRequest<Result<List<AdditionalGroupDto>>>;