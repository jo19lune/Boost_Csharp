using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boost_CSharp
{
    class Personnes
    {
        private int _num;
        private string _nom;
        private string _prenoms;
        private string _cin;
        private string _adresse;
        private string _phone;
        private string _email;

        public int Num { get{ return _num; } set { _num = value; } }
        public string Nom { get{ return _nom; } set { _nom = value; }}
        public string Prenoms { get{ return _prenoms; } set { _prenoms = value; } }
        public string Cin { get{ return _cin; } set { _cin = value; } }
        public string Adresse { get{ return _adresse; } set { _adresse = value; } }
        public string Phone { get{ return _phone; } set { _phone= value; } }
        public string Email { get{ return _email; } set { _email = value; } }

        public Personnes() { }

        public Personnes(string nom, string prenoms, string cin, string adresse, string phone, string email)
        {
            this._nom = nom;
            this._prenoms = prenoms;
            this._cin = cin;
            this._adresse = adresse;
            this._phone = phone;
            this._email = email;
        }

        public Personnes(int num, string nom, string prenoms, string cin, string adresse, string phone, string email)
        {
            this._num = num;
            this._nom = nom;
            this._prenoms = prenoms;
            this._cin = cin;
            this._adresse = adresse;
            this._phone = phone;
            this._email = email;
        }
    }
}
