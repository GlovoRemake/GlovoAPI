using Core.Entities.Identity;
using Domain.Entities.Base;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Courier;

public class CourierProfile : BaseEntityWithIsDeleted<int>
{
    public Guid UserId { get; set; }
    public double Balance { get; set; }
    public TransportType TransportType { get; set; }

    // conn
    public UserEntity User { get; set; } = default!;
}
