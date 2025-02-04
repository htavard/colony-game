using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace colony_game
{
    class Botaniste : Sorciere
    {
        //Constructeur de la classe Botaniste 
        public Botaniste(string _nom, Stock _stock) : base(_nom, _stock)
        {
            _blessure = 20;
            _symbole = "ζ";
        }

        //Méthode "seBlesser" adaptée à la classe Botaniste 
        public override void seBlesser(int n)
        {
            if (n == 3)
            {
                Pdv -= _blessure;
                Moral -= _blessure / 2;
            }
            Console.WriteLine("Quelle affreuse nouvelle ! " + this.getNom() + " s'est blessée en concoctant, pense à checker ses pdv !");
            Console.ReadLine();
        }

    }
}
