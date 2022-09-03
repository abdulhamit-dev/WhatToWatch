using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToWatch.Entities.Conrete;

namespace WhatToWatch.DataAccess.Concrete.Contexts
{
    public class WhatToWatchContext: DbContext
    {
     
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("WhatToWatchCon");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Name = "hamit",
                Surname = "yılmaz",
                UserName = "admin",
                Password = "123"
            });
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 2,
                Name = "hamit2",
                Surname = "yilmaz",
                UserName = "admin2",
                Password = "123"
            });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieNoteAndVote> MovieNoteAndVotes { get; set; }
    }
}
