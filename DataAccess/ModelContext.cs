using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;


namespace DataAccess
{
    public class ModelContext : DbContext
    {
        public ModelContext(DbContextOptions options): base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        public DbSet<News> News { get; set; }

        public DbSet<CheckingSource> CheckingSources { get; set; }

    }
}
