using Backend.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Backend.ViewModels
{
    public partial class MainViewModel : ObservableRecipient
    {
        [ObservableProperty]
        private Connection _currentConnection = new();

        [ObservableProperty]
        private DatabaseTable _crudTable = new();

        // Brug Rfam's offentligt-tilgængelige MySql-database som test (ReadOnly).
        [RelayCommand]
        public void GetTestDatabase()
        {
            CurrentConnection.ServerAddress = "mysql-rfam-public.ebi.ac.uk";
            CurrentConnection.UserName = "rfamro";
            CurrentConnection.Password = "";
            CurrentConnection.Port = "4497";
            CurrentConnection.Database = "Rfam";
            CurrentConnection.TableName = "clan";
        }
    }
}
