using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Company;

public class RequestsPagedDto
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public bool IsApproval { get; set; }
}
