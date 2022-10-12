using System.Windows;

namespace DataBase_MVVM_Test
{
    /// <summary>
    /// Interaction logic for CRUDWindow.xaml
    /// </summary>
    public partial class CRUDWindow : Window
    {
        public CRUDWindow()
        {
            InitializeComponent();
        }

        private void OnClick_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
