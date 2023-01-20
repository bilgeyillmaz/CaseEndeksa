using Endeksa.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Endeksa.Repository
{
    public class TkgmDbContext : DbContext
    {
        public TkgmDbContext(DbContextOptions<TkgmDbContext> options) : base(options)
        {
        }

        public DbSet<Root> Roots { get; set; }

        //public DbSet<Properties> PropertiesTable { get; set; }
        //public DbSet<Geometry> Geometries { get; set; }



        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<Geometry>().HasNoKey();
        //    //modelBuilder.Entity<Properties>().HasNoKey();
        //    modelBuilder.Entity<Geometry>(builder =>
        //    {
        //        builder.HasNoKey();
        //        builder.ToTable("Geometries");
        //    });
        //    modelBuilder.Entity<Properties>(builder =>
        //    {
        //        builder.HasNoKey();
        //        builder.ToTable("PropertiesTable");
        //    });

        //    base.OnModelCreating(modelBuilder);

        //}

    }
}
