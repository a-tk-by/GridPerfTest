using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Xaml.Interactivity;

namespace GridPerfTest.Avalonia;

public class DataGridFillColumns : Behavior<DataGrid>
{
    private bool _generateTemplatedColumns;
    public static readonly DirectProperty<DataGridFillColumns, bool> GenerateTemplatedColumnsProperty = AvaloniaProperty.RegisterDirect<DataGridFillColumns, bool>("GenerateTemplatedColumns", o => o.GenerateTemplatedColumns, (o, v) => o.GenerateTemplatedColumns = v);
    
    private int _parametersCount;
    public static readonly DirectProperty<DataGridFillColumns, int> ParametersCountProperty = AvaloniaProperty.RegisterDirect<DataGridFillColumns, int>("ParametersCount", o => o.ParametersCount, (o, v) => o.ParametersCount = v);

    public bool GenerateTemplatedColumns
    {
        get => _generateTemplatedColumns;
        set
        {
            SetAndRaise(GenerateTemplatedColumnsProperty, ref _generateTemplatedColumns, value);
            Update();
        }
    }

    public int ParametersCount
    {
        get => _parametersCount;
        set
        {
            SetAndRaise(ParametersCountProperty, ref _parametersCount, value);
            Update();
        }
    }

    private void Update()
    {
        if (AssociatedObject is null)
        {
            return;
        }
        
        AssociatedObject.Columns.Clear();

        bool templated = GenerateTemplatedColumns;

        for (int i = 0, n = ParametersCount; i < n; ++i)
        {
            var source = $"Value{i:00}";

            AssociatedObject.Columns.Add(
                templated
                    ? new DataGridObjectColumn
                    {
                        Header = $"Templated {source}",
                        Binding = new Binding(source) {Mode = BindingMode.OneWay},
                    }
                    : new DataGridTextColumn
                    {
                        Header = $"Plain {source}",
                        Binding = new Binding(source + ".Value") {Mode = BindingMode.OneWay, StringFormat = "0.00##"}
                    }
            );
        }
    }
}