using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Boost_CSharp
{
    class ConnectionBase
    {
        private readonly string URL;

        public ConnectionBase()
        {
            // Chaîne de connexion à la base de données
            URL = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\exercices\csharp\Boost_CSharp\Boost_CSharp\MaBase.mdf;Integrated Security=True";
        }

        // Méthode pour ouvrir une connexion
        public SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(URL);
            try
            {
                connection.Open();
                Console.WriteLine("Connexion ouverte avec succès.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de l'ouverture de la connexion : " + ex.Message);
            }
            return connection;
        }

        // Méthode pour fermer une connexion
        public void CloseConnection(SqlConnection connection)
        {
            try
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    Console.WriteLine("Connexion fermée avec succès.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la fermeture de la connexion : " + ex.Message);
            }
        }

        // Méthode pour exécuter une requête SELECT
        public DataTable ExecuteSelectQuery(string query)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = OpenConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de l'exécution de la requête SELECT : " + ex.Message);
                }
                finally
                {
                    CloseConnection(connection);
                }
            }

            return dataTable;
        }

        // Méthode pour exécuter une requête INSERT, UPDATE, DELETE
        public void ExecuteNonQuery(string query)
        {
            using (SqlConnection connection = OpenConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    Console.WriteLine("Requête exécutée avec succès.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de l'exécution de la requête : " + ex.Message);
                }
                finally
                {
                    CloseConnection(connection);
                }
            }
        }
    }
}
