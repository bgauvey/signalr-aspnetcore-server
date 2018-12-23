using slignaraspnetcoreserver.EF;
using slignaraspnetcoreserver.Models;
using System;
using System.Linq;

namespace slignaraspnetcoreserver.Repository
{
    public class GaugeRepository : IGaugeRepository
    {
        private Func<GaugeContext> _contextFactory;

        public Gauge Gauge => GetGauge();

        public GaugeRepository(Func<GaugeContext> context)
        {
            _contextFactory = context;
        }

        private Gauge GetGauge()
        {
            using (var context = _contextFactory.Invoke())
            {
                return context.Gauges.FirstOrDefault();
            }
        }
    }
}
