using Backend.ViewModels;
using System.Windows;

namespace DataBase_MVVM_Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel = new();
        public MainWindow()
        {
            InitializeComponent();

            DataContext = _viewModel;
        }

        // Klargør et nyt CRUD-vindue og vis det.
        private void OnClick_Connect(object sender, RoutedEventArgs e)
        {
            _viewModel.CurrentConnection.ConnectToDatabase();

            _viewModel.CrudTable.FillTable(_viewModel.CurrentConnection);

            CRUDWindow CRUDWindow = new()
            {
                DataContext = this.DataContext
            };
            CRUDWindow.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _viewModel.CurrentConnection.DisconnectFromDatabase();
        }
    }
}
