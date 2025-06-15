using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Boost_CSharp
{
    class ValidationNumerique
    {
        public static bool ValiderNumerique(string cin, int longMax)
        {
            return cin.Length == longMax;
        }

        public static void BloquerSaisieEtAfficherMessage(TextBox textBox, KeyPressEventArgs e, int longMax)
        {
            if (textBox.Text.Length >= longMax && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Ce champ doit contenir exactement " + longMax + " chiffres.", "Limite atteinte", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Seuls les chiffres sont autorisés.", "Caractère invalide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        internal static void BloquerSaisieEtAfficherMessage(ComboBox textBox, KeyPressEventArgs e, int longMax)
        {
            if (textBox.Text.Length >= longMax && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Ce champ doit contenir exactement " + longMax + " chiffres.", "Limite atteinte", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Seuls les chiffres sont autorisés.", "Caractère invalide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
