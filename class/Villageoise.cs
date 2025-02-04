using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace colony_game
{
    abstract class Villageoise
    {
        //Attributs de la classe Villageoise
        protected string _nom;
        protected int _pdv; //Sur une échelle de 100 points
        protected int _moral; //Sur une échelle de 100 points
        protected int _blessure;
        protected Stock _stock;
        protected string _symbole;

        //Getter de l'attribut symbole
        public string getSymbole()
        {
            return _symbole;
        }

        //Constructeur de la classe Villageoise 
        public Villageoise(string nom, Stock stock)
        {
            _nom = nom;
            _pdv =100;
            _moral = 100;
            _stock = stock;
        }

        //Propriété Pdv, permet l'accès et la modification de la variable 
        public int Pdv
        {
            get { return _pdv; }
            set { _pdv = value; }
        }

        //Getter de l'attribut nom
        public string getNom() { return _nom; }

        //Méthode qui retourne le type de la villageoise dans une chaîne de caractère grâce à la méthode GetType() de C#
        public string getTypeVillageoise()
        {
            string type = this.GetType().ToString();
            type = type.Substring(8);
            return type;
        }

        //Propriété Moral, permet l'accès et la modification de la variable 
        public int Moral
        {
            get { return _moral; }
            set { _moral = value; }
        }

        //Méthode abstraite seBlesser() redéfinie dans les classes les plus spécifiques, car chaque villageoise se blesse différement 
        //L'impact de seBlesser dépend du métier exercé 
        //L'argument n est un entier aléatoire qui donnera la probabilité de se blesser (1/n)
        public abstract void seBlesser(int n); 

        //Getter de l'attribut blessure, retourne le nombre de pdv que perd une villageoise en se blessant 
        public int getBlessure() { return _blessure; }

        //Méthode qui informe le joueur de la mort d'une villageoise et la retire du village
        public void mourir(Village v,Grille grilleJeu)
        {
            
            Console.WriteLine(this.getNom() + " : (" + this.getTypeVillageoise() + ") vient de mourir. Tu nous manqueras, RIP :( Appuyez sur entree pour continuer...");
            Console.ReadLine();

            v.removeVillageoise(this);
            grilleJeu.dessineVillage(v);
        }
        

    }
}
