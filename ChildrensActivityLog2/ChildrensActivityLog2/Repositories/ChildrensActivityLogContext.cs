﻿using ChildrensActivityLog2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildrensActivityLog2.Repositories
{
    public class ChildrensActivityLogContext : DbContext
    {
        public ChildrensActivityLogContext(DbContextOptions<ChildrensActivityLogContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Child> Children { get; set; }
        public DbSet<SleepingPeriod> SleepingPeriods { get; set; }
        public DbSet<PlayEvent> PlayEvents { get; set; }
        public DbSet<ChildrensPlayEvents> ChildrensPlayEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChildrensPlayEvents>()
                .HasKey(c => new { c.ChildId, c.PlayEventId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
