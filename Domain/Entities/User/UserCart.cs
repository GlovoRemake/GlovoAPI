using Core.Entities.Identity;
using Domain.Entities.Company.Affiliate;
using Domain.Entities.Company.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.User;

public class UserCart
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int ProductId { get; set; }
    public int Count { get; set; }
    public Guid AffiliateId { get; set; }

    // conn
    public UserEntity User { get; set; }
    public CompanyProduct Product { get; set; }
    public CompanyAffiliate Affiliate { get; set; }
    public ICollection<UserCartAdditional>? Additionals { get; set; }
}
