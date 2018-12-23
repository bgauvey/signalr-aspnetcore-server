using System;
using Microsoft.AspNetCore.SignalR;
using slignaraspnetcoreserver.Repository;
using System.Threading.Tasks;

namespace slignaraspnetcoreserver.Hubs
{
    public class GaugeHub : Hub
    {
        private readonly IGaugeRepository _repository;

        public GaugeHub(IGaugeRepository repository)
        {
            _repository = repository;
        }

        public async Task GetGaugesData()
        {
            await Clients.All.SendAsync("GetGaugesData", _repository.Gauge);
        }
    }
}
