using DOUListener.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DOUListener.Data
{
	public class ListenerDbContext : DbContext
	{
        public DbSet<PostInfo> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-82U51T3\MSSQLSERVER2; Database=DOUPosts; Trusted_Connection=True; MultipleActiveResultSets=true");
        }
    }
}
