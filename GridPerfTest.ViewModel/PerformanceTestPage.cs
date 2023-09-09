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

        Measurements.CollectionChanged += (sender, args) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AverageMeasurement)));
    }

    public ObservableCollection<TimeSpan> Measurements { get; } = new();

    public TimeSpan? AverageMeasurement => Measurements.Count == 0 ? null : Measurements.Aggregate(default(TimeSpan), (a, b) => a + b) / Measurements.Count;

    public override string ToString() => $"{ParametersForPage} {(TemplatedColumns ? "Templated" : "Plain")}";

    public Stopwatch? Stopwatch { get; set; }
    
    public event PropertyChangedEventHandler? PropertyChanged;
}