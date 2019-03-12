using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbContextTest
{
    public class TestDbContext : DbContext
    {
        public Guid Id { get; set; }

        //public TestDbContext()
        //{
        //    Id = Guid.NewGuid();
        //}

        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
            Id = Guid.NewGuid();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        DbSet<Test> Tests { get; set; }
    }
}
