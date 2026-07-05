using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Company.Partner;

public class PartnerRole
{
    public int Id { get; set; }
    public string Name { get; set; }

    // conn
    public ICollection<Employee> Employees { get; set; }
}
