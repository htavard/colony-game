using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace colony_game
{
    abstract class Coordinatrice : Villageoise
    {
        //Constructeur de Coordinatrice avec appel à la classe mère Villageoise
        public Coordinatrice(string _nom, Stock _stock) : base(_nom, _stock) { }

        //Méthode abstraite "seBlesser" non déclarée ici car la classe n'est pas la plus spécifique de la hiérarchie 
        public abstract override void seBlesser(int n);

    }
}
