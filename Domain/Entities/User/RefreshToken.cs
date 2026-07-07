using Core.Entities.Identity;
using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.User;

public class RefreshToken : BaseEntity<int>
{
    public string Token { get; set; } = null!;
    public Guid UserId { get; set; }

    public DateTime Expires { get; set; }
    public bool IsRevoked { get; set; }

    // conn
    public UserEntity User { get; set; }
}
