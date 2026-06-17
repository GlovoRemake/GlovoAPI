using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Data;

public class GlovoDbContext : IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>
{
    public GlovoDbContext(DbContextOptions<GlovoDbContext> options) : base(options) { }
}