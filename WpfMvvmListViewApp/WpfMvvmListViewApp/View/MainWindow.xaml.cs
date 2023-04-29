using System.Windows;

using WpfMvvmListViewApp.ViewModel;

namespace WpfMvvmListViewApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //DataContext = App.Current.MainVM; 
            DataContext = App.Current?.Services?.GetService(typeof(MainViewModel));
        }
    }
}
