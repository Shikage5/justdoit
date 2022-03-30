using JustDoIt.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustDoIt.Data
{
    public class JustDoItDBContext : DbContext
    {
        public DbSet<ToDoItem> ToDoItems { get; set; }= null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=G02DEXN05630;initial catalog=JustDoItDb;trusted_connection=true");
        }

    }
}