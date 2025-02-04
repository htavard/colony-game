using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Media;
using System.Diagnostics;
using System.Threading;

namespace colony_game
{
    class Structure
    {
        //Attribut de la classe Structure 
        protected List<Batiment> _structure;

        //Propriété Structure qui permet l'accès et la modification de la variable 
        public List<Batiment> _Structure { get; set; }

        //Constructeur de la classe Structure 
        public Structure()
        {
            _structure = new List<Batiment> { };
        }

        //Méthode qui retourne la taille de la structure, substitue de la méthode Count()
        public int getStructureLength()
        {
            return _structure.Count();
        }

        //Méthode qui retourne le nombre de bâtiment du type passé en argument présents dans la structure
        public int getNbBatType(string type)
        {
            int nbType = 0;
            for (int i = 0; i < _structure.Count(); i++)
            {
                if (_structure[i].getTypeBat() == type)
                {
                    nbType += 1;
                }
            }
            return nbType;
        }


        //Méthode qui ajoute un bâtiment à la structure, substitue de la méthode Add()
        public void addBatiment(Batiment b)
        {
            int cpt = 0;
            for (int i = 0; i < _structure.Count(); i++)
            {
                if (b.getTypeBat() == _structure[i].getTypeBat())
                {
                    cpt += 1;
                    b.Quantite += 1;
                }
            }
            if (cpt == 0)
                _structure.Add(b);
        }

        //Méthode qui retire un bâtiment de la structure, substitue de la méthode Remove()
        public void removeBatiment(Batiment b)
        {
            
            for (int i = 0; i < _structure.Count(); i++)
            {
                if (b.getTypeBat() == _structure[i].getTypeBat())
                {
                    _structure.Remove(_structure[i]);
                    break;
                }
            }
            
        }

        //Méthode qui retourne le bâtiment ayant pour indice l'entier passé en argument, substitue de structure[indice]
        public Batiment getBatiment(int indice)
        {
            return _structure[indice];
        }

