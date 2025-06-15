using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Boost_CSharp
{
    class ValidationEmail
    {
        public static bool ValiderEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        public static void ValiderEtAfficherMessage(TextBox textBox)
        {
            string email = textBox.Text;
            if (email != "")
            {
                if (!ValiderEmail(email))
                {
                    MessageBox.Show("Adresse email invalide !\nExemple mail valide : example@email.com ", "Erreur de validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox.Focus();
                }
            }
        }
    }
}
