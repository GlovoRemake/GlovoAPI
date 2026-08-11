using AutoMapper;
using Core.Dtos.Exceptions.Company;
using Core.Dtos.Promocods;
using Core.Interfaces;
using Domain.Entities;
using Domain.Entities.Company;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class PromocodeService(
    ISoftDeleteRepository<Promocode, int> _promocodeRepository,
    ISoftDeleteRepository<Company, Guid> _companyRepository,
    IMapper _mapper) : IPromocodeService
{
    public async Task CreatePromocodeAsync(Guid? companyId, CreatePromocodeDto dto)
    {
        if (dto == null)
        {
            throw new InvalidDataException("Відсутня інформація про промокод!");
        }
        if (companyId != null && !await _companyRepository.Query().AnyAsync(c => c.Id == companyId))
        {
            throw new CompanyNotFoundException("Компанія не знайдена!");
        }
        var promocode = _mapper.Map<Promocode>(dto);
        await _promocodeRepository.AddAsync(promocode);
    }

    public async Task DeletePromocodeAsync(int id)
    {
        var promocode = await _promocodeRepository.GetByIdAsync(id);
        if (promocode == null)
        {
            throw new InvalidOperationException("Промокод не знайдено!");
        }
        promocode.IsActive = false;
        await _promocodeRepository.DeleteAsync(id);
    }

    public async Task<PromocodeDto> GetPromocodeAsync(int id)
    {
        var entity = await _promocodeRepository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new InvalidOperationException("Промокод не знайдено!");
        }
        var promocodeDto = _mapper.Map<PromocodeDto>(entity);
        return promocodeDto;
    }

    public async Task<List<PromocodeDto>> GetAllPromocodesAsync(Guid companyId)
    {
        if (!await _companyRepository.Query().AnyAsync(c => c.Id == companyId))
        {
            throw new CompanyNotFoundException("Компанія не знайдена!");
        }
        var entities = await _promocodeRepository.Query().Where(p => p.CompanyId == companyId && !p.IsDeleted).ToListAsync();
        var promocodeDtos = _mapper.Map<List<PromocodeDto>>(entities);
        return promocodeDtos;
    }

    public async Task UpdatePromocodeAsync(Guid? companyId, UpdatePromocodeDto dto)
    {
        var entity = _mapper.Map<Promocode>(dto);
        if (companyId != null && !await _companyRepository.Query().AnyAsync(c => c.Id == companyId))
        {
            throw new CompanyNotFoundException("Компанія не знайдена!");
        }
        await _promocodeRepository.UpdateAsync(entity);
    }
}
