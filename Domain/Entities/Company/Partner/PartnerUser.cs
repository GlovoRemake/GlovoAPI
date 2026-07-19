using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities.Base;

namespace Domain.Entities.Company.Partner;

public class PartnerUser : BaseEntityWithIsDeleted<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string? Avatar { get; set; }
    public string PasswordHash { get; set; }

    public ICollection<Company> Companies { get; set; }
    public ICollection<Employee> Employees { get; set; }
}
