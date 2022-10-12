using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace Backend.Models
{
    public partial class Connection : ObservableObject
    {
        [ObservableProperty]
        private string _tableName = @"table-name";

        // Variabler der bestemmer hvad der står øverst i MainWindow, og hvilken farve det står i.
        [ObservableProperty]
        private string _connectedStatus = "Status: Disconnected";

        [ObservableProperty]
        private string _connectionStatusStringColor = "#ff0000";

        // Alt der tilhører connection-stringen
        [ObservableProperty]
        private string _serverAddress = "server-address";

        [ObservableProperty]
        private string _database = "database";

        [ObservableProperty]
        private string _port = "port";

        [ObservableProperty]
        private string _userName = "user-name";

        [ObservableProperty]
        private string _password = "password";

        [ObservableProperty]
        private MySqlConnection? _sqlConnection;

        // Kombiner al relevant information til en ConnectionString ved brug af en lambda-expression.
        public string ConnectionString
            => $"server={ServerAddress}; database={Database}; port={Port}; username={UserName}; password={Password}";

        // Boolean lambda-expression der tjekker om der findes en SqlConnection, og hvis der gør; om den er tilsluttet.
        public bool IsConnected => SqlConnection?.State == System.Data.ConnectionState.Open;

        // Tilslut til en MySql-server og kommuniker det til brugeren.
        public void ConnectToDatabase()
        {
            try
            {
                SqlConnection = new MySqlConnection(ConnectionString);

                SqlConnection.Open();
                ConnectedStatus = "Status: Connected";
                ConnectionStatusStringColor = "#008f02"; // Grøn farve
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        // UI-tilgængelig kommando: Fjern forbindelsen til en MySql-server og kommuniker det til brugeren.
        [RelayCommand]
        public void DisconnectFromDatabase()
        {
            if (IsConnected)
            {
                try
                {
                    if (SqlConnection != null)
                    {
                        SqlConnection.Dispose();
                        ConnectedStatus = "Status: Disconnected";
                        ConnectionStatusStringColor = "#ff0000"; // Rød farve
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }
    }
}