        //Méthode qui demande au joueur quel type de bâtiment il souhaite construire, puis l'ajoute à la structure (s'il a les ressources nécessaires) et vide le stock en conséquence 
        public void choixConstruction(Stock _stockvillage, List<List<Ressource>> _recettesBat,Grille grilleJeu)
        {
            

            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                string ecranConstruction = File.ReadAllText("constructionbat.txt");
                Console.WriteLine(ecranConstruction);
                Console.WriteLine("\n\n\n" + _stockvillage + "\n\n");
                Console.WriteLine("\nQuel type de bâtiment voulez-vous construire ? Usine (U)? Laboratoire (L) ? Echap pour quitter... ");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;
                if (keyPressed == ConsoleKey.U)
                {
                    bool possede = false;
                    if (grilleJeu.existe("☼"))
                        possede = true;
                    Console.ReadLine();
                    bool faisable = _stockvillage.isDispoStock(_recettesBat[0]);
                    if (faisable && !possede)
                    {
                        Console.Write("Comment voulez-vous appelez votre usine ? ");
                        string choixNom = Console.ReadLine();
                        Batiment u = new Batiment("Usine", choixNom, _stockvillage, "☼");
                        //Ajout du bâtiment à la structure 
                        this.addBatiment(u);
                        //Retrait du stock des ressources nécessaires à la construction 
                        for (int i = 0; i < _recettesBat[0].Count(); i++)
                        {
                            for (int j = 0; j < _stockvillage.getStockLength(); j++)
                                if (_stockvillage.getRessource(j).getNom() == _recettesBat[0][i].getNom())
                                    _stockvillage.getRessource(j).Quantite -= _recettesBat[0][i].Quantite;
                        }
                        //Placement du bâtiment sur la grille à l'aide des flèches directionnelles
                        Console.WriteLine("\nOù voulez vous placer votre usine ? (vous pouvez choisir l'emplacement avec les fleches directionnelles. Appuyez sur entrée pour continuer...");
                        Console.ReadLine();
                        grilleJeu.PlacementGrille(grilleJeu.recupPosition("⌂")[0], grilleJeu.recupPosition("⌂")[1], "☼");
                       
                        Console.WriteLine("Usine construite avec succès ! Appuyez sur entrée pour continuer...");
                        Console.ReadLine();

                    }
                    else if(possede)
                    {
                        Console.WriteLine("Vous disposez déjà d'une usine, et notre souhait de préserver la nature surpasse votre soif de ressource, il va falloir être patient !");
                        Console.WriteLine("\n\t\t Appuyez sur entrée pour continuer...");
                        Console.ReadLine();
                    }
                    //Cas où le joueur ne dispose pas des ressources demandées 
                    else
                    {

                        Console.WriteLine("Vous ne disposez pas des ressources nécessaires pour construire une usine, retentez plus tard !");
                        Console.WriteLine("\n\t\t Appuyez sur entrée pour continuer...");
                        Console.ReadLine();

                    }

                    
                }
                else if (keyPressed == ConsoleKey.L)
                {
                    bool possede = false;
                    if (grilleJeu.existe("♦"))
                        possede = true;
                    bool faisable = _stockvillage.isDispoStock(_recettesBat[1]);
                    if (faisable && !possede)
                    {
                        Console.ReadLine();
                        Console.WriteLine("Comment voulez-vous appelez votre laboratoire ? ");
                        string choixNom = Console.ReadLine();
                        Batiment l = new Batiment("Laboratoire", choixNom, _stockvillage, "♦");
                        //Ajout du bâtiment à la structure 
                        this.addBatiment(l);
                        //Retrait du stock des ressources nécessaires à la construction 
                        for (int i = 0; i < _recettesBat[1].Count(); i++)
                        {
                            for (int j = 0; j < _stockvillage.getStockLength(); j++)
                                if (_stockvillage.getRessource(j).getNom() == _recettesBat[1][i].getNom())
                                    _stockvillage.getRessource(j).Quantite -= _recettesBat[1][i].Quantite;
                        }
                        //Placement du bâtiment sur la grille à l'aide des flèches directionnelles
                        Console.WriteLine("\nOù voulez vous placer votre laboratoire ? (vous pouvez choisir l'emplacement avec les fleches directionnelles. Appuyez sur entrée pour continuer...");
                        Console.ReadLine();
                        grilleJeu.PlacementGrille(grilleJeu.recupPosition("⌂")[0], grilleJeu.recupPosition("⌂")[1], "♦");
                        
                        Console.WriteLine("Laboratoire construit avec succès ! Appuyez sur entrée pour continuer...");
                        Console.ReadLine();
                    }
                    else if(possede)
                    {
                        Console.WriteLine("Vous disposez déjà d'un laboratoire et nous savons à quel point il est dangereux d'y concocter, c'est pourquoi vous ne pouvez en avoir qu'un.");
                        Console.WriteLine("\n\t\t Appuyez sur entrée pour continuer...");
                        Console.ReadLine();
                    }
                    //Cas où le joueur ne dispose pas des ressources demandées 
                    else
                    {
                        Console.WriteLine("Vous ne disposez pas des ressources nécessaires pour construire un laboratoire, retentez plus tard !");
                        Console.WriteLine("\n\t\t Appuyez sur entrée pour continuer...");
                        Console.ReadLine();
                    }                       
                }
            }
            while (keyPressed != ConsoleKey.U && keyPressed != ConsoleKey.L && keyPressed!=ConsoleKey.Escape);
            
        }

        //Méthode qui convertit un minerais en un fragment aléatoire, sous réserve que le joueur ait construit une usine 
        public void transformerUsine(Stock _stockvillage,Mineuse m, Grille grilleJeu)
        {
            bool parcouru = false;
            Ressource minerais = new Ressource("Minerais", 1);
            Ressource or = new Ressource("Or", 1);
            Ressource argent = new Ressource("Argent", 1);
            Ressource verre = new Ressource("Verre", 1);
            int i = 0;

            if (this.getStructureLength() == 0)
            {
                Console.WriteLine("Vous n'avez pas d'usine... Tant pis, ça ramasse ça ramasse !");
                m.recolter(grilleJeu);

            }
            else
            {
                while (i < _stockvillage.getStockLength() && parcouru == false)
                {
                    for (int j = 0; j < this.getStructureLength(); j++)
                    {
                        if (this.getBatiment(j).getTypeBat() == "Usine")
                        {
                            if (_stockvillage.getRessource(i).getNom() == minerais.getNom() && _stockvillage.getRessource(i).Quantite > 0)
                            {
                                _stockvillage.removeRessource(minerais);
                                grilleJeu.mineuseUsine();
                                Random alea = new Random();
                                int proba = alea.Next(6);
                                if (proba == 0)
                                {
                                    for(int k=1;k<=this.getBatiment(j)._nbProduction;k++)
                                        _stockvillage.addRessource(or);
                                    Console.WriteLine("Transformation de minerais : "+this.getBatiment(j)._nbProduction+" or en plus dans le stock !");
                                }

                                else if (proba == 1 || proba == 2)
                                {
                                    for (int k = 1; k <= this.getBatiment(j)._nbProduction; k++)
                                        _stockvillage.addRessource(argent);
                                    Console.WriteLine("Transformation de minerais : " + this.getBatiment(j)._nbProduction + " argent en plus dans le stock !");
                                }
                                else if (proba == 3 || proba == 4 || proba==5)
                                {
                                    for (int k = 1; k <= this.getBatiment(j)._nbProduction; k++)
                                        _stockvillage.addRessource(verre);
                                    Console.WriteLine("Transformation de minerais : " + this.getBatiment(j)._nbProduction + " verre en plus dans le stock !");
                                }
                                parcouru = true;

                            }
                            else if (_stockvillage.getRessource(i).getNom() == minerais.getNom() && _stockvillage.getRessource(i).Quantite <= 0)
                            {
                                parcouru = true;
                                Console.WriteLine("Vous n'avez pas de minerais à transformer... Vous en ramassez immédiatement !");
                                m.recolter(grilleJeu);
                                Console.WriteLine("1 minerai en plus dans le stock !");

                            }
                            i++;
                        }
                    }



                }

            }

        }

