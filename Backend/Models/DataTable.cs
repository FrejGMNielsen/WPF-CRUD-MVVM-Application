using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics;

namespace Backend.Models
{
    // En model der repræsenterer den brugervalgte databasetabel der redigeres fra en MySql databaseserver.
    public partial class DatabaseTable : ObservableObject
    {
        [ObservableProperty]
        private DataTable _crudTable = new();

        // Fyld datatabellen med data fra en MySql databaseserver.
        public void FillTable(Connection currentConnection)
        {
            string query = $"SELECT * FROM {currentConnection.TableName}";
            MySqlCommand cmd = new(query, currentConnection.SqlConnection);

            try
            {
                CrudTable = new DataTable();
                if (currentConnection.SqlConnection != null && !currentConnection.IsConnected)
                {
                    currentConnection.SqlConnection.Open();
                }
                CrudTable.Load(cmd.ExecuteReader());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        // UI-tilgængelig kommando: Opdater kilde-tabellen (fra MySql databaseserveren).
        [RelayCommand]
        public void UpdateSourceTable(Connection currentConnection)
        {
            // Brug en MySqlDataAdapter til at opdatere alle ændringer der er foretaget i den lokale CRUD-tabellen.
            string query = $"SELECT * FROM {currentConnection.TableName}";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, currentConnection.SqlConnection);

            // MySqlCommandBuilder'en er nødvendig for at give adapteren Insert, Update og Delete-kommandoerne.
            MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
            builder.GetInsertCommand();
            builder.GetUpdateCommand();
            builder.GetDeleteCommand();

            adapter.Update(CrudTable);
        }
    }
}
