using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Company;

public class CompanyType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string IconPath { get; set; }

    // conn
    public ICollection<Company> Companies { get; set; }
}
