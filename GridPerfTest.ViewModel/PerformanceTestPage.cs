namespace GridPerfTest.ViewModel;

public sealed class PerformanceTestPage
{
    public SourceItem[] SourceItems { get; }
    
    public int ParametersForPage { get; }

    public PerformanceTestPage(SourceItem[] sourceItems, int parametersForPage, bool templated)
    {
        SourceItems = sourceItems;
        ParametersForPage = parametersForPage;
    }

    public List<TimeSpan> Measurements { get; } = new();
}