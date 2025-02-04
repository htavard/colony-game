using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace colony_game
{
    class Bucheronne : Ouvriere
    {
        //Constructeur de la classe Bucheronne 
        public Bucheronne(string _nom, Stock _stock) : base(_nom, _stock)
        {
            _blessure = 30;
            _symbole = "ξ";
        }

        //Méthode "recolter" adaptée à la classe Bucheronne
        public override void recolter(Grille grilleJeu)
        {
            if (grilleJeu.existe("♣"))
            {
                Ressource bois = new Ressource("Bois", 1);
                _stock.addRessource(bois);
                grilleJeu.enleveArbre("♣", "ξ");
                Console.WriteLine("1 bois en plus dans le stock!");
            }
            else
                Console.WriteLine("Vous ne pouvez plus couper de bois, il n'y en a plus ! il faut faire pousser une forêt !");
        }

        //Méthode "seBlesser" adaptée à la classe Bucheronne
        public override void seBlesser(int n)
        {
            int proba = n;
            if (proba == 0)
            {
                Pdv -= _blessure;
                Moral -= _blessure / 2;
                Console.WriteLine(this.getNom() + " s'est blessée ! Son moral et ses pdv sont directement affectés...");
                
            }
        }

    }
}

