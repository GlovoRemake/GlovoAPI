using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities.Base;

namespace Domain.Entities.Company;

public class CompanyType : BaseEntityWithIsDeleted<int>
{
    public string Name { get; set; }
    public string IconPath { get; set; }

    // conn
    public ICollection<Company> Companies { get; set; }
}
