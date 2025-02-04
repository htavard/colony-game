using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace colony_game
{
    class Village

    {
        //Attribut de la classe Village 
        protected List<Villageoise> _village;

        //Propriété de Village, permet l'accès et la modification
        public List<Villageoise> _Village { get { return _village; } set { _village = value; } }

        //Constructeur de la classe Village
        public Village()
        {
            _village = new List<Villageoise> { };
        }

        //Méthode qui retourne la taille du village, substitut de Count()
        public int getTailleVillage()
        {
            return _village.Count();
        }

        //Méthode qui retourne la villageoise se trouvant à l'indice passé en argument dans le village 
        public Villageoise getVillageoise(int indice)
        {
            if (indice == _village.Count())
                return _village[indice - 1];
            else
                return _village[indice];
        }

        //Méthode qui ajoute une villageoise au village, substitut de Add()
        public void addVillageoise(Villageoise v)
        {
            _village.Add(v);
        }

        //Méthode qui ajoute une villageoise au village, substitut de Remove()
        public void removeVillageoise(Villageoise v)
        {
            _village.Remove(v);
        }

        //Méthode qui retourne le nombre de villageoise que compte le village pour un type précis donné en argument 
        public int getNbVillageoiseType(string type)
        {
            int nbType = 0;
            for (int i = 0; i < _village.Count(); i++)
            {
                if (_village[i].getTypeVillageoise() == type)
                {
                    nbType += 1;
                }
            }
            return nbType;
        }

        //Méthode qui vérifie les conditions de défaites et affiche la défaite du joueur en conséquence
        

        //Méthode qui ajoute au village un semble de villageoise que nous avons jugé nécessaire d'avoir au démarrage d'une partie
        public void initialiserVillage(Stock _stockVillage)
        {
            Bucheronne bu = new Bucheronne("Buche", _stockVillage);
            Mineuse mi = new Mineuse("Cailloux", _stockVillage);
            Mineuse min = new Mineuse("Rocher", _stockVillage);
            Jardiniere ja = new Jardiniere("Petale", _stockVillage);
            Jardiniere jado = new Jardiniere("Marguerite", _stockVillage);
            Occultiste oc = new Occultiste("Rageuse", _stockVillage);
            Botaniste bo = new Botaniste("Paillette", _stockVillage);
            Botaniste bota = new Botaniste("Clochette", _stockVillage);
            Guerisseuse gu = new Guerisseuse("Doliprane", _stockVillage);
            Cuisiniere cu = new Cuisiniere("Deguste", _stockVillage);
            Combattante ca = new Combattante("Baston", _stockVillage);
            this.addVillageoise(bu);
            this.addVillageoise(mi);
            this.addVillageoise(min);
            this.addVillageoise(ja);
            this.addVillageoise(jado);
            this.addVillageoise(oc);
            this.addVillageoise(bo);
            this.addVillageoise(bota);
            this.addVillageoise(gu);
            this.addVillageoise(cu);
            this.addVillageoise(ca);
        }

        

        //Méthode qui transforme une occultiste en villageoise aléatoire parmi le type ouvrière 
        public void purifier(Occultiste o, Stock s)
        {
            string name = o.getNom();
            this.removeVillageoise(o);
            Random alea = new Random();
            int proba = alea.Next(3);
            if (proba == 0)
            {
                Bucheronne b = new Bucheronne(name, s);
                this.addVillageoise(b);
                Console.WriteLine(name + " n'est plus une occultiste désormais, mais une bûcheronne ! Espérons qu'elle profite de sa nouvelle vie dénuée de haine.");
            }
            else if (proba == 1)
            {
                Mineuse m = new Mineuse(name, s);
                this.addVillageoise(m);
                Console.WriteLine(name + " n'est plus une occultiste désormais, mais une mineuse ! Espérons qu'elle profite de sa nouvelle vie dénuée de haine.");
            }
            else if (proba == 2)
            {
                Jardiniere j = new Jardiniere(name, s);
                this.addVillageoise(j);
                Console.WriteLine(name + " n'est plus une occultiste désormais, mais une jardinière ! Espérons qu'elle profite de sa nouvelle vie dénuée de haine.");
            }
            
        }

        //Méthode ToString() spécifique au village. Quand on affiche le village v, on obtient toute sa composition
        public override string ToString()
        {
            string chRes = "";
            //Donne la population totale du village 
            chRes += "Votre village est peuplé de " + getTailleVillage() + " villageoises \n";
            List<Villageoise> types = new List<Villageoise> { };
            types.Add(_village[0]);
            //Vérifier si le type de la villageoise ajoutée est déjà dans la liste répertoriant les types 
            for (int i = 0; i < this.getTailleVillage(); i++)
            {
                if (getNbVillageoiseType(_village[i].getTypeVillageoise()) != 0)
                {
                    bool isHere = false;
                    for (int j = 0; j < types.Count(); j++)
                    {
                        if (_village[i].GetType() == types[j].GetType())
                            isHere = true;
                    }
                    if (isHere == false)
                        types.Add(_village[i]);
                }
            }
            for (int k = 0; k < types.Count(); k++)
            {
                string type = types[k].getTypeVillageoise();
                //Donne le nombre de villageoise par type 
                chRes += "\nIl y a " + getNbVillageoiseType(types[k].getTypeVillageoise()) + " villageoise(s) de type '" + type + "' ("+types[k].getSymbole()+")\n";
                //Affichage spécifique pour chaque villageoise 
                for(int i=0; i < this.getTailleVillage(); i++)
                {
                    if(_village[i].getTypeVillageoise() == type)
                    {
                        chRes += this.getVillageoise(i).getNom() + " : " + this.getVillageoise(i).Pdv + " points de vie et " + this.getVillageoise(i).Moral + " points de moral. \n";
                    }
                }
            }

            return chRes;
        }

    }
}
