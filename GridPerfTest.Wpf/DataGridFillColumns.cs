using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Xaml.Behaviors;

namespace GridPerfTest;

public sealed class DataGridFillColumns : Behavior<DataGrid>
{
    public static readonly DependencyProperty ParametersCountProperty = 
        DependencyProperty.Register(nameof(ParametersCount), typeof(int), typeof(DataGridFillColumns),
            new FrameworkPropertyMetadata(AnyPropertyChanged) 
        );

    public static readonly DependencyProperty GenerateTemplatedColumnsProperty =
        DependencyProperty.Register(nameof(GenerateTemplatedColumns), typeof(bool), typeof(DataGridFillColumns),
            new FrameworkPropertyMetadata(AnyPropertyChanged)
        );

    private static void AnyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is DataGridFillColumns fillColumns)
        {
            fillColumns.Update();
        }
    }

    public int ParametersCount
    {
        get => (int) GetValue(ParametersCountProperty);
        set => SetValue(ParametersCountProperty, value);
    }

    public bool GenerateTemplatedColumns
    {
        get => (bool) GetValue(GenerateTemplatedColumnsProperty);
        set => SetValue(GenerateTemplatedColumnsProperty, value);
    }

    protected override void OnAttached()
    {
        base.OnAttached();
        Update();
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

    protected override void OnDetaching()
    {
        AssociatedObject.Columns.Clear();
        base.OnDetaching();
    }
}