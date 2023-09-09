using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace GridPerfTest;

public class DataGridObjectColumn : DataGridBoundColumn
{
    protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
    {
        return GenerateElement(cell, dataItem);
    }

    protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
    {
        cell.HorizontalContentAlignment = HorizontalAlignment.Stretch;
        cell.VerticalContentAlignment = VerticalAlignment.Center;
        ContentControl content = new()
        {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            HorizontalContentAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,
            VerticalContentAlignment = VerticalAlignment.Stretch
        };
        
        ApplyStyle(false, content);
        if (!ApplyBinding(content, ContentControl.ContentProperty))
        {
            content.Content = dataItem;
        }
        return content;
    }
        
    private bool ApplyBinding(DependencyObject target, DependencyProperty property)
    {
        BindingBase binding = Binding;
        if (binding != null)
        {
            BindingOperations.SetBinding(target, property, binding);
            return true;
        }

        BindingOperations.ClearBinding(target, property);
        return false;
    }

    private void ApplyStyle(bool defaultToElementStyle, FrameworkElement element)
    {
        Style? style = PickStyle(defaultToElementStyle);
        if (style != null)
        {
            element.Style = style;
        }
    }

    private Style? PickStyle(bool defaultToElementStyle)
    {
        Style style = ElementStyle;
        if (defaultToElementStyle && style == null)
        {
            style = ElementStyle;
        }

        return style;
    }
}