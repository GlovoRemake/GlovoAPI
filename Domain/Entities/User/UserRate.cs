using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Identity;
using Domain.Entities.Base;
using Domain.Entities.Order;
using Domain.Entities.Company;

namespace Domain.Entities.User;

public class UserRate : BaseEntity<int>
{
    public int OrderId { get; set; }
    public Guid UserId { get; set; }
    public Guid CompanyId { get; set; }
    public bool IsPositively { get; set; }

    // conn
    public UserOrder Order { get; set; }
    public UserEntity User { get; set; }
    public Company.Company Company { get; set; }
}