using System;
using System.Diagnostics;
using Avalonia.Controls;
using GridPerfTest.ViewModel;

namespace GridPerfTest.Avalonia;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    private PerformanceTestPage? _previouslySelectedPage;


    private void Selector_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is TabControl {SelectedItem: PerformanceTestPage page} tabControl)
        {
            if (page == _previouslySelectedPage)
            {
                return;
            }
                
            page.Stopwatch = Stopwatch.StartNew();
            tabControl.LayoutUpdated += TabControlOnLayoutUpdated;
            _previouslySelectedPage = page;
        }
            
        void TabControlOnLayoutUpdated(object? unused1, EventArgs unused2)
        {
            if (page.Stopwatch is {IsRunning: true} sw)
            {
                sw.Stop();
                page.Measurements.Add(sw.Elapsed);
                page.Stopwatch = null;
            }
                
            tabControl.LayoutUpdated -= TabControlOnLayoutUpdated;
        }
        
    }
}