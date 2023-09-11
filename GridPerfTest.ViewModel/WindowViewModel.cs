using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace GridPerfTest.ViewModel;

public sealed class WindowViewModel : INotifyPropertyChanged
{
    public WindowViewModel()
    {
        SourceData = Enumerable.Range(0, 1000).Select(_ => new SourceItem()).ToArray();

        Pages = new(
            from templated in new[] {false, true}
            from parameters in new[] {64, 32, 16, 8}
            select new PerformanceTestPage(SourceData, parameters, templated)
        );

        ResetMeasurementsCommand = new SimpleCommand(ResetMeasurements);
        StartAutoMeasurementsCommand = new SimpleCommand(StartAutoMeasurements);
        StopAutoMeasurementsCommand = new SimpleCommand(StopAutoMeasurements);
        CollectResultsCommand = new SimpleCommand(CollectResults);
    }

    public SourceItem[] SourceData { get; }

    public ObservableCollection<PerformanceTestPage> Pages { get; }

    private PerformanceTestPage? _selectedPage;

    public PerformanceTestPage? SelectedPage
    {
        get => _selectedPage;
        set => SetField(ref _selectedPage, value);
    }


    public ICommand ResetMeasurementsCommand { get; }
    
    private void ResetMeasurements()
    {
        foreach (var page in Pages)
        {
            page.Measurements.Clear();
        }
    }

    
    public ICommand StartAutoMeasurementsCommand { get; }
    
    public ICommand StopAutoMeasurementsCommand { get; }
    
    public bool AutoMeasurementsIsRunning
    {
        get => _autoMeasurementsIsRunning;
        set => SetField(ref _autoMeasurementsIsRunning, value);
    }


    private bool _stopAutoMeasurements = false;
    private bool _autoMeasurementsIsRunning = false;

    private async void StartAutoMeasurements()
    {
        if (AutoMeasurementsIsRunning)
        {
            return;
        }

        AutoMeasurementsIsRunning = true;
        CollectedResults = null;
        
        try
        {
            _stopAutoMeasurements = false;
            ResetMeasurements();
            await Task.Delay(500);
            for (int i = 0; i < 10; ++i)
            {
                foreach (var page in Pages)
                {
                    if (_stopAutoMeasurements)
                    {
                        return;
                    }

                    SelectedPage = page;
                    await Task.Delay(1000);
                }
            }
        }
        finally
        {
            AutoMeasurementsIsRunning = false;
            CollectResults();
        }
    }

    private void StopAutoMeasurements()
    {
        _stopAutoMeasurements = true;
    }


    public ICommand CollectResultsCommand { get; }

    private string? _collectedResults;

    public string? CollectedResults
    {
        get => _collectedResults;
        set => SetField(ref _collectedResults, value);
    }

    private void CollectResults()
    {
        CollectedResults = String.Join("\t",
            Pages.SelectMany(static p => new[]
            {
                p.AverageMeasurement.TotalMilliseconds.ToString("0") ?? "",
                p.MeasurementDeviation.TotalMilliseconds.ToString("0.0") ?? ""
            }));
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}