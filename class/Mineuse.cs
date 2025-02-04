using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace colony_game
{
    class Mineuse : Ouvriere
    {
        //Constructeur de la classe Mineuse 
        public Mineuse(string _nom, Stock _stock) : base(_nom, _stock)
        {
            _blessure = 30;
            _symbole = "θ";
        }

        //Méthode "recolter" adaptée à la classe Mineuse 
        public override void recolter(Grille grilleJeu)
        {
            Ressource mine = new Ressource("Minerais", 1);
            _stock.addRessource(mine);
            grilleJeu.mineuseMine();
            Console.WriteLine("1 minerai en plus dans le stock !");
        }

        //Méthode "seBlesser" adaptée à la classe Mineuse 
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
