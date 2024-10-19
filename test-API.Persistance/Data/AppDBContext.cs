using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using test_API.Domain.Entities;

namespace test_API.Data
{
    public class AppDBContext : DbContext
    {
        public DbSet<Candidate> Candidates { get; set; }
        public AppDBContext(DbContextOptions<AppDBContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
          
        }
    }
}
