using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Data;

public class GlovoDbContext : DbContext
{
    public GlovoDbContext(DbContextOptions<GlovoDbContext> options) : base(options) { }
}