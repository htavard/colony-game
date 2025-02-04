using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace colony_game
{
    class Grille
    {
        //Attribut de la classe Grille
        private string[,] grilleJeu;

        //Constructeur de la classe Grille 
        public Grille(int lignes, int colonnes)
        {

            grilleJeu = new string[lignes, colonnes];
            for (int i = 0; i < lignes; i++)
            {
                for (int j = 0; j < colonnes; j++)
                {
                    grilleJeu[i, j] = " ";
                }
            }
        }

        //Méthode qui substitue Random() et règle le problème du "faux aléatoire"
        Random alea = new Random();
        public int RandomNumber(int a, int b)
        {
            lock (this)
            {
                return alea.Next(a, b);
            }
        }

        //Méthode qui permet d'afficher la grille de jeu
        public void AfficherGrille()

        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("      .");
            for (int j = 0; j < grilleJeu.GetLength(1); j++)
                Console.Write("___.");
            Console.WriteLine();
            for (int i = 0; i < grilleJeu.GetLength(0); i++)
            {
                Console.Write("      |");
                for (int j = 0; j < grilleJeu.GetLength(1); j++)
                {

                    Console.Write(" ");
                    if (grilleJeu[i, j] == "♣")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(grilleJeu[i, j]);
                    }
                    else if (grilleJeu[i, j] == "▒")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(grilleJeu[i, j]);
                    }
                    else if (grilleJeu[i, j] == "↓")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(grilleJeu[i, j]);
                    }
                    else if (grilleJeu[i, j] == "ζ")
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(grilleJeu[i, j]);
                    }
                    else if (grilleJeu[i, j] == "ξ")
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(grilleJeu[i, j]);
                    }
                    else if (grilleJeu[i, j] == "π")
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(grilleJeu[i, j]);
                    }
                    else if (grilleJeu[i, j] == "σ")
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write(grilleJeu[i, j]);
                    }
                    else if (grilleJeu[i, j] == "Σ")
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(grilleJeu[i, j]);
                    }
                    else if (grilleJeu[i, j] == "Φ")
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write(grilleJeu[i, j]);
                    }
                    else if (grilleJeu[i, j] == "θ")
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(grilleJeu[i, j]);
                    }
                    else if (grilleJeu[i, j] == "Ω")
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write(grilleJeu[i, j]);
                    }
                    else if (grilleJeu[i, j] == "⌂")
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(grilleJeu[i, j]);
                    }
                    else if (grilleJeu[i, j] == "☼")
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(grilleJeu[i, j]);
                    }
                    else if (grilleJeu[i, j] == "♦")
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(grilleJeu[i, j]);
                    }
                    else if (grilleJeu[i, j].All(char.IsDigit))
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write(grilleJeu[i, j]);
                    }
                    else
                        Console.Write(" ");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(" |");
                }

                Console.WriteLine();
                Console.Write("      |");
                for (int j = 0; j < grilleJeu.GetLength(1); j++)
                    Console.Write("___|");

                Console.WriteLine(" ");
            }
            Console.WriteLine("\n\t\t Appuyez sur entrée pour continuer...");
            Console.ResetColor();

        }

        //Méthode qui permet de créer et afficher une mine dans la grille de jeu
        public string[,] CreerMine() 
        {

            int quelBord = RandomNumber(0, 4);
            int colDepart = 0;
            int ligDepart = 0;
            int colArrivee = 0;
            int ligArrivee = 0;
            int bordAdja = RandomNumber(0, 2);
            if (quelBord == 0)//bord gauche
            {
                ligDepart = RandomNumber(0, grilleJeu.GetLength(0)); //départ
                colDepart = 0;
                colArrivee = RandomNumber(0, grilleJeu.GetLength(1));
                if (bordAdja == 0) //bord adjacent à droite de celui choisi, ici c'est le bas
                    ligArrivee = grilleJeu.GetLength(0) - 1;
                else //bord adjacent gauche
                    ligArrivee = 0;
            }
            else if (quelBord == 1)//bord bas
            {
                ligDepart = grilleJeu.GetLength(0) - 1;
                colDepart = RandomNumber(0, grilleJeu.GetLength(1));
                ligArrivee = RandomNumber(0, grilleJeu.GetLength(0));
                if (bordAdja == 0)
                    colArrivee = grilleJeu.GetLength(1) - 1;
                else
                    colArrivee = 0;
            }
            else if (quelBord == 2)//bord droite
            {
                ligDepart = RandomNumber(0, grilleJeu.GetLength(0));
                colDepart = grilleJeu.GetLength(1) - 1;
                colArrivee = RandomNumber(0, grilleJeu.GetLength(1));
                if (bordAdja == 0) //bord adjacent à droite de celui choisi, ici c'est le haut
                    ligArrivee = 0;
                else
                    //bord adjacent gauche
                    ligArrivee = grilleJeu.GetLength(0) - 1;
            }
            else if (quelBord == 3)//bord haut
            {
                ligDepart = 0;
                colDepart = RandomNumber(0, grilleJeu.GetLength(1));
                ligArrivee = RandomNumber(0, grilleJeu.GetLength(0));
                if (bordAdja == 0)
                    colArrivee = 0;
                else
                    colArrivee = grilleJeu.GetLength(1) - 1;
            }
            grilleJeu[ligDepart, colDepart] = "▒";
            grilleJeu[ligArrivee, colArrivee] = "▒";
            //il faut que les deux points se rejoignent !
            while ((ligDepart != ligArrivee && colDepart != colArrivee) || (Math.Abs(ligDepart - ligArrivee) + Math.Abs(colDepart - colArrivee) > 3))
            {
                if (ligDepart >= ligArrivee && ligDepart > 1)
                {
                    ligDepart -= 1;
                    grilleJeu[ligDepart, colDepart] = "▒";
                    ligDepart -= 1;
                    grilleJeu[ligDepart, colDepart] = "▒";
                    if (colDepart >= colArrivee && colDepart > 0)
                    {
                        colDepart -= 1;
                        grilleJeu[ligDepart, colDepart] = "▒";
                    }
                    else if (colDepart < grilleJeu.GetLength(1) - 1)
                    {
                        colDepart += 1;
                        grilleJeu[ligDepart, colDepart] = "▒";
                    }
                }
                else if (ligDepart < grilleJeu.GetLength(0) - 1)
                {
                    ligDepart += 1;
                    grilleJeu[ligDepart, colDepart] = "▒";
                    if (colDepart >= colArrivee && colDepart > 1)
                    {
                        colDepart -= 1;
                        grilleJeu[ligDepart, colDepart] = "▒";
                        colDepart -= 1;
                        grilleJeu[ligDepart, colDepart] = "▒";
                    }
                    else if (colDepart < grilleJeu.GetLength(1) - 2)
                    {
                        colDepart += 1;
                        grilleJeu[ligDepart, colDepart] = "▒";
                        colDepart += 1;
                        grilleJeu[ligDepart, colDepart] = "▒";
                    }
                }
            }



            return grilleJeu;
        }

        //Méthode qui permet de rendre vide une case occupée par un symbole passé en argument 
        public void retirerSymbole(string symbole)
        {
            for (int i = 0; i < grilleJeu.GetLength(0); i++)
            {
                for (int j = 0; j < grilleJeu.GetLength(1); j++)
                    if(grilleJeu[i, j] == symbole)
                    {
                        grilleJeu[i, j] = " ";
                    }
            }
        }

        public void PlacementGrille(int i, int j, string symboleArrivee)
        {
            ConsoleKey keyPressed;
            int[] posBatiment = new int[2];
            string[,] grilleRef = grilleJeu;
            bool aPose = false;
            do
            {
                do
                {


                    string caseDeBase = grilleJeu[i, j];
                    grilleJeu[i, j] = "↓";
                    Console.Clear();
                    AfficherGrille();

                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    keyPressed = keyInfo.Key;
                    grilleJeu[i, j] = caseDeBase;
                    if (keyPressed == ConsoleKey.UpArrow)
                    {
                        if (i == 0)
                            i = grilleJeu.GetLength(0);
                        i--;
                    }
                    else if (keyPressed == ConsoleKey.DownArrow)
                    {
                        if (i == grilleJeu.GetLength(0) - 1)
                            i = -1;
                        i++;
                    }
                    else if (keyPressed == ConsoleKey.LeftArrow)
                    {
                        if (j == 0)
                            j = grilleJeu.GetLength(1);
                        j--;
                    }
                    else if (keyPressed == ConsoleKey.RightArrow)
                    {
                        if (j == grilleJeu.GetLength(1) - 1)
                            j = -1;
                        j++;
                    }
                    posBatiment[0] = i; posBatiment[1] = j;
                }

                while (keyPressed != ConsoleKey.Enter);

                if (grilleRef[posBatiment[0], posBatiment[1]] == " ")
                    aPose = true;
                else
                {
                    Console.WriteLine("Vous ne pouvez pas construire ici, c'est du vandalisme !\n\nAppuyez sur entrée pour continuer...");
                    Console.ReadLine();
                }

            }
            while (!aPose);
            grilleJeu[posBatiment[0], posBatiment[1]] = symboleArrivee;
        }

        //Méthode qui permet de créer aléatoirement une forêt dans la grille de jeu
        public string[,] CreerForet() 
        {

            int ligneDepart = RandomNumber(0, grilleJeu.GetLength(0));
            int colDepart = RandomNumber(0, grilleJeu.GetLength(1));
            int ligneSuivante = ligneDepart;
            int colSuivante = colDepart;
            grilleJeu[ligneDepart, colDepart] = "♣";
            for (int i = 0; i < 100; i++)
            {
                while ((ligneSuivante == ligneDepart && colSuivante == colDepart) || (ligneSuivante >= grilleJeu.GetLength(0) || colSuivante >= grilleJeu.GetLength(1) || ligneSuivante < 0 || colSuivante < 0))
                {
                    ligneSuivante = ligneDepart + RandomNumber(-1, 2);
                    colSuivante = colDepart + RandomNumber(-1, 2);
                }
                ligneDepart = ligneSuivante;
                colDepart = colSuivante;
                grilleJeu[ligneDepart, colDepart] = "♣";

            }


            return grilleJeu;
        }

        //Méthode qui efface le contenu de la grille 
        public string[,] EffacerGrille()
        {
            for (int i = 0; i < grilleJeu.GetLength(0); i++)
            {
                for (int j = 0; j < grilleJeu.GetLength(1); j++)
                    grilleJeu[i, j] = " ";
            }
            return grilleJeu;
        }

        ////Méthode qui permet de vérifier qu'un carré de 5*5 est disponible à partir de la case centrale
        public bool isCarreDispo(int ligneDepart, int colonneDepart) 
        {
            bool dispo = false;
            if (ligneDepart - 2 >= 0 && ligneDepart + 2 < grilleJeu.GetLength(0) && colonneDepart - 2 >= 0 && colonneDepart + 2 < grilleJeu.GetLength(1))
            {
                dispo = true;
            }
            return dispo;
        }

        //Méthode qui crée et affiche le carré central où l'on retrouve le nombre de villageoise pour chaque type à côté de leur symbole respectif 
        public void dessineVillage(Village v)
        {
            if(existe("⌂")) //met a jour le nombre de villageoises 
            {
                int[] posVillage = recupPosition("⌂");
                int l = posVillage[0];
                int c = posVillage[1];
                grilleJeu[l - 2, c - 1] = v.getNbVillageoiseType("Botaniste").ToString();
                grilleJeu[l - 2, c] = v.getNbVillageoiseType("Bucheronne").ToString();
                grilleJeu[l - 1, c + 2] = v.getNbVillageoiseType("Combattante").ToString();
                grilleJeu[l, c + 2] = v.getNbVillageoiseType("Cuisiniere").ToString();
                grilleJeu[l + 2, c + 1] = v.getNbVillageoiseType("Guerisseuse").ToString();
                grilleJeu[l + 2, c] = v.getNbVillageoiseType("Jardiniere").ToString();
                grilleJeu[l + 1, c - 2] = v.getNbVillageoiseType("Mineuse").ToString();
                grilleJeu[l, c - 2] = v.getNbVillageoiseType("Occultiste").ToString();
            }
            else
            {
                int ligneDepart = (grilleJeu.GetLength(0) / 2);
                int colDepart = (grilleJeu.GetLength(1) / 2);
                while (!isCarreDispo(ligneDepart, colDepart))
                {
                    if (colDepart + 1 >= grilleJeu.GetLength(1))
                    {
                        colDepart = 0;
                        if (!isCarreDispo(ligneDepart, colDepart))
                        {
                            if (ligneDepart + 1 >= grilleJeu.GetLength(0))
                                ligneDepart = 0;
                            else
                                ligneDepart += 1;
                        }
                        else
                        {
                            colDepart += 1;
                            if (ligneDepart + 1 >= grilleJeu.GetLength(0))
                                ligneDepart = 0;
                            else
                                ligneDepart += 1;
                        }
                    }
                }
                grilleJeu[ligneDepart, colDepart] = "⌂";
                grilleJeu[ligneDepart - 1, colDepart - 1] = "ζ";
                grilleJeu[ligneDepart - 2, colDepart - 1] = v.getNbVillageoiseType("Botaniste").ToString();
                grilleJeu[ligneDepart - 1, colDepart] = "ξ";
                grilleJeu[ligneDepart - 2, colDepart] = v.getNbVillageoiseType("Bucheronne").ToString();
                grilleJeu[ligneDepart - 1, colDepart + 1] = "π";
                grilleJeu[ligneDepart - 1, colDepart + 2] = v.getNbVillageoiseType("Combattante").ToString();
                grilleJeu[ligneDepart, colDepart + 1] = "σ";
                grilleJeu[ligneDepart, colDepart + 2] = v.getNbVillageoiseType("Cuisiniere").ToString();
                grilleJeu[ligneDepart + 1, colDepart + 1] = "Σ";
                grilleJeu[ligneDepart + 2, colDepart + 1] = v.getNbVillageoiseType("Guerisseuse").ToString();
                grilleJeu[ligneDepart + 1, colDepart] = "Φ";
                grilleJeu[ligneDepart + 2, colDepart] = v.getNbVillageoiseType("Jardiniere").ToString();
                grilleJeu[ligneDepart + 1, colDepart - 1] = "θ";
                grilleJeu[ligneDepart + 1, colDepart - 2] = v.getNbVillageoiseType("Mineuse").ToString();
                grilleJeu[ligneDepart, colDepart - 1] = "Ω";
                grilleJeu[ligneDepart, colDepart - 2] = v.getNbVillageoiseType("Occultiste").ToString();
            }           
        }
        
        //Méthode qui récupère dans un tableau d'entiers la position d'un symbole passé en argument 
        public int[] recupPosition(string recherche)
        {
            int[] pos = { 0, 0 };
            for (int i = 0; i < grilleJeu.GetLength(0); i++)
            {
                for (int j = 0; j < grilleJeu.GetLength(1); j++)
                {
                    if (grilleJeu[i, j] == recherche)
                    {
                        pos[0] = i;
                        pos[1] = j;
                    }
                }
            }
            return pos;
        }

        //Méthode qui enlève un arbre apèrs qu'une bucheronne se soit placée sur cette même case 
        public void enleveArbre(string symboleDestination, string symboleVillageoise)
        {
            int[] posAbre = new int[2];
            int[] posDepart = recupPosition("⌂");
            List<List<int>> positionArbres = new List<List<int>>();
            List<int> positionActuelleArbre = new List<int>();
            positionActuelleArbre.Add(0);
            positionActuelleArbre.Add(0);
            List<double> distanceArbreVillage = new List<double>();
            for (int i = 0; i < grilleJeu.GetLength(0); i++)
            {
                for (int j = 0; j < grilleJeu.GetLength(1); j++)
                {
                    if (grilleJeu[i, j] == symboleDestination)
                    {
                        positionActuelleArbre[0] = i;
                        positionActuelleArbre[1] = j;
                        double distance = Math.Sqrt((Math.Abs(i - posDepart[0]) ^ 2) + Math.Abs(j - posDepart[1]) ^ 2);
                        distanceArbreVillage.Add(distance);
                        positionArbres.Add(positionActuelleArbre);
                    }
                }
            }
            double minDistance = Math.Sqrt((30-posDepart[0])^2+(50-posDepart[1])^2);
            int index = 0;
            for (int k = 1; k < distanceArbreVillage.Count; k++)
            {
                if (minDistance >= distanceArbreVillage[k])
                {
                    minDistance = distanceArbreVillage[k];
                    index = k;
                }
            }
            grilleJeu[positionArbres[index][0], positionArbres[index][1]] = symboleVillageoise;

        }

        //Méthode qui vérifie si un symbole passé en argument est déjà dans la grille de jeu
        public bool existe(string symbole)
        {
            bool existe = false;
            for (int i = 0; i < grilleJeu.GetLength(0); i++)
            {
                for (int j = 0; j < grilleJeu.GetLength(1); j++)
                {
                    if (grilleJeu[i, j] == symbole)
                        existe = true;
                }
            }
            return existe;
        }

        //Méthode qui permet de faire revenir une villageoise au centre de la map après s'être déplacée vers son lieux de travail 
        public void rentrerVillageoise(string symbole)
        {
            for (int i = 0; i < grilleJeu.GetLength(0); i++)
            {
                for (int j = 0; j < grilleJeu.GetLength(1); j++)
                {
                    if (grilleJeu[i, j] == symbole)
                    {
                        if (symbole == "ξ")
                        {
                            if ((i + 1 >= grilleJeu.GetLength(0)) || grilleJeu[i + 1, j] != "⌂")
                                grilleJeu[i, j] = " ";
                        }
                        else if (symbole == "ζ")
                        {
                            if ((i + 1 == grilleJeu.GetLength(0)) || j + 1 == grilleJeu.GetLength(0) || grilleJeu[i + 1, j + 1] != "⌂")
                                grilleJeu[i, j] = " ";
                        }
                        else if (symbole == "Φ")
                        {
                            if ((i - 1 == 0) || grilleJeu[i - 1, j] != "⌂")
                                grilleJeu[i, j] = " ";
                        }
                        else if (symbole == "θ")
                        {
                            if ((j + 1 == grilleJeu.GetLength(1)) || i == 0 || grilleJeu[i - 1, j + 1] != "⌂")
                            {

                                if (existe("☼"))
                                {
                                    int[] posUsine = recupPosition("☼");
                                    int l = posUsine[0];
                                    int c = posUsine[1];
                                    if (c - 1 >= 0 && grilleJeu[l, c - 1] == "θ")
                                    {
                                        grilleJeu[l, c - 1] = " ";
                                    }
                                    else if (l - 1 >= 0 && grilleJeu[l - 1, c] == "θ")
                                    {
                                        grilleJeu[l - 1, c] = " ";
                                    }
                                    else if (c + 1 < grilleJeu.GetLength(1) && grilleJeu[l, c + 1] == "θ")
                                    {
                                        grilleJeu[l, c + 1] = " ";
                                    }
                                    else if (l + 1 < grilleJeu.GetLength(0) && grilleJeu[l + 1, c] == "θ")
                                    {
                                        grilleJeu[l + 1, c] = " ";
                                    }
                                    else
                                        grilleJeu[i, j] = "▒";
                                }
                                else
                                    grilleJeu[i, j] = "▒";
                            }

                        }
                    }
                }
            }
        }

        //Méthode qui envoie les mineuses sur les cases de la mine 
        public void mineuseMine()
        {
            int indexMine;
            List<List<int>> positionsMines = new List<List<int>>();

            for (int i = 0; i < grilleJeu.GetLength(0); i++)
            {
                for (int j = 0; j < grilleJeu.GetLength(1); j++)
                {
                    if (grilleJeu[i, j] == "▒")
                    {
                        List<int> posMine = new List<int>();
                        posMine.Add(i); posMine.Add(j);
                        positionsMines.Add(posMine);
                    }

                }
            }
            indexMine = RandomNumber(0, positionsMines.Count());
            List<int> choixMine = positionsMines[indexMine];
            grilleJeu[choixMine[0], choixMine[1]] = "θ";

        }

        //Méthode qui vérifie si une case dont la position est passée en argument est occupée par un symbole quelconque 
        public bool isCaseLibre(int i, int j)
        {
            bool libre = true;
            if (i < 0 || i >= grilleJeu.GetLength(0) || j < 0 || j >= grilleJeu.GetLength(1))
                libre = false;
            else if (grilleJeu[i, j] != " ")
                libre = false;
            return libre;
        }

        //Méthode qui envoie les mineuses sur une case adjacente à l'usine 
        public void mineuseUsine()
        {
            if(existe("☼"))
            {
                int indexUsine;
                List<List<int>> positionsUsines = new List<List<int>>();

                for (int i = 0; i < grilleJeu.GetLength(0); i++)
                {
                    for (int j = 0; j < grilleJeu.GetLength(1); j++)
                    {
                        if (grilleJeu[i, j] == "☼")
                        {
                            List<int> posUsine = new List<int>();
                            posUsine.Add(i); posUsine.Add(j);
                            positionsUsines.Add(posUsine);
                        }

                    }
                }
                indexUsine = RandomNumber(0, positionsUsines.Count());
                List<int> choixMine = positionsUsines[indexUsine];
                if (isCaseLibre(choixMine[0], choixMine[1] - 1))
                    grilleJeu[choixMine[0], choixMine[1] - 1] = "θ";
                else if (isCaseLibre(choixMine[0] - 1, choixMine[1]))
                    grilleJeu[choixMine[0] - 1, choixMine[1]] = "θ";
                else if (isCaseLibre(choixMine[0], choixMine[1] + 1))
                    grilleJeu[choixMine[0], choixMine[1] + 1] = "θ";
                else if (isCaseLibre(choixMine[0] + 1, choixMine[1]))
                    grilleJeu[choixMine[0] + 1, choixMine[1]] = "θ";
                else
                    Console.WriteLine("Il n'y a plus de place autour de cette usine !");
            }
            
        }

        //Méthode qui envoie les botanistes sur une case adjacente au labo  
        public void BotanisteLabo()
        {   
            if (existe("♦"))
            {
                int indexLabo;
                List<List<int>> positionsLabos = new List<List<int>>();

                for (int i = 0; i < grilleJeu.GetLength(0); i++)
                {
                    for (int j = 0; j < grilleJeu.GetLength(1); j++)
                    {
                        if (grilleJeu[i, j] == "♦")
                        {
                            List<int> posLab = new List<int>();
                            posLab.Add(i); posLab.Add(j);
                            positionsLabos.Add(posLab);
                        }
                    }
                }
                indexLabo = RandomNumber(0, positionsLabos.Count());
                List<int> choixLabo = positionsLabos[indexLabo];
                if (isCaseLibre(choixLabo[0], choixLabo[1] - 1))
                    grilleJeu[choixLabo[0], choixLabo[1] - 1] = "ζ";
                else if (isCaseLibre(choixLabo[0] - 1, choixLabo[1]))
                    grilleJeu[choixLabo[0] - 1, choixLabo[1]] = "ζ";
                else if (isCaseLibre(choixLabo[0], choixLabo[1] + 1))
                    grilleJeu[choixLabo[0], choixLabo[1] + 1] = "ζ";
                else if (isCaseLibre(choixLabo[0] + 1, choixLabo[1]))
                    grilleJeu[choixLabo[0] + 1, choixLabo[1]] = "ζ";
                else
                    Console.WriteLine("Il n'y a plus de place autour de ce laboratoire !");
            }
            
        }
    }
}
