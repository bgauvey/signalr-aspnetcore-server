using System;
using Microsoft.EntityFrameworkCore;
using slignaraspnetcoreserver.Models;

namespace slignaraspnetcoreserver.EF
{
    public class GaugeContext : DbContext
    {
        public GaugeContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<Gauge> Gauges { get; set; }
    }
}
