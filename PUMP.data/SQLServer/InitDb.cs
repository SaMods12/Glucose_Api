using Microsoft.EntityFrameworkCore;
using PUMP.helpers;
using PUMP.models;
using System.Numerics;

namespace PUMP.data.SQLServer;

public class InitDb : DbContext
{

    public virtual DbSet<Users> Users { get; set; }

    public virtual DbSet<Doctor> Doctor { get; set; }

    public virtual DbSet<Patient> Patient { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(Settings.ConnectionString,
                builder => { builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null); });
        }
    }
}