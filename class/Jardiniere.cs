using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace colony_game
{
     class Jardiniere : Ouvriere
    {
        //Constructeur de la classe Jardiniere
        public Jardiniere(string _nom, Stock _stock) : base(_nom, _stock) 
        {
            _blessure = 10;
            _symbole = "Φ";
        }

        //Méthode qui susbtitue l'aléatoire classique en réglant le problème de "faux aléatoire"
        Random alea = new Random();
        public int RandomNumber(int a, int b)
        {
            lock (this)
                return alea.Next(a, b);
        }

        //Méthode "recolter" adaptée à la classe Jardinière 
        public override void recolter(Grille grilleJeu)
        {

            int proba = RandomNumber(0,6);
            if (proba == 0)
            {
                Ressource ble = new Ressource("Blé", 1);
                _stock.addRessource(ble);
                Console.WriteLine("1 blé en plus dans le stock!");

            } else if (proba == 1)
            {
                Ressource poire = new Ressource("Poire", 1);
                _stock.addRessource(poire);
                Console.WriteLine("1 poire en plus dans le stock!");

            }
            else if (proba == 2)
            {
                Ressource courge = new Ressource("Courge", 1);
                _stock.addRessource(courge);
                Console.WriteLine("1 courge en plus dans le stock!");

            }
            else if (proba==3)
            {
                Ressource lavande = new Ressource("Lavande", 1);
                _stock.addRessource(lavande);
                Console.WriteLine("1 lavande en plus dans le stock!");

            }
            else if (proba==4)
            {
                Ressource planteMagique = new Ressource("Plante magique", 1);
                _stock.addRessource(planteMagique);
                Console.WriteLine("1 plante magique en plus dans le stock!");
            }
            else
            {
                Ressource orties = new Ressource("Orties", 1);
                _stock.addRessource(orties);
                Console.WriteLine("1 ortie en plus dans le stock!");


            }
        }


        //Méthode "seBlesser" adaptée à la classe Jardinière 
        public override void seBlesser(int n)
        {
            if (n == 1)
            {
                Pdv -= _blessure;
                Moral -= _blessure / 2;
                Console.WriteLine(this.getNom() + " s'est blessée ! Son moral et ses pdv sont directement affectés...");
            }
        }

    }
}
