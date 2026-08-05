using AutoMapper;
using AutoMapper.Internal.Mappers;
using Core.Dtos.Company;
using Core.Dtos.Exceptions.Company;
using Core.Interfaces;
using Domain.Entities.Company;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services;

public class CompanyService(
        IRepository<RequestCompany, long> _requestCompanyRepo,
        ISoftDeleteRepository<Company, Guid> _companyRepo,
        IMapper _mapper
    ) : ICompanyService
{
    public async Task<PagedRequestCompanyDto> GetAllRequests(RequestsPagedDto dto)
    {
        var (requests, totalCount) = await _requestCompanyRepo.ListPagedAsync(
            dto.PageNumber,
            dto.PageSize,
            predicate: x => true,
            orderBy: q => q
               .OrderBy(x => x.IsApprove != null)
               .ThenBy(x => x.Name)
        );

        var requestDtos = _mapper.Map<List<RequestCompanyDto>>(requests);

        return new PagedRequestCompanyDto
        {
            Requests = requestDtos,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)dto.PageSize)
        };
    }

    public async Task<CompanyDto?> ApprovalRequest(ApprovalCompanyDto dto)
    {
        var request = await _requestCompanyRepo.Query().FirstOrDefaultAsync(x => x.Id == dto.RequestId);
        if (request == null) throw new RequestNotFoundException();

        if (dto.IsApprove)
        {
            var company = _mapper.Map<Company>(request);
            company.DateCreated = DateTime.UtcNow;
            company.DateUpdated = DateTime.UtcNow;
            await _companyRepo.AddAsync(company);
            request.CompanyId = company.Id;

            request.IsApprove = true;
            await _requestCompanyRepo.UpdateAsync(request);

            return _mapper.Map<CompanyDto>(company);
        }
        else
        {
            request.Message = dto.Message;
            request.IsApprove = false;
            await _requestCompanyRepo.UpdateAsync(request);

            return null;
        }
    }

    public async Task<CompanyDto?> GetCompanyAsync(Guid companyId)
    {
        var company = await _companyRepo.Query().FirstOrDefaultAsync(x => x.Id == companyId);
        return company == null ? null : _mapper.Map<CompanyDto>(company);
    }
}
