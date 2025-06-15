using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Boost_CSharp
{
    class ValidationChaine
    {
        public static void BloquerSaisie(TextBox textBox, KeyPressEventArgs e, int tailleMax)
        {
            if (textBox.Text.Length >= tailleMax && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Ce champ est limité à '" + tailleMax + "' caractères.", "Limite atteinte", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
                MessageBox.Show("Seuls les lettres alphabétiques et les espaces sont autorisés.", "Caractère invalide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
