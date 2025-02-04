using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace colony_game
{
    class Ressource
    {
        //Attributs de la classe Ressource 
        private string _nom;
        private int _quantite;
        
        //Constructeur de la classe Ressource
        public Ressource(string nom, int quantite)
        {
            _nom = nom;
            _quantite = quantite;
        }

        //Gette de l'attribut nom
        public string getNom()
        {
            return _nom;
        }

        //Propriété quantité, permet l'accès et la modification
        public int Quantite
        {
            get { return _quantite; }
            set { _quantite = value; }

        }
        
        //ToString() adaptée à la classe Ressource, affiche son nom et sa quantité
        public override string ToString()
        {
            string chRes = "";
            chRes += "Ressource : " + _nom + " Quantité : " + _quantite;
            return chRes;
        }
    }
}
