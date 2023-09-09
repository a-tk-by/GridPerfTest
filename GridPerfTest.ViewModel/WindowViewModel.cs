namespace GridPerfTest.ViewModel;

public sealed class WindowViewModel
{
    public WindowViewModel()
    {
        SourceData = Enumerable.Range(0, 1000).Select(_ => new SourceItem()).ToArray();

        Pages = (
            from templated in new[] {false, true}
            from parameters in new[] {8, 16, 32, 64}
            select new PerformanceTestPage(SourceData, parameters, templated)
        ).ToArray();
    }

    public SourceItem[] SourceData { get; }

    public PerformanceTestPage[] Pages { get; }
}

