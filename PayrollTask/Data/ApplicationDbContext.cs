﻿using Microsoft.EntityFrameworkCore;

namespace PayrollTask.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}
}
