using System;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace cartservice;

// This code taken from the OpenTelemetryExample
public class Instrumentation : IDisposable
{
    
    public static string MeterName => "cartservice.meter";

    private readonly Meter meter;

    public Instrumentation()
    {
        string? version = typeof(Instrumentation).Assembly.GetName().Version?.ToString();

        meter = new Meter(MeterName, version);

        FreezingDaysCounter = meter.CreateCounter<long>("latexiron.days.freezing", "days", "The number of days where latexiron is cold");
        FreezingDaysCounter.Add(10);

        UpDownCounter = meter.CreateUpDownCounter<long>("latexiron.days.updown", "days", "The number of days where latexiron is up or down");
        UpDownCounter.Add(5);
    }

    public UpDownCounter<long> UpDownCounter  { get; }
    
    public Counter<long> FreezingDaysCounter { get; }

    public void Dispose()
    {
        meter.Dispose();
    }
}
