using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace colony_game
{
       class Batiment
       {
            //Attributs de la classe Batiment 
            public string _type;    
            public string _nom;
            protected int _niveau; //Échelle de 1 à 3
            public Stock _stockvillage;
            public int _quantite;
            public int _nbProduction;
            public string _symbole;
    
            //Constructeur de la classe Batiment 
            public Batiment(string type, string nom, Stock stockvillage, string symbole)
            {
                _type = type;    
                _nom = nom;
                _niveau = 1;    
                _stockvillage = stockvillage;
                _quantite = 1;
                _nbProduction = 1;
                _symbole = symbole;
            }

        //Getter de l'attribut symbole
        public string getSymbole() { return _symbole; }

        //Propriété NbProduction qui permet l'accès et la modification de la variable 
        public int NbProduction { get { return _nbProduction; } set { _nbProduction = value; } }

        //Propriété Niveau qui permet l'accés et la modification de la variable 
        public int Niveau { get { return _niveau; } set { _niveau = value; } }

        //Propriété Quantite qui permet l'accés et la modification de la variable 
        public int Quantite { get { return _quantite; } set { _quantite = value; } }

        //Getter de l'attribut "Nom"
        public string getNom()
        {
            return _nom;
        }

        //Getter de l'attribut "Type"
        public string getTypeBat() { return _type; }

        //Méthode qui permet d'augmenter le niveau d'un bâtiment si cela est possible ou non
        public void Ameliorer(Stock stockvillage, List<Ressource> recetteBatPrecis)
        {
            bool faisable = stockvillage.isDispoStock(recetteBatPrecis);
            if (faisable)
            {
                if (Niveau < 3)
                {
                    Niveau += 1;
                    NbProduction += 1;
                    Console.WriteLine("Votre batiment a atteint un niveau supérieur, son rendement augmente !");
                    
                }
                else
                {
                    Niveau = 3;
                    NbProduction = 3;
                    Console.WriteLine("Votre bâtiment est au max de son swag, vous ne pouvez plus l'améliorer.");
                    
                }
                for(int i =0; i<recetteBatPrecis.Count(); i++)
                {
                    for (int j = 0; j < stockvillage.getStockLength(); j++)
                        if(stockvillage.getRessource(j).getNom() == recetteBatPrecis[i].getNom())
                            stockvillage.getRessource(j).Quantite -= recetteBatPrecis[i].Quantite;
                }
            }
            else
                Console.WriteLine("Vous n'avez pas les ressources nécessaires à l'amélioration, hop au boulot !");
            Console.ReadLine();
         }
                    
       }

        

}


