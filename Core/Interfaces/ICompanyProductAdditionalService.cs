using Core.Dtos.Company.Product.Additional;
using Core.Dtos.Company.Product.AdditionalGroup;
using Npgsql.PostgresTypes;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Core.Interfaces;

public interface ICompanyProductAdditionalService
{
    Task<List<AdditionalGroupDto>> GetAdditionalsGroup(int productId);
    Task<AdditionalGroupDto> CreateAdditionalGroup(int productId, CreateAdditionalGroupDto dto);
    Task<AdditionalGroupDto> UpdateAdditionalGroup(int additionalGroupId, UpdateAdditionalGroupDto dto);
    Task<bool> DeleteAdditionalGroup(int additionalGroupId);
    Task ReorderAdditionalGroup(int productId, ReorderAdditionalGroupDto dto);
}
