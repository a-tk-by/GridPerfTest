using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Xaml.Interactivity;
using GridPerfTest.ViewModel;

namespace GridPerfTest.Avalonia;

public class TreeDataGridFillColumns : Behavior<TreeDataGrid>
{
    private bool _generateTemplatedColumns;
    public static readonly DirectProperty<TreeDataGridFillColumns, bool> GenerateTemplatedColumnsProperty = AvaloniaProperty.RegisterDirect<TreeDataGridFillColumns, bool>("GenerateTemplatedColumns", o => o.GenerateTemplatedColumns, (o, v) => o.GenerateTemplatedColumns = v);
    
    private int _parametersCount;
    public static readonly DirectProperty<TreeDataGridFillColumns, int> ParametersCountProperty = AvaloniaProperty.RegisterDirect<TreeDataGridFillColumns, int>("ParametersCount", o => o.ParametersCount, (o, v) => o.ParametersCount = v);

    
    private SourceItem[]? _source;
    public static readonly DirectProperty<TreeDataGridFillColumns, SourceItem[]?> SourceProperty = AvaloniaProperty.RegisterDirect<TreeDataGridFillColumns, SourceItem[]?>("Source", o => o.Source, (o, v) => o.Source = v);

    private IDataTemplate? _dataTemplate;
    public static readonly DirectProperty<TreeDataGridFillColumns, IDataTemplate?> DataTemplateProperty = AvaloniaProperty.RegisterDirect<TreeDataGridFillColumns, IDataTemplate?>("DataTemplate", o => o.DataTemplate, (o, v) => o.DataTemplate = v);

    public SourceItem[]? Source
    {
        get => _source;
        set
        {
            SetAndRaise(SourceProperty, ref _source, value); 
            Update();
        }
    }

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

    public IDataTemplate? DataTemplate
    {
        get => _dataTemplate;
        set
        {
            SetAndRaise(DataTemplateProperty, ref _dataTemplate, value);
            Update();
        }
    }

    private void Update()
    {
        if (AssociatedObject is null || Source is null)
        {
            return;
        }

        bool templated = GenerateTemplatedColumns;

        var itemsSource = new FlatTreeDataGridSource<SourceItem>(Source);


        for (int i = 0, n = ParametersCount; i < n; ++i)
        {
            var source = $"Value{i:00}";

            if (templated)
            {
                var column = new ObjectColumn<SourceItem>(
                    $"Templated {source}",
                    GetRawValueExpression(source),
                    DataTemplate,
                    AssociatedObject
                );
                itemsSource.Columns.Add(column);
            }
            else
            {
                var column = new TextColumn<SourceItem, double>(
                    $"Plain {source}",
                    GetValueExpression(source)
                );
            
                itemsSource.Columns.Add(column);    
            }
            
            
            /*templated
                    ? new ObjectColumn<SourceItem>(
                        $"Templated {source}",
                        CreateObjectAccessTo(source)
                        )
                    :*/ 
        }
    
        AssociatedObject.Source = itemsSource;
    }

    private Expression<Func<SourceItem, double>> GetValueExpression(string source)
    {
        if (!_valueCache.TryGetValue(source, out var result))
        {

            var parameter = Expression.Parameter(typeof(SourceItem), "input");
            var property = Expression.Property(parameter, source);
            var value = Expression.Property(property, "Value");

            result = Expression.Lambda<Func<SourceItem, double>>(value, parameter);

            _valueCache[source] = result;
        }

        return result;
    }
    
    private Expression<Func<SourceItem, object?>> GetRawValueExpression(string source)
    {
        if (!_rawValueCache.TryGetValue(source, out var result))
        {

            var parameter = Expression.Parameter(typeof(SourceItem), "input");
            var property = Expression.Property(parameter, source);
            
            result = Expression.Lambda<Func<SourceItem, object?>>(property, parameter);

            _rawValueCache[source] = result;
        }

        return result;
    }

    private readonly Dictionary<string, Expression<Func<SourceItem, double>>> _valueCache = new();
    private readonly Dictionary<string, Expression<Func<SourceItem, object?>>> _rawValueCache = new();


//Expression.Lambda<>() Binding = new Binding(source + ".Value") {Mode = BindingMode.OneWay, StringFormat = "0.00##"}

}