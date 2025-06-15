using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Boost_CSharp
{
    class GestionPersonnes
    {
        private readonly ConnectionBase con;

        public GestionPersonnes() { con = new ConnectionBase(); }

        // ajout
        internal void AjouterPersonne(Personnes monPersonnes)
        {
            string req = "INSERT INTO Personnes (nom, prenoms, cin, adresse, phone, email) " +
                       "VALUES (@Nom, @Prenoms, @Cin, @Adresse, @Phone, @Email)";
            
            using (SqlConnection connection = con.OpenConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(req, connection);
                    cmd.Parameters.AddWithValue("@Nom", monPersonnes.Nom);
                    cmd.Parameters.AddWithValue("@Prenoms", monPersonnes.Prenoms);
                    cmd.Parameters.AddWithValue("@Cin", monPersonnes.Cin);
                    cmd.Parameters.AddWithValue("@Adresse", monPersonnes.Adresse);
                    cmd.Parameters.AddWithValue("@Phone", monPersonnes.Phone);
                    cmd.Parameters.AddWithValue("@Email", monPersonnes.Email);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Personnes ajouté avec succès.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de l'ajout du Personne : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally
                {
                    con.CloseConnection(connection);
                }
            }
        }

        //lire
        internal void AfficherLesPersonnes(DataGridView data)
        {
            string req = "SELECT * FROM Personnes";
            using (SqlConnection connection = con.OpenConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(req, connection);
                    SqlDataReader dr = cmd.ExecuteReader();

                    data.Columns.Clear();
                    data.Rows.Clear();
                    data.Columns.Add("ID", "ID");
                    data.Columns.Add("NOM", "NOM");
                    data.Columns.Add("PRENOM(S)", "PRENOM(S)");
                    data.Columns.Add("CIN", "CIN");
                    data.Columns.Add("ADRESSE", "ADRESSE");
                    data.Columns.Add("PHONE", "PHONE");
                    data.Columns.Add("EMAIL", "EMAIL");

                    int i = 0;

                    while (dr.Read())
                    {
                        data.Rows.Add();
                        data.Rows[i].Cells[0].Value = dr["num"];
                        data.Rows[i].Cells[1].Value = dr["nom"];
                        data.Rows[i].Cells[2].Value = dr["prenoms"];
                        data.Rows[i].Cells[3].Value = dr["cin"];
                        data.Rows[i].Cells[4].Value = dr["adresse"];
                        data.Rows[i].Cells[5].Value = dr["phone"];
                        data.Rows[i].Cells[6].Value = dr["email"];
                        i++;
                    }
                    dr.Dispose();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Erreur de lecture des Personnes:" + ex.Message);
                }
                finally
                {
                    con.CloseConnection(connection);
                }
            }
        }

        //modifier
        internal void ModifierPersonne(Personnes monPersonnes)
        {
            string req = "UPDATE Personnes SET nom = @Nom, prenoms = @Prenoms, cin = @Cin, adresse = @Adresse, phone = @Phone, email = @Email " +
                       "WHERE num = @Id";
            using (SqlConnection connection = con.OpenConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(req, connection);
                    cmd.Parameters.AddWithValue("@Id", monPersonnes.Num);
                    cmd.Parameters.AddWithValue("@Nom", monPersonnes.Nom);
                    cmd.Parameters.AddWithValue("@Prenoms", monPersonnes.Prenoms);
                    cmd.Parameters.AddWithValue("@Cin", monPersonnes.Cin);
                    cmd.Parameters.AddWithValue("@Adresse", monPersonnes.Adresse);
                    cmd.Parameters.AddWithValue("@Phone", monPersonnes.Phone);
                    cmd.Parameters.AddWithValue("@Email", monPersonnes.Email);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Personne modifié avec succès.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de la modification du Personne : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally
                {
                    con.CloseConnection(connection);
                }
            }
        }

        // supprimer
        internal void SupprimerPersonnes(List<int> ids)
        {
            using (SqlConnection conn = con.OpenConnection())
            {
                foreach (int id in ids)
                {
                    string query = "DELETE FROM Personnes WHERE num = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                con.CloseConnection(conn);
            }
        }

        // lister les id
        public void afficherNumPersonnes(ComboBox combo)
        {
            string req = "SELECT * FROM Personnes";
            using (SqlConnection connection = con.OpenConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand(req, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    combo.Items.Clear();

                    while (reader.Read())
                    {
                        combo.Items.Add(reader[0].ToString());
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de la lecture des Personnes : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally
                {
                    con.CloseConnection(connection);
                }
            }
        }

        // completer les champs
        public void completerLesChamps(ComboBox combo, TextBox nom, TextBox prenom, TextBox cin, TextBox adresse, TextBox phone, TextBox email)
        {
            string req = "SELECT nom, prenoms, cin, adresse, phone, email FROM Personnes where num = '" + combo.Text + "'";

            using (SqlConnection connection = con.OpenConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand(req, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    reader.Read();

                    nom.Text = reader[0].ToString();
                    prenom.Text = reader[1].ToString();
                    cin.Text = reader[2].ToString();
                    adresse.Text = reader[3].ToString();
                    phone.Text = reader[4].ToString();
                    email.Text = reader[5].ToString();

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de la lecture des personnes : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally
                {
                    con.CloseConnection(connection);
                }
            }
        }
    }
}
