using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace colony_game
{
    class Stock
    {
        //Attribut de la classe Stock
        protected List<Ressource> _stock;

        //Propriété Stock qui permet l'accès et la modification de la variable
        public List<Ressource> _Stock { get; set; }

        //Constructeur de la classe Stock
        public Stock()
        {
            _stock = new List<Ressource> { };
        }

        //Méthode qui retourne la taille du stock, substitue de la méthode Count()
        public int getStockLength() { return _stock.Count(); }

        //Méthode qui ajoute une ressource au stock, substitue de la méthode Add()
        public void addRessource(Ressource r)
        {
            int cpt = 0;
            for(int i=0; i<_stock.Count(); i++)
            {
                if(r.getNom() == _stock[i].getNom())
                {
                    cpt += 1;
                    _stock[i].Quantite += 1;
                }
            }
            if (cpt == 0)
                _stock.Add(r);
        }

        //Méthode qui retire une ressource du stock, substitue de la méthode Remove()
        public void removeRessource(Ressource r)
        {
            int cpt = 0;
            for (int i = 0; i < _stock.Count(); i++)
            {
                if (r.getNom() == _stock[i].getNom())
                {
                    cpt += 1;
                    _stock[i].Quantite -= 1;
                }
            }
            if (cpt == 0)
                Console.WriteLine("Vous n'avez pas de cette ressource");
        }

        //Méthode qui retourne l'index d'une ressource dont on spécifie le nom en argument 
        public int indexRessource(string nomRessource)
        {
            int index = 0;
            for (int i=0;i<_stock.Count();i++)
            {
                if (_stock[i].getNom() == nomRessource)
                    index = i;
            }
            return index;
        }

        //Méthode qui retourne la quantité de ressources disponible dans le stock en fonction du type de la ressource 
        public int getQuantiteRessourceType(string type)
        {
            int quantite = 0;
            for (int i = 0; i < _stock.Count(); i++)
            {
                if (_stock[i].getNom() == type)
                {
                    quantite += _stock[i].Quantite;
                }
            }
            return quantite; 
        }                

        //Méthode qui retourne la ressource à l'indice indiqué en argument, substitue de stock[indice]
        public Ressource getRessource(int indice)
        {
            return _stock[indice];
        }

        //Méthode qui soigne une villageoise grâce à une potion avant de la retirer du stock
        public void SoinPotion(Villageoise v)
        {
            int index = indexRessource("Potion de soin");
            if (_stock[index].Quantite > 0)
            {
                _stock[index].Quantite -= 1;
                if (v.Pdv + 40 > 100)
                    v.Pdv = 100;
                else
                    v.Pdv += 40;
            }
            else
                Console.WriteLine("Vous n'avez plus de potions, vous pouvez toujours utiliser la magie des guérisseuses pour vous soigner");            
        }

        //Méthode qui initie le stock avec toutes les ressources qu'il peut contenir en initialisant leur quantité à 0
        public void initialiserStock()
        {
            Ressource bois = new Ressource("Bois", 0);
            this.addRessource(bois);
            Ressource minerais = new Ressource("Minerais", 0);
            this.addRessource(minerais);
            Ressource ble = new Ressource("Blé", 0);
            this.addRessource(ble);
            Ressource planteMagique = new Ressource("Plante magique", 0);
            this.addRessource(planteMagique);
            Ressource argent = new Ressource("Argent", 0);
            this.addRessource(argent);
            Ressource or = new Ressource("Or", 0);
            this.addRessource(or);
            Ressource verre = new Ressource("Verre", 0);
            this.addRessource(verre);
            Ressource lavande = new Ressource("Lavande", 0);
            this.addRessource(lavande);
            Ressource poire = new Ressource("Poire", 0);
            this.addRessource(poire);
            Ressource courge = new Ressource("Courge", 0);
            this.addRessource(courge);
            Ressource orties = new Ressource("Orties", 0);
            this.addRessource(orties);
            Ressource potionCreation = new Ressource("Potion création", 0);
            this.addRessource(potionCreation);
            Ressource potionSoin = new Ressource("Potion soin", 0);
            this.addRessource(potionSoin);
            Ressource potionPurification = new Ressource("Potion purification", 0);
            this.addRessource(potionPurification);
            Ressource potionEngrais = new Ressource("Potion engrais", 0);
            this.addRessource(potionEngrais);
        }

        //Méthode qui range dans une liste de liste les recettes indiquant les ressources nécessaires à la préparation des 4 types de potions 
        public List<List<Ressource>> initialiserRecettes()
        {
            List<List<Ressource>> lesRecettes = new List<List<Ressource>> { };
            Ressource planteMagique = new Ressource("Plante magique", 1);
            Ressource lavande = new Ressource("Lavande", 1);
            Ressource argent = new Ressource("Argent", 1);
            Ressource or = new Ressource("Or", 1);
            Ressource orties = new Ressource("Orties", 1);

            //Potion de soin : indice 0
            List<Ressource> potionSoin = new List<Ressource> { };
            potionSoin.Add(lavande);
            potionSoin.Add(argent);
            potionSoin.Add(planteMagique);
            lesRecettes.Add(potionSoin);

            // Potion de purification : indice 1
            List<Ressource> potionPurification = new List<Ressource> { };
            potionPurification.Add(or);
            potionPurification.Add(orties);
            potionPurification.Add(planteMagique);
            lesRecettes.Add(potionPurification);

            // Potion de création : indice 2
            List<Ressource> potionCreation = new List<Ressource> { };
            potionCreation.Add(or);
            potionCreation.Add(argent);
            potionCreation.Add(planteMagique);
            lesRecettes.Add(potionCreation);

            // Potion d'engrais : indice 3
            List<Ressource> potionEngrais = new List<Ressource> { };
            potionEngrais.Add(lavande);
            potionEngrais.Add(orties);
            potionEngrais.Add(planteMagique);
            lesRecettes.Add(potionEngrais);

            return lesRecettes;
        }

        //Méthode qui vérifie si le stock contient les ressources nécessaires à la préparation de la recette passée en argument 
        public bool isDispoStock(List<Ressource> recette)
        {
            bool isDispo = true;
            for(int i=0; i<recette.Count(); i++)
            {
                for(int j=0; j<this.getStockLength(); j++)
                {
                    if (recette[i].getNom() == this.getRessource(j).getNom() && recette[i].Quantite > this.getRessource(j).Quantite)
                        isDispo = false;
                }
            }            
            return isDispo;
        }
        //Méthode qui vérifie si la ressource en argument existe dans le stock
        public bool existeStock(string nomRessource)
        {
            bool existe = false;
            for (int i=0;i< _stock.Count();i++)
            {
                if (_stock[i].getNom() == nomRessource)
                    existe = true;
            }
            return existe;
        }
        //Méthode ToString qui affiche le contenu du stock lorsque l'on affiche la variable stock 
        public override string ToString()
        {
            string chRes = "";
            List<Ressource> types = new List<Ressource> { };
            types.Add(_stock[0]);
            int nbRessources = 0;
            //Boucle qui vérifie si la ressource est déjà parmi la liste de types que contient le stock
            for (int i = 0; i < getStockLength(); i++)
            {
                if (getQuantiteRessourceType(_stock[i].getNom()) != 0)
                {
                    nbRessources += 1;
                    bool isHere = false;
                    for (int j = 0; j < types.Count(); j++)
                    {
                        if (_stock[i].getNom() == types[j].getNom())
                            isHere = true;
                    }
                    if (isHere == false)
                        types.Add(_stock[i]);
                }
            }
            chRes += "Votre stock contient " + nbRessources + " types de ressources \n";
            //Spécification de la quantité pour chaque ressource
            for (int k = 0; k < types.Count(); k++)
            {
                chRes += "Il y a " + getQuantiteRessourceType(types[k].getNom()) + " ressource(s) de type '" + types[k].getNom() + "'\n";
            }

            return chRes;
        }
    }
}
