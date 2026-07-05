using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities.Base;

namespace Domain.Entities.Company.Partner;

public class PartnerRole : BaseEntityWithIsDeleted<int>
{
    public string Name { get; set; }

    // conn
    public ICollection<Employee> Employees { get; set; }
}
