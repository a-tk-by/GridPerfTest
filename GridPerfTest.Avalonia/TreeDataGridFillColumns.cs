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
        return source switch
        {
            "Value00" => item => item.Value00.Value,
            "Value01" => item => item.Value01.Value,
            "Value02" => item => item.Value02.Value,
            "Value03" => item => item.Value03.Value,
            "Value04" => item => item.Value04.Value,
            "Value05" => item => item.Value05.Value,
            "Value06" => item => item.Value06.Value,
            "Value07" => item => item.Value07.Value,
            "Value08" => item => item.Value08.Value,
            "Value09" => item => item.Value09.Value,
            "Value10" => item => item.Value10.Value,
            "Value11" => item => item.Value11.Value,
            "Value12" => item => item.Value12.Value,
            "Value13" => item => item.Value13.Value,
            "Value14" => item => item.Value14.Value,
            "Value15" => item => item.Value15.Value,
            "Value16" => item => item.Value16.Value,
            "Value17" => item => item.Value17.Value,
            "Value18" => item => item.Value18.Value,
            "Value19" => item => item.Value19.Value,
            "Value20" => item => item.Value20.Value,
            "Value21" => item => item.Value21.Value,
            "Value22" => item => item.Value22.Value,
            "Value23" => item => item.Value23.Value,
            "Value24" => item => item.Value24.Value,
            "Value25" => item => item.Value25.Value,
            "Value26" => item => item.Value26.Value,
            "Value27" => item => item.Value27.Value,
            "Value28" => item => item.Value28.Value,
            "Value29" => item => item.Value29.Value,
            "Value30" => item => item.Value30.Value,
            "Value31" => item => item.Value31.Value,
            "Value32" => item => item.Value32.Value,
            "Value33" => item => item.Value33.Value,
            "Value34" => item => item.Value34.Value,
            "Value35" => item => item.Value35.Value,
            "Value36" => item => item.Value36.Value,
            "Value37" => item => item.Value37.Value,
            "Value38" => item => item.Value38.Value,
            "Value39" => item => item.Value39.Value,
            "Value40" => item => item.Value40.Value,
            "Value41" => item => item.Value41.Value,
            "Value42" => item => item.Value42.Value,
            "Value43" => item => item.Value43.Value,
            "Value44" => item => item.Value44.Value,
            "Value45" => item => item.Value45.Value,
            "Value46" => item => item.Value46.Value,
            "Value47" => item => item.Value47.Value,
            "Value48" => item => item.Value48.Value,
            "Value49" => item => item.Value49.Value,
            "Value50" => item => item.Value50.Value,
            "Value51" => item => item.Value51.Value,
            "Value52" => item => item.Value52.Value,
            "Value53" => item => item.Value53.Value,
            "Value54" => item => item.Value54.Value,
            "Value55" => item => item.Value55.Value,
            "Value56" => item => item.Value56.Value,
            "Value57" => item => item.Value57.Value,
            "Value58" => item => item.Value58.Value,
            "Value59" => item => item.Value59.Value,
            "Value60" => item => item.Value60.Value,
            "Value61" => item => item.Value61.Value,
            "Value62" => item => item.Value62.Value,
            "Value63" => item => item.Value63.Value,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    private Expression<Func<SourceItem, object?>> GetRawValueExpression(string source)
    {
        return source switch
        {
            "Value00" => item => item.Value00,
            "Value01" => item => item.Value01,
            "Value02" => item => item.Value02,
            "Value03" => item => item.Value03,
            "Value04" => item => item.Value04,
            "Value05" => item => item.Value05,
            "Value06" => item => item.Value06,
            "Value07" => item => item.Value07,
            "Value08" => item => item.Value08,
            "Value09" => item => item.Value09,
            "Value10" => item => item.Value10,
            "Value11" => item => item.Value11,
            "Value12" => item => item.Value12,
            "Value13" => item => item.Value13,
            "Value14" => item => item.Value14,
            "Value15" => item => item.Value15,
            "Value16" => item => item.Value16,
            "Value17" => item => item.Value17,
            "Value18" => item => item.Value18,
            "Value19" => item => item.Value19,
            "Value20" => item => item.Value20,
            "Value21" => item => item.Value21,
            "Value22" => item => item.Value22,
            "Value23" => item => item.Value23,
            "Value24" => item => item.Value24,
            "Value25" => item => item.Value25,
            "Value26" => item => item.Value26,
            "Value27" => item => item.Value27,
            "Value28" => item => item.Value28,
            "Value29" => item => item.Value29,
            "Value30" => item => item.Value30,
            "Value31" => item => item.Value31,
            "Value32" => item => item.Value32,
            "Value33" => item => item.Value33,
            "Value34" => item => item.Value34,
            "Value35" => item => item.Value35,
            "Value36" => item => item.Value36,
            "Value37" => item => item.Value37,
            "Value38" => item => item.Value38,
            "Value39" => item => item.Value39,
            "Value40" => item => item.Value40,
            "Value41" => item => item.Value41,
            "Value42" => item => item.Value42,
            "Value43" => item => item.Value43,
            "Value44" => item => item.Value44,
            "Value45" => item => item.Value45,
            "Value46" => item => item.Value46,
            "Value47" => item => item.Value47,
            "Value48" => item => item.Value48,
            "Value49" => item => item.Value49,
            "Value50" => item => item.Value50,
            "Value51" => item => item.Value51,
            "Value52" => item => item.Value52,
            "Value53" => item => item.Value53,
            "Value54" => item => item.Value54,
            "Value55" => item => item.Value55,
            "Value56" => item => item.Value56,
            "Value57" => item => item.Value57,
            "Value58" => item => item.Value58,
            "Value59" => item => item.Value59,
            "Value60" => item => item.Value60,
            "Value61" => item => item.Value61,
            "Value62" => item => item.Value62,
            "Value63" => item => item.Value63,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    /*private Expression<Func<SourceItem, double>> GetValueExpression(string source)
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

    */

}