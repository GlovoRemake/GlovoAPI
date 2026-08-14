using Core.Dtos.Company.Product.AdditionalGroup;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services;

public class CompanyProductAdditionalService : ICompanyProductAdditionalService
{
    public Task<List<AdditionalGroupDto>> GetAdditionalsGroup(int additionalGroupId)
    {
        throw new NotImplementedException();
    }

    public Task<AdditionalGroupDto> UpdateAdditionalGroup(int additionalGroupId, UpdateAdditionalGroupDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<AdditionalGroupDto> CreateAdditionalGroup(int productId, CreateAdditionalGroupDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAdditionalGroup(int additionalGroupId)
    {
        throw new NotImplementedException();
    }

    public Task<AdditionalGroupDto> ReorderAdditionalGroup(int additionalGroupId, ReorderAdditionalGroupDto dto)
    {
        throw new NotImplementedException();
    }
}
