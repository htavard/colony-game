using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace colony_game
{
    class Occultiste : Sorciere
    {
        //Constructeur de la classe Occultiste 
        public Occultiste(string _nom, Stock _stock) : base(_nom, _stock)
        {
            _blessure = 20;
            _symbole = "Ω";
        }

        //Méthode "seBlesser" adaptée à la classe Occultiste 
        public override void seBlesser(int n)
        {
            Pdv -= _blessure;
            Moral -= _blessure / 2;
        }

        //Méthode qui choisit aléatoirement une villageoise (autre qu'une guerisseuse) et la fait mourir
        public void lancerMalefice(Village v,Grille grilleJeu)
        {
            Random alea = new Random();
            int proba = alea.Next(30);
            List<Villageoise> listeVictimes = new List<Villageoise>();
            foreach(Villageoise victime in v._Village)
                if (victime.getTypeVillageoise() != "Guerisseuse" && victime.getTypeVillageoise() != "Occultiste")
                    listeVictimes.Add(victime); 
            int indexVillageoise = alea.Next(0,listeVictimes.Count());
            if (proba == 1)
            {
                listeVictimes[indexVillageoise].mourir(v,grilleJeu);
                Console.Clear();
                Console.WriteLine("C'est le drame !!! Une occultiste (ou sorcière possédant le seum) a lancé un maléfice sur " + listeVictimes[indexVillageoise].getNom() + " ! Elle meurt sur le coup et sans pouvoir se défendre. RIP.");
                Console.WriteLine("\n\n Appuyez sur entrée pour continuer...");
                Console.ReadLine();
            }
        }

    }
}
