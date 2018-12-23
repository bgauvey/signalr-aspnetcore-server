using System;
using Microsoft.AspNetCore.SignalR;
using slignaraspnetcoreserver.Hubs;
using slignaraspnetcoreserver.Models;
using slignaraspnetcoreserver.Repository;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;
using TableDependency.SqlClient;

namespace slignaraspnetcoreserver.SqlTableDependencies
{
    public class GaugeDatabaseSubscription : IDatabaseSubscription
    {
        private bool disposedValue = false;
        private readonly IGaugeRepository _repository;
        private readonly IHubContext<GaugeHub> _hubContext;
        private SqlTableDependency<Gauge> _tableDependency;

        public GaugeDatabaseSubscription(IGaugeRepository repository, IHubContext<GaugeHub> hubContext)
        {
            _repository = repository;
            _hubContext = hubContext;
        }

        public void Configure(string connectionString)
        {
            _tableDependency = new SqlTableDependency<Gauge>(connectionString, null, null, null, null, null, DmlTriggerType.All);
            _tableDependency.OnChanged += Changed;
            _tableDependency.OnError += TableDependency_OnError;
            _tableDependency.Start();

            Console.WriteLine("Waiting for receiving notifications...");
        }

        private void TableDependency_OnError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine($"SqlTableDependency error: {e.Error.Message}");
        }

        private void Changed(object sender, RecordChangedEventArgs<Gauge> e)
        {
            if (e.ChangeType != ChangeType.None)
            {
                // TODO: manage the changed entity
                var changedEntity = e.Entity;
                _hubContext.Clients.All.SendAsync("GetGaugesData", _repository.Gauge);
            }
        }

        #region IDisposable

        ~GaugeDatabaseSubscription()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _tableDependency.Stop();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
