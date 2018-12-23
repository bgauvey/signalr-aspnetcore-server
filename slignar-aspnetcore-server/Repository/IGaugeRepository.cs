using System;
using slignaraspnetcoreserver.Models;

namespace slignaraspnetcoreserver.Repository
{
    public interface IGaugeRepository
    {
        Gauge Gauge { get; }
    }
}
