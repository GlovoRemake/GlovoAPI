using Core.Dtos.Company;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces;

public interface ICompanyService
{
    Task<PagedRequestCompanyDto> GetAllRequests(RequestsPagedDto dto);
    Task<CompanyDto?> ApprovalRequest(ApprovalCompanyDto dto);
    Task<CompanyDto?> GetCompanyAsync(Guid companyId);
    Task UpdateCompanyAsync(UpdateCompanyDto dto);
}
