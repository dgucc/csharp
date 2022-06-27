using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;

namespace RazorPagesMovies.Data {
    public class RazorPagesMoviesContext : DbContext {
#pragma warning disable CS8618
        public RazorPagesMoviesContext(DbContextOptions<RazorPagesMoviesContext> options)
#pragma warning restore CS8618
            : base(options) {
        }

        public DbSet<RazorPagesMovie.Models.Movie>? Movie { get; set; }
    }
}
