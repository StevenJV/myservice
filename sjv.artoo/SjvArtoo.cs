using System;
using MassTransit;
using Serilog;
using Topshelf;

namespace sjv.artoo
{
    public class SjvArtoo : ServiceControl
    {
        private readonly IBusControl _bus;

        public SjvArtoo(IBusControl bus)
        {
            _bus = bus;
        }

        public bool Start(HostControl hostControl)
        {
            _bus.Start();
            Log.Logger.Information($"sjv.artoo started at: {DateTime.UtcNow}");
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _bus?.Stop(TimeSpan.FromSeconds(30));
            Log.Logger.Information($"sjv.artoo stopped at: {DateTime.UtcNow}");
            return true;
        }
    }
}