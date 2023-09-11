using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml.Templates;

namespace GridPerfTest.Avalonia;

public class ObjectColumn<TModel> : ColumnBase<TModel>
{
    private readonly IDataTemplate? _dataTemplate;
    private readonly TreeDataGrid _parent;
    private readonly Func<TModel, object?> _selector;

    public ObjectColumn(
        object? header,
        Expression<Func<TModel, object?>> selector,
        IDataTemplate? dataTemplate,
        TreeDataGrid parent,
        GridLength? width = null,
        ColumnOptions<TModel>? options = null) : base(header, width, options ?? new ColumnOptions<TModel>())
    {
        _dataTemplate = dataTemplate;
        _parent = parent;
        _selector = selector.Compile();
    }

    public override ICell CreateCell(IRow<TModel> row)
    {
        var data = _selector(row.Model);
        return new TemplateCell(
            data, 
            _ => _dataTemplate ?? _parent.FindDataTemplate(data) ?? throw new KeyNotFoundException(),
            null,
            new TemplateColumnOptions<TModel>()
        {
            CanUserSortColumn = false
        });
    }

    public override Comparison<TModel?>? GetComparison(ListSortDirection direction)
    {
        return null;
    }
}