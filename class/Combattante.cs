using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace colony_game
{
    class Combattante : Coordinatrice
    {
        //Attributs de la classe Combattante 
        private int _experienceCombat;

        //Constructeur de la classe Combattante
        public Combattante(string _nom, Stock _stock) : base(_nom, _stock)
        {
            _blessure = 10;
            _experienceCombat = 1; //Échelle de 1 à 3
            _symbole = "π";
        }

        //Getter de l'attribut "Expérience de combat"
        public int getExperienceCombat() { return _experienceCombat; }

        //Méthode "seBlesser" adaptée à la classe Combattante 
        public override void seBlesser(int n)
        {

            if (n == 0)
            {
                Pdv -= _blessure;
                Moral -= _blessure / 2;
            }
            Console.WriteLine("Terrible ! " + this.getNom() + " s'est blessée lors de l'entraînement, pense à checker ses pdv !");
            
        }

        //Méthode permettant à une combattante de s'entraîner avec une autre et d'augmenter leurs expériences de combat respectives
        public void sentrainerAvec(Combattante c)
        {
            if (this._experienceCombat < 3 && c._experienceCombat < 3)
            {
                this._experienceCombat += 1;
                c._experienceCombat += 1;
            }
            else if (this._experienceCombat == 3 && c._experienceCombat < 3)
                c._experienceCombat += 1;
            else if (this._experienceCombat < 3 && c._experienceCombat == 3)
                this._experienceCombat += 1;
            else
                Console.WriteLine("Ces combattantes sont déjà au maximum de leur potentiel, utilisez les pour entrainer les autres !");
            
        }
    }
}
