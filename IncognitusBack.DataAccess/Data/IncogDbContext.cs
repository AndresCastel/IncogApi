using IncognitusBack.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IncognitusBack.DataAccess.Data
{
    public class IncogDbContext : DbContext
    {
        public IncogDbContext(DbContextOptions<IncogDbContext> options) : base(options)
        {

        }
        public DbSet<Type_Register> Type_Register { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<RosterC> RosterC { get; set; }
        public DbSet<StuffType> StuffType { get; set; }
        public DbSet<Stuff> Stuff { get; set; }
        public DbSet<Stuff_Assign> Stuff_Assign { get; set; }
        public DbSet<Employee_Register> Employee_Register { get; set; }
        public DbQuery<TimesheetsReport> TimesheetsReport { get; set; }
       

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Query<TimesheetsReport>().ToView("TimesheetsReportView");

        }

    }
}
