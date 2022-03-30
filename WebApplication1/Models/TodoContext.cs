using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        public DbSet<WebApplication1.Models.Pays> Pays { get; set; }

        public DbSet<WebApplication1.Models.Population> Population { get; set; }

        public DbSet<WebApplication1.Models.Individu> Individu { get; set; }
    }
}
