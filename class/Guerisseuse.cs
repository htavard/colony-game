using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace colony_game
{
    class Guerisseuse : Sorciere
    {
        //Constructeur de la classe Guerisseuse
        public Guerisseuse(string _nom, Stock _stock) : base(_nom, _stock) 
        {
            _symbole = "Σ";
        }

        //Méthode "seBlesser" adaptée à la classe Jardinière 
        public override void seBlesser(int n)
        {
            if (n == 0)
            {
                this.Moral =Moral - 30;
                Console.WriteLine(this.getNom() + " s'est blessée en pleine incantation ! Elle s'est guérie toute seule, mais attention à son moral !");
            }
            
        }

        //Méthode qui permet à une guerisseuse de soigner une villageoise en "absorbant" sa blessure 
        public void guerir(Villageoise v)
        {
            v.Pdv += v.getBlessure();
            
        }
    }
}
