using Core.Dtos.Account.Address;
using Core.Dtos.Company.Affiliate;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Account.Order;

public class UserOrderDto
{
    public int Id { get; set; }
    public GetProfileDto User { get; set; }
    public Company.CompanyDto Company { get; set; }
    public AffiliateDto Affiliate { get; set; }
    public AddressDto UserLocation { get; set; }
    public double TotalPrice { get; set; }
    public double ProductsPrice { get; set; }
    public double DeliveryFee { get; set; }
    public double Fee { get; set; }
    public double TipPercent { get; set; }
    public double TipAmount { get; set; }
    public OrderStatus Status { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public GetProfileDto Courier { get; set; }

    public List<UserOrderProductDto> Products { get; set; } = [];
}
