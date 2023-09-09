using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace GridPerfTest.ViewModel;

public sealed class PerformanceTestPage : INotifyPropertyChanged
{
    public SourceItem[] SourceItems { get; }
    
    public int ParametersForPage { get; }
    
    public bool TemplatedColumns { get; }

    public PerformanceTestPage(SourceItem[] sourceItems, int parametersForPage, bool templatedColumns)
    {
        SourceItems = sourceItems;
        ParametersForPage = parametersForPage;
        TemplatedColumns = templatedColumns;

        Measurements.CollectionChanged += (sender, args) =>
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AverageMeasurement)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MeasurementDeviation)));
        };
    }

    public ObservableCollection<TimeSpan> Measurements { get; } = new();

    public TimeSpan? AverageMeasurement => Measurements.Count == 0 ? null : Measurements.Aggregate(default(TimeSpan), (a, b) => a + b) / Measurements.Count;

    public TimeSpan? MeasurementDeviation => GetDeviation(Measurements);

    private TimeSpan? GetDeviation(ObservableCollection<TimeSpan> measurements)
    {
        var n = measurements.Count;
        if (n < 2) return null;

        var (s1, s2) = measurements.Select(x => x.TotalMilliseconds)
            .Select(x => (s1: x, s2: x * x))
            .Aggregate((s1: 0.0, s2: 0.0), (a, b) => (a.s1 + b.s1, a.s2 + b.s2));
        var (a1, a2) = (s1 / n, s2 / n);

        var d = Math.Sqrt(a2 - a1 * a1);

        return TimeSpan.FromMilliseconds(d);
    }

    public override string ToString() => $"{ParametersForPage} {(TemplatedColumns ? "Templated" : "Plain")}";

    public Stopwatch? Stopwatch { get; set; }
    
    public event PropertyChangedEventHandler? PropertyChanged;
}