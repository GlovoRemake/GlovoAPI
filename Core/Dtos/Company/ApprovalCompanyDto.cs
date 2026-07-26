using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Company;

public class ApprovalCompanyDto
{
    public long RequestId { get; set; }
    public bool IsApprove { get; set; }
}