        //Méthode qui range dans une liste de listes les recettes indiquant les ressources nécessaires à la construction/l'amélioration des bâtiments 
        public List<List<Ressource>> initialiserRecettes()
        {
            List<List<Ressource>> lesRecettes = new List<List<Ressource>> { };
            Ressource bois = new Ressource("Bois", 3);
            Ressource minerais = new Ressource("Minerais", 3);
            Ressource argent = new Ressource("Argent", 1);
            Ressource or = new Ressource("Or", 1);
            Ressource verre = new Ressource("Verre", 2);
            Ressource bcpbois = new Ressource("Bois", 15);


            //Recette création d'usine : indice 0
            List<Ressource> creationUsine = new List<Ressource> { };
            creationUsine.Add(minerais);
            creationUsine.Add(bois);
            lesRecettes.Add(creationUsine);

            //Recette création de laboratoire : indice 1
            List<Ressource> creationLaboratoire = new List<Ressource> { };
            creationLaboratoire.Add(argent);
            creationLaboratoire.Add(verre);
            creationLaboratoire.Add(bois);
            lesRecettes.Add(creationLaboratoire);

            //Recette amélioration usine : indice 2
            List<Ressource> ameliorationUsine = new List<Ressource> { };
            ameliorationUsine.Add(argent);
            ameliorationUsine.Add(verre);
            ameliorationUsine.Add(bcpbois);
            lesRecettes.Add(ameliorationUsine);

            //Recette amélioration laboratoire : indice 3
            List<Ressource> ameliorationLaboratoire = new List<Ressource> { };
            ameliorationLaboratoire.Add(or);
            ameliorationLaboratoire.Add(verre);
            ameliorationUsine.Add(bcpbois);
            lesRecettes.Add(ameliorationLaboratoire);

            return lesRecettes;

        }

        //Méthode ToString qui lorsqu'on l'appelle affiche le contenu de la structure 
        public override string ToString()
        {
            string chRes = "";
            chRes += "Votre village contient " + getStructureLength() + " bâtiment(s) \n";
            List<Batiment> types = new List<Batiment> { };
            if(_structure.Count() > 0)
            {
                    //Boucle qui répertorie les types de bâtiments dans une liste 
                    for (int i = 0; i < this.getStructureLength(); i++)
                    {

                        types.Add(_structure[i]);
                    }

                    for (int k = 0; k < types.Count(); k++)
                    {
                        string type = types[k].getTypeBat();
                        chRes += "\nIl y a " + getNbBatType(types[k].getTypeBat()) + " bâtiments(s) de type '" + type + "'\n";
                        //Affichage spécifique selon le type de bâtiment 
                        for (int i = 0; i < this.getStructureLength(); i++)
                        {
                            if (_structure[i].getTypeBat() == type && type=="Usine")
                            {
                                chRes += this.getBatiment(i).getNom() + " : Niveau " + this.getBatiment(i).Niveau + ", produira " + this.getBatiment(i).NbProduction + " fragment(s) aléatoirement en échange d'un minerais \n";
                            } 
                            else if (_structure[i].getTypeBat() == type && type == "Laboratoire")
                            {
                                chRes += this.getBatiment(i).getNom() + " : Niveau " + this.getBatiment(i).Niveau + ", vous permet de concocter de superbes potions !!!!!!!!! \n";

                            }

                        }
                    }
            } else
            {
                chRes += "Il n'y a pour l'instant aucun bâtiment dans votre village";
            }
            

            return chRes;
        }

    }
}

