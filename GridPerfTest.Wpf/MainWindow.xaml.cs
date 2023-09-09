using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GridPerfTest.ViewModel;

namespace GridPerfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private PerformanceTestPage? _previouslySelectedPage;
        
        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
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
}