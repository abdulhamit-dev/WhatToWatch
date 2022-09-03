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
            //modelBuilder.ApplyConfiguration(new )

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Name = "hamit",
                Surname = "yılmaz",
                UserName = "hamit",
                Password = "123"
            });
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 2,
                Name = "ayşe",
                Surname = "öztürk",
                UserName = "ayse",
                Password = "321"
            });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieNoteAndScore> MovieNoteAndScores { get; set; }
    }
}
