using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using authApi.Models;

namespace authApi.Data
{
    public class authApiContext : DbContext
    {
        public authApiContext (DbContextOptions<authApiContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Course { get; set; }

        public DbSet<Lesson> Lesson { get; set; }

        public DbSet<Section> Section { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<WatchLog> WatchLog { get; set; }
    }
}
