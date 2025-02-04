using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace colony_game
{
    class Cuisiniere : Coordinatrice
    {
        //Constructeur de la classe Cuisiniere  
        public Cuisiniere(string _nom, Stock _stock) : base(_nom,_stock)
        {
            _blessure = 10;
            _symbole = "σ";
        }

        //Méthode "seBlesser" adaptée à la classe Cuisiniere
        public override void seBlesser(int n)
        {
            if (n == 2)
            {
                Pdv -= _blessure;
                Moral -= _blessure / 2;            
                Console.WriteLine("Purée ! " + this.getNom() + " s'est blessée en cuisinant, pense à checker ses pdv ! (et les assiettes des villageoises, y'a peut-être un doigt dedans...)");
                Console.ReadLine();

            }
        }

    }
}
