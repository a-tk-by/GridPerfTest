namespace GridPerfTest.ViewModel;

public sealed class ItemValue
{
    public ItemValue(double value)
    {
        Value = value;
    }
    
    public double Value { get; }

    public bool AboveNormal => Value > 0.8;

    public bool BelowNormal => Value < 0.2;
}