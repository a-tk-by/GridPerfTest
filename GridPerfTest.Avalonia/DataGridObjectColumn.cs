using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;

namespace GridPerfTest.Avalonia;

public class DataGridObjectColumn : DataGridBoundColumn
{
    protected override Control GenerateElement(DataGridCell cell, object dataItem)
    {
        cell.HorizontalContentAlignment = HorizontalAlignment.Stretch;
        cell.VerticalContentAlignment = VerticalAlignment.Center;

        ContentControl element = new ContentControl()
        {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            HorizontalContentAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,
            VerticalContentAlignment = VerticalAlignment.Stretch
        };
        
        if (Binding != null)
            element.Bind(ContentControl.ContentProperty, Binding);
        
        return element;
    }
    
    protected override object PrepareCellForEdit(Control editingElement, RoutedEventArgs editingEventArgs)
    {
        throw new System.NotSupportedException();
    }

    protected override Control GenerateEditingElementDirect(DataGridCell cell, object dataItem)
    {
        throw new System.NotSupportedException();
    }
}