using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Company;

public class PagedRequestCompanyDto
{
    public List<RequestCompanyDto> Requests { get; set; } = [];
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}
