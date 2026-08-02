using AutoMapper;
using Core.Dtos.Company.Affiliate.WorkingHours;
using Domain.Entities.Company.Affiliate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Mappers;

public class WorkingHoursMapper : Profile
{
    public WorkingHoursMapper()
    {
        CreateMap<CompanyAffiliatesWorkingHour, WorkingHoursDto>();
    }
}
