using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Boost_CSharp
{
    public partial class Form1 : Form
    {
        private GestionPersonnes gP;
        public Form1()
        {
            InitializeComponent();
        }

        private void nomP_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationChaine.BloquerSaisie(nomP, e, 100);
        }

        private void prenomsP_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationChaine.BloquerSaisie(prenomsP, e, 100);
        }

        private void cinP_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationNumerique.BloquerSaisieEtAfficherMessage(cinP, e, 12);
        }

        private void phoneP_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidationNumerique.BloquerSaisieEtAfficherMessage(phoneP, e, 10);
        }

        private void emailP_Leave(object sender, EventArgs e)
        {
            ValidationEmail.ValiderEtAfficherMessage(emailP);
        }

        private void phoneP_Leave(object sender, EventArgs e)
        {
            if (phoneP.Text != "")
            {
                if (!ValidationNumerique.ValiderNumerique(phoneP.Text, 10))
                {
                    MessageBox.Show("Ce champ doit contenir exactement 10 chiffres.", "Limite pas atteinte", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    phoneP.Focus();
                }
            }
        }

        private void donneP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow ma_var = donneP.Rows[e.RowIndex];
                idP.Text = ma_var.Cells[0].Value.ToString();
                nomP.Text = ma_var.Cells[1].Value.ToString();
                prenomsP.Text = ma_var.Cells[2].Value.ToString();
                cinP.Text = ma_var.Cells[3].Value.ToString();
                adrsP.Text = ma_var.Cells[4].Value.ToString();
                phoneP.Text = ma_var.Cells[5].Value.ToString();
                emailP.Text = ma_var.Cells[6].Value.ToString();
            }
        }

        private void btnAjoutP_Click(object sender, EventArgs e)
        {
            try
            {
                string nom = nomP.Text;
                string prenoms = prenomsP.Text;
                string cin = cinP.Text;
                string adresse = adrsP.Text;
                string phone = phoneP.Text;
                string email = emailP.Text;

                if (string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(prenoms) || string.IsNullOrEmpty(cin) || string.IsNullOrEmpty(adresse)
                    || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Merci bien de tous remplir.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    try
                    {
                        Personnes personne = new Personnes(nom, prenoms, cin, adresse, phone, email);
                        gP = new GestionPersonnes();
                        gP.AjouterPersonne(personne);
                        actualiserP();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur :" + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur :" + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnModifierP_Click(object sender, EventArgs e)
        {
            if (idP.Text == "")
            {
                MessageBox.Show("Veuillez Selectionner une personne.", "Cible non trouver", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    int num = int.Parse(idP.Text);
                    string nom = nomP.Text;
                    string prenoms = prenomsP.Text;
                    string cin = cinP.Text;
                    string adresse = adrsP.Text;
                    string phone = phoneP.Text;
                    string email = emailP.Text;

                    if (string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(prenoms) || string.IsNullOrEmpty(cin) || string.IsNullOrEmpty(adresse)
                        || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(email))
                    {
                        MessageBox.Show("Merci bien de tous remplir.", "Champ vide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        try
                        {
                            Personnes personne = new Personnes(num, nom, prenoms, cin, adresse, phone, email);
                            gP = new GestionPersonnes();
                            gP.ModifierPersonne(personne);
                            actualiserP();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Erreur :" + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur :" + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnSupprimerP_Click(object sender, EventArgs e)
        {
            List<int> ids = RecupererIdsSelectionnes(donneP);

            if (ids.Count > 0)
            {
                DialogResult reponse = MessageBox.Show("Voulez vous vraiment supprimer cette/ces " + ids.Count + " personne(s) ?", "Supprimer la(les) personne(s)", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (reponse == DialogResult.Yes)
                {
                    gP.SupprimerPersonnes(ids); // Utilise la méthode de suppression adaptée

                    MessageBox.Show(ids.Count + " personne(s) supprimée(s) avec succès !");
                    actualiserP();// Actualise l'affichage des données
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner au moins une ligne !");
            }
        }

        private void btnActualiserP_Click(object sender, EventArgs e)
        {
            actualiserP();
        }

        private void actualiserP()
        {
            idP.Text = "";
            nomP.Clear();
            prenomsP.Clear();
            cinP.Clear();
            adrsP.Clear();
            phoneP.Clear();
            emailP.Clear();
            listerNumP();
            afficherDonnerPersonnes();
        }

        private void idP_SelectedIndexChanged(object sender, EventArgs e)
        {
            gP = new GestionPersonnes();
            gP.completerLesChamps(idP, nomP, prenomsP, cinP, adrsP, phoneP, emailP);
        }

        private void listerNumP()
        {
            try
            {
                gP = new GestionPersonnes();
                gP.afficherNumPersonnes(idP);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur :" + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void afficherDonnerPersonnes()
        {
            try
            {
                gP = new GestionPersonnes();
                gP.AfficherLesPersonnes(donneP);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur :" + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private List<int> RecupererIdsSelectionnes(DataGridView datagridview)
        {
            List<int> idsSelectionnes = new List<int>();

            foreach (DataGridViewRow row in datagridview.SelectedRows)
            {
                if (row.Cells["ID"].Value != null) // Assurez-vous que la colonne des IDs est nommée "id"
                {
                    idsSelectionnes.Add(Convert.ToInt32(row.Cells["ID"].Value));
                }
            }

            return idsSelectionnes;
        }

        private void cinP_Leave(object sender, EventArgs e)
        {
            if (cinP.Text != "")
            {
                if (!ValidationNumerique.ValiderNumerique(cinP.Text, 12))
                {
                    MessageBox.Show("Ce champ doit contenir exactement 12 chiffres.", "Limite pas atteinte", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cinP.Focus();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            afficherDonnerPersonnes();
            listerNumP();
        } 
    }
}
