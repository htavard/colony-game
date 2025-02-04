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

    class Game
    {
        //Attributs de la classe Game 
        private Grille grilleJeu;
        private int nbActionsJournee; //Nombre d'action max par tour
        public int NbActionsJournee { get { return nbActionsJournee; } }
        public static int tourActuel = 1;


        //Constructeur de Game 
        public Game()
        {
            nbActionsJournee = 5;
            grilleJeu = new Grille(22, 40);
        }

        //Permet de répercuter le "coût" d'une action sur le total des actions possibles sur une journée
        public void SetActionJournee(int cout)
        {
            if (nbActionsJournee - cout >= 0)
                nbActionsJournee -= cout;
            else
                Console.WriteLine("Vous n'avez pas assez d'actions restantes pour effectuer cela, terminez votre tour en effectuant une action moins couteuse.");
        }

        //Fonction qui lance aléatoirement l'attaque du monstre et applique ses conséquences au village
        public void attaqueMechenchon(Village v, Stock s, Structure st, Grille grilleJeu)
        {
            int probaAttaque = RandomNumber(0, 12);
            if (probaAttaque == 1)
            {
                string mechantScreen = File.ReadAllText("assets/sprites/mechenchon.txt");
                //Affichage de l'écran d'attaque du méchenchon 
                Console.Clear();
                Console.WriteLine(mechantScreen);
                int nbCombattantes = v.getNbVillageoiseType("Combattante");
                List<Combattante> lesCombattantes = new List<Combattante> { };
                //Créer une liste avec toutes les combattantes du village
                for (int i = 0; i < v.getTailleVillage(); i++)
                {
                    if (v.getVillageoise(i).getTypeVillageoise() == "Combattante")
                        lesCombattantes.Add((Combattante)v.getVillageoise(i));
                }
                //Index pour choisir une combattante au hasard 
                int indexCombattante = RandomNumber(0, lesCombattantes.Count());
                int niveauMax = 1;
                //Déterminer le niveau maximum de combat au sein du village 
                for (int j = 0; j < lesCombattantes.Count(); j++)
                {
                    if (lesCombattantes[j].getExperienceCombat() > niveauMax)
                        niveauMax = lesCombattantes[j].getExperienceCombat();
                }
                //Pas de combattante : game over 
                if (nbCombattantes == 0)
                {
                    string mort = File.ReadAllText("assets/sprites/gameover.txt");
                    Console.WriteLine(mort);
                    Console.WriteLine("\n Vous n'aviez pas assez de combattantes pour déjouer l'attaque du terrible et maléfique Méchenchon, il s'envole en criant : 'MOI ! MOI ! MAIS QUI elle...'");
                    Console.WriteLine("vous n'entendez pas la suite mais vous savez que votre village est anéanti, de même que vos chances de victoire. Appuyez sur entrée pour revenir au menu principal ...");
                    Console.ReadLine();
                    Start();
                }

                //Il y a des combattantes et le niveau max est de 1
                else if (nbCombattantes >= 1 && niveauMax == 1)
                {
                    lesCombattantes[indexCombattante].mourir(v,grilleJeu);
                    Console.WriteLine("\n Le méchenchon est sans pitié, et vos combattantes sont trop faibles! Vous perdez l'intégralité de votre stock et de vos bâtiments. Il s'envole en criant : 'MOI ! MOI ! MAIS QUI elle...'");
                    Console.WriteLine("vous n'entendez pas la suite mais vous savez que votre village est anéanti, et vos chances de victoire sabotées. Appuyez sur entrée pour continuer...");
                    for (int k = 0; k < s.getStockLength(); k++)
                        s.getRessource(k).Quantite = 0;
                    //Destruction des bâtiments 
                    for (int k = 0; k < st.getStructureLength(); k++)
                    {
                        grilleJeu.retirerSymbole(st.getBatiment(k).getSymbole());
                        st.removeBatiment(st.getBatiment(k));
                    }
                }
                //Il y a des combattantes et le niveau max est de 2
                else if (nbCombattantes >= 1 && niveauMax == 2)
                {
                    lesCombattantes[indexCombattante].mourir(v,grilleJeu);
                    Console.WriteLine("\n Le méchenchon est sans pitié, mais vos combattantes résistent! Vous perdez la moitié de votre stock et de vos bâtiments reviennent au niveau de base.  Il s'envole en criant : 'MOI ! MOI ! MAIS QUI elle...'");
                    Console.WriteLine("vous n'entendez pas la suite mais vous savez que votre village est affaibli, mais vous restez fort.e. Appuyez sur entrée pour continuer...");
                    for (int k = 0; k < s.getStockLength(); k++)
                        s.getRessource(k).Quantite /= 2;
                    //Retour au niveau 1 pour tous les bâtiments 
                    for (int k = 0; k < st.getStructureLength(); k++)
                        st.getBatiment(k).Niveau = 1;
                }
                //Il y a des combattantes et le niveau max est de 3
                else if (nbCombattantes >= 1 && niveauMax == 3)
                {
                    lesCombattantes[indexCombattante].mourir(v, grilleJeu);
                    Console.WriteLine("Le méchenchon n'est qu'une vaste arnaque selon vous et votre combattante de niveau 3 partie au combat s'en retrouve couverte de gloire. Tellement qu'elle finit par s'étouffer, finissant sa vie entre une rondelle de courge et un épis de blé...");
                    Console.WriteLine("Appuyez sur entrée pour continuer...");
                }
                Console.ReadLine();

            }
        }

        //Méthode qui permet le lancement d'une partie 
        public void Start()
        {
            //Lancement de la musique qui sera jouée en boucle durant la partie 
            var musiqueJeu = new SoundPlayer("assets/sounds/musiquejeu.wav");
            musiqueJeu.Load();
            musiqueJeu.PlayLooping();
            Console.Clear();
            //Permet de lire les symbole utilisés 
            Console.OutputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.Unicode;
            //Affichage écran d'accueil 
            string welcome = File.ReadAllText("assets/sprites/startscreen.txt");
            Console.WriteLine(welcome);
            Console.WriteLine("\nAppuyez sur entrée pour continuer...");
            Console.ReadLine();
            //Menu de départ 
            string[] optionsLancement = { "Jouer", "Règles du jeu", "Quitter" };
            Menu mainMenu = new Menu("Que voulez vous faire ? (vous pouvez utiliser les flèches directionnelles)", optionsLancement);
            int indexLancement = mainMenu.Run();
            //Instanciation du village, du stock et de la structure 
            Village v = new Village();
            Stock s = new Stock();
            Structure st = new Structure();
            //Initialisation du village, du stock et de la structure 
            s.initialiserStock();
            v.initialiserVillage(s);
            Bucheronne bobo = new Bucheronne("Racine", s);
            v.addVillageoise(bobo);

            switch (indexLancement)
            {
                //Cas où le joueur clique sur "Jouer"
                //Création de la grille et début de la partie avec la fonction Action()
                case 0:

                    grilleJeu.CreerForet();
                    grilleJeu.CreerForet();
                    grilleJeu.CreerMine();
                    grilleJeu.dessineVillage(v);

                    grilleJeu.AfficherGrille();
                    Console.ReadLine();
                    Action(v, s, st, grilleJeu);
                    Console.Read();
                    break;

                //Cas où le joueur clique sur "Règles du jeu"
                //Affiche le fichier etxte correspondant
                case 1:

                    Console.Clear();
                    string regles = File.ReadAllText("assets/sprites/reglesJeu.txt");
                    Console.WriteLine(regles);
                    Console.ReadLine();
                    this.Start();
                    break;

                //Cas où le joueur clique sur "Quitter"
                //Fermeture de la console et arrêt du jeu
                case 2:
                    ExitGame();
                    break;

            }
        }

        //Méthode qui substitut Random et règle le problème du "faux aléatoire"
        Random alea = new Random();
        public int RandomNumber(int a, int b)
        {
            lock (this)
                return alea.Next(a, b);
        }

        public void verifierMort(Village v)
        {
            //Parcourir le village
            for (int i = 0; i < v.getTailleVillage(); i++)
            {
                //Si la villageoise n'a plus de pdv
                if (v.getVillageoise(i).Pdv <= 0)
                {

                    if (v.getVillageoise(i).getTypeVillageoise() == "Botaniste")
                    {
                        //Cas de la dernière botaniste du jeu
                        if (v.getNbVillageoiseType("Botaniste") == 1)
                        {
                            v.getVillageoise(i).mourir(v,grilleJeu);
                            string mort = File.ReadAllText("assets/sprites/gameover.txt");
                            Console.WriteLine(mort);
                            Console.WriteLine("Votre dernière botaniste vient de mourir, votre village court à sa perte, et vous voyez dans son regard le reflet de la fin de l'humanité. Appuyez sur entrée pour revenir au menu principal...");
                            Console.ReadLine();
                            Start();

                        }
                        else
                            v.getVillageoise(i).mourir(v,grilleJeu);
                    }
                    else
                        v.getVillageoise(i).mourir(v,grilleJeu);


                }
            }
        }
        public void isGameOver(Village v)
        {
            bool allUnder25 = true;
            bool oneIsDepressed = false;
            for (int i = 0; i < v.getTailleVillage(); i++)
            {
                //Est ce que tout le monde est en dessous de 25 niveau moral ?
                if (v.getVillageoise(i).Moral > 25)
                    allUnder25 = false;
                //Y a-t-il une villageoise à 0 niveau moral ?
                if (v.getVillageoise(i).Moral == 0)
                    oneIsDepressed = true;
            }
            if (allUnder25 == true || oneIsDepressed == true)
            {
                string mort = File.ReadAllText("assets/sprites/gameover.txt");
                Console.WriteLine(mort);
                Console.WriteLine("\nLe moral est au plus bas, et vous sentez que l'esprit des occultistes s'empare de vos troupes et pousse chacun a grignotter du fenouil cru, c'est un suicide collectif.");
                Console.WriteLine("\nAppuyez sur entrée pour revenir au menu principal...");
                Console.ReadLine();
                Start();
            }
        }

        //Fonction qui permet de fermer la console et d'arrêter le jeu
        public void ExitGame()
        {
            Environment.Exit(0);
        }

        //Fonction qui gère chaque cas qui peut-être choisi par le joueur pour évoluer dans la partie 
        public void Action(Village v, Stock s, Structure st, Grille grilleJeu)
        {
            //Affichage écran qui informe des actions restantes 
            Console.Clear();
            if(nbActionsJournee>1)
                Console.WriteLine("Il vous reste " + NbActionsJournee + " actions à faire aujourd'hui !");
            else
                Console.WriteLine("Il vous reste " + NbActionsJournee + " action à faire aujourd'hui !");
            Console.WriteLine("\n\n\nAppuyez sur entrée pour continuer...");
            Console.Read();
            Console.Clear();
            //Vérifier les morts à chaque tour 
            this.verifierMort(v);
            //Probabilité de l'attaque de l'occultiste à chaque tour 
            MenaceOccultiste(v);
            //Vérifier si les actions sont à 0 ou non
            FinDeJournee(v, s, st, grilleJeu);
            //Affichage du menu de choix d'action à réaliser 
            string[] optionAction = { "Laisser le village travailler", "Soigner les troupes", "Entrainer les combattantes", "Faire appel aux sorcières", "Améliorer ou constuire un bâtiment", "Voir l'état du village","Voir les règles", "Quitter" };
            Menu choixAction = new Menu("Qu'est-ce que voulez vous faire ?", optionAction);
            int indexAction = choixAction.Run();
            //Initialisation des recttes nécessaires à la partie 
            List<List<Ressource>> recettePotions = s.initialiserRecettes();
            List<List<Ressource>> recettesBat = st.initialiserRecettes();



            switch (indexAction)
            {
                //Laisser le village travailler 
                case 0:

                    Console.Clear();
                    //Ramassage des bucheronnes 
                    for (int i = 0; i < v.getTailleVillage(); i++)
                    {
                        if (v.getVillageoise(i).getTypeVillageoise() == "Bucheronne")
                        {

                            int n = RandomNumber(0, 6);
                            ((Bucheronne)v.getVillageoise(i)).recolter(grilleJeu);
                            ((Bucheronne)v.getVillageoise(i)).seBlesser(n);

                        }
                        //Ramassage des mineuses 
                        else if (v.getVillageoise(i).getTypeVillageoise() == "Mineuse")
                        {
                            int probaUsine = RandomNumber(0, 2);
                            //Une chance sur deux de ramasser du minerai
                            if (probaUsine == 0)
                            {
                                int n = RandomNumber(0, 6);
                                ((Mineuse)v.getVillageoise(i)).recolter(grilleJeu);
                                ((Mineuse)v.getVillageoise(i)).seBlesser(n);

                            }
                            //Une chance sur deux de transformer le minerai à l'usine 
                            else
                            {
                                int n = RandomNumber(0, 6);
                                st.transformerUsine(s, (Mineuse)v.getVillageoise(i), grilleJeu);
                                ((Mineuse)v.getVillageoise(i)).seBlesser(n);
                            }
                        }
                        //Ramassage des jardinières 
                        else if (v.getVillageoise(i).getTypeVillageoise() == "Jardiniere")
                        {
                            int n = RandomNumber(0, 8);

                            ((Jardiniere)v.getVillageoise(i)).recolter(grilleJeu);
                            ((Jardiniere)v.getVillageoise(i)).seBlesser(n);
                        }
                    }

                    Console.ReadLine();
                    //Affichage du stock
                    Console.WriteLine("\n" + s);
                    //Gestion de la position des symboles des ouvrières sur la grille 
                    grilleJeu.AfficherGrille();
                    grilleJeu.rentrerVillageoise("ξ");
                    grilleJeu.rentrerVillageoise("Φ");
                    grilleJeu.rentrerVillageoise("θ");

                    Console.ReadLine();
                    //Coût de l'action
                    SetActionJournee(1);
                    //Retour au menu
                    Action(v, s, st, grilleJeu);
                    break;

                //Soigner les troupes 
                case 1:
                    int index = 0;
                    List<Guerisseuse> listeGuerisseuses = new List<Guerisseuse>();
                    foreach (Villageoise personne in v._Village)  //Liste de guerisseuses
                        if (personne.getTypeVillageoise() == "Guerisseuse")
                            listeGuerisseuses.Add((Guerisseuse)personne);
                    List<Villageoise> villageoisesBlessees = new List<Villageoise>();
                    foreach (Villageoise personne in v._Village) //Liste de personnes blessées
                        if (personne.Pdv < 100)
                            villageoisesBlessees.Add(personne);
                    villageoisesBlessees = quicksort(villageoisesBlessees, 0, villageoisesBlessees.Count()-1); //trie la liste des blessées de manière croissante sur leur Pdv restants.
                    bool aSoigneAvecGuerisseuse = false;
                    bool aSoigneAvecPotions = false;
                    for (int j = 0; j < s.getStockLength(); j++)
                    {
                        if (s.getRessource(j).getNom() == "potionSoin")
                            index = j;
                    }
                    for (int i = 0; i < villageoisesBlessees.Count(); i++)
                    {
                        if (s.existeStock("PotionSoin"))  //Si on a des potions de soin, les utilise d'abord pour soigner les troupes avant d'utiliser les guerisseuses et indique au joueur combien de potions il a utilisé
                        {
                            int nbPotionDepart = s._Stock[index].Quantite; ;
                            while (villageoisesBlessees[i].Pdv < 100 && s._Stock[index].Quantite > 0)
                                s.SoinPotion(villageoisesBlessees[i]);
                            if (nbPotionDepart != 0 && nbPotionDepart != s.getQuantiteRessourceType("potionSoin"))
                            {
                                Console.WriteLine($"Vous avez utilisé {nbPotionDepart - s.getQuantiteRessourceType("potionSoin")} potion(s) pour soigner {villageoisesBlessees[i].getNom()} !");
                                aSoigneAvecPotions = true;
                            }
                        }
                        else //Si le joueur n'a pas ou plus de potions de soin, les guerisseuses se mettent a soigner les blessés
                        {
                            int k = 0;
                            int proba2;
                            while (villageoisesBlessees[i].Pdv < 100 && k < listeGuerisseuses.Count())
                            {
                                if (listeGuerisseuses[k].Pdv - villageoisesBlessees[i].getBlessure() > 0)
                                {
                                    listeGuerisseuses[k].guerir(villageoisesBlessees[i]);
                                    proba2 = RandomNumber(0, 10);
                                    listeGuerisseuses[k].seBlesser(proba2);
                                    aSoigneAvecGuerisseuse = true;
                                }
                                k += 1;
                            }
                        }


                    }
                    if (aSoigneAvecGuerisseuse)
                        Console.WriteLine("Vous avez utilisé vos guerisseuses pour soigner les troupes, faites attention à leur moral !"); //les guerisseuses absorbent les blessures, ce qui affecte directement leur moral.
                    verifierMort(v);
                    if (aSoigneAvecPotions || aSoigneAvecGuerisseuse)
                    {
                        Console.ReadLine();
                        Console.WriteLine("\nAppuyez sur entree pour continuer...");
                        Console.ReadLine();
                        SetActionJournee(1);
                        Action(v, s, st, grilleJeu);
                    }
                    else
                    {
                        //Tout le monde a ses points de vie au max, ou alors les guerisseuses restantes ne peuvent pas se permettre de soigner les blessures car sinon elles leur seront fatales.
                        Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("Nul besoin de soigner vos troupes ! Ou du moins c'est peut etre ce que vous pensez...Surveillez vos guerisseuses");
                        Console.ReadLine();
                        Action(v, s, st, grilleJeu);
                    }

                    break;

                //Entraîner les combattantes 
                case 2:
                    //Créer une liste avec toutes les combattantes du village 
                    List<Combattante> listeCombattante = new List<Combattante>();
                    foreach (Villageoise personne in v._Village)
                        if (personne.getTypeVillageoise() == "Combattante")
                            listeCombattante.Add((Combattante)personne);
                    Console.Clear();
                    //Choix du type d'entraînement 
                    string[] optionExp = { "2 combatantes de niveau 1 ? (2 actions)", "2 combattantes de niveau 2 ? (5 actions)" };
                    Menu choixExp = new Menu("Qui voulez vous entrainer (niveau max d'une combattante : 3", optionExp);
                    int indexExp = choixExp.Run();
                    switch (indexExp)
                    {
                        //Deux villageoises de niveau 1
                        case 0:
                            int cpt = 0; int i = 0;
                            int k = -1;
                            while (cpt < 2 && i < listeCombattante.Count())
                            {

                                
                                if (listeCombattante[i].getExperienceCombat() == 1 && k<0)
                                {
                                    k = i;
                                    cpt += 1;
                                }
                                //Deux combattantes s'entraînent ensemble 
                        
                                else if (listeCombattante[i].getExperienceCombat() == 1 && k>0 && k!=i)
                                {
                                    cpt += 1;
                                    listeCombattante[i].sentrainerAvec(listeCombattante[k]);
                                    Random alea = new Random();
                                    int proba1 = alea.Next(4);
                                    listeCombattante[i].seBlesser(proba1);
                                    listeCombattante[k].seBlesser(proba1);


                                }
                                i += 1;

                            }
                            if (cpt < 2)
                            {
                                Console.ReadLine();
                                Console.WriteLine("Désolé, vous n'avez pas assez de combattantes de niveau 1 pour effectuer cette action, créez en une nouvelle grâce aux potions concoctées par les sorcières.");
                                Console.ReadLine();
                                Action(v, s, st, grilleJeu);
                            }

                            else
                            {
                                SetActionJournee(2);
                                Action(v, s, st, grilleJeu);
                            }
                            break;

                        //Deux villageoises de niveau 2
                        case 1:
                            int cptbis = 0; int j = 0;
                            k = -1;
                            while (cptbis < 2 && j < listeCombattante.Count())
                            {

                                
                                if (listeCombattante[j].getExperienceCombat() == 2 && k < 0)
                                {
                                    k = j;
                                    cptbis += 1;
                                }
                                //Deux combattantes s'entraînent ensemble 
                                
                                else if (listeCombattante[j].getExperienceCombat() == 2 && k!=j && k>0)
                                {
                                    cptbis += 1;
                                    listeCombattante[j].sentrainerAvec(listeCombattante[k]);
                                    Random alea = new Random();
                                    int proba3 = alea.Next(4);
                                    listeCombattante[j].seBlesser(proba3);
                                    listeCombattante[k].seBlesser(proba3);
                                }
                                j += 1;

                            }
                            //S'il y a moins de deux combattantes dans le village 
                            if (cptbis < 2)
                            {
                                Console.ReadLine();
                                Console.WriteLine("Désolé, vous n'avez pas assez de combattantes de niveau 2 pour effectuer cette action, créez en une nouvelle grâce aux potions concoctées par les sorcières pour l'entrainer jusqu'au niveau 2.");
                                Console.ReadLine();
                                Action(v, s, st, grilleJeu);
                            }

                            else
                            {
                                //Coût de l'action
                                SetActionJournee(5);
                                //Retour au menu 
                                Action(v, s, st, grilleJeu);
                            }
                            break;
                    }
                    break;

                //Faire appel aux soricères 
                case 3:

                    //Choix de l'action relative aux sorcières 
                    string[] optionSorciere = { "Créer une villageoise", "Régénérer une forêt", "Purifier une occultiste", "Concocter des potions" };
                    Menu choixSorciere = new Menu("Comment voulez vous utiliser la magie ?", optionSorciere);
                    int indexSorciere = choixSorciere.Run();
                    bool possedeRessource;
                    bool possedeBotaniste;
                    switch (indexSorciere)
                    {
                        //Créer une villageoise 
                        case 0:
                            possedeRessource = false;
                            possedeBotaniste = false;
                            //Est-ce que le village possède une botaniste ?
                            foreach (Villageoise personne in v._Village)
                                if (personne.getTypeVillageoise() == "Botaniste")
                                    possedeBotaniste = true;

                            //Est-ce que le stock contient une potion de création ?
                            for (int i = 0; i < s.getStockLength(); i++)
                            {
                                if (s.getRessource(i).getNom() == "Potion création" && s.getRessource(i).Quantite > 0)
                                {
                                    possedeRessource = true;
                                    s.removeRessource(s.getRessource(i));
                                }

                            }
                            if (possedeRessource)
                            {
                                //Choix du type de villageoise à créer 
                                string[] optionCreeVillageoise = { "Ouvrière [Bucheronne, Jardinière, Mineuse]", "Coordinatrice [Combattante, Cuisinière]", "Sorcière [Botaniste, Guérisseuse, Occultiste]" };
                                Menu choixCreeVillageoise = new Menu("Quel type de villageoise voulez vous créer ?", optionCreeVillageoise);
                                int indexCreeVillageoise = choixCreeVillageoise.Run();
                                switch (indexCreeVillageoise)
                                {
                                    //Ouvrière 
                                    case 0:
                                        Console.ReadLine();
                                        double probaOuv = alea.NextDouble();
                                        Console.ReadLine();
                                        //Détermine aléatoirement quelle ouvrière va naître
                                        if (probaOuv <= 1 / 3)
                                        {
                                            Console.WriteLine("Bravo une bucheronne vient de naître, comment voulez vous l'appeler ?");
                                            string nom = Console.ReadLine();
                                            Bucheronne bu = new Bucheronne(nom, s);
                                            Console.ReadLine();
                                            v.addVillageoise(bu);
                                        }
                                        else if (probaOuv > 1 / 3 && probaOuv <= 2 / 3)
                                        {
                                            Console.WriteLine("Bravo une Jardinière vient de naître, comment voulez vous l'appeler ?");
                                            string nom = Console.ReadLine();
                                            Jardiniere bu = new Jardiniere(nom, s);
                                            Console.ReadLine();
                                            v.addVillageoise(bu);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Bravo une Mineuse vient de naître, comment voulez vous l'appeler ?");
                                            string nom = Console.ReadLine();
                                            Mineuse bu = new Mineuse(nom, s);
                                            Console.ReadLine();
                                            v.addVillageoise(bu);
                                        }
                                        
                                        break;

                                    //Coordinatrice 
                                    case 1:
                                        int probaCoord = RandomNumber(1, 101);
                                        Console.ReadLine();
                                        //Détermine aléatoirement quelle coordinatrice va naître
                                        if (probaCoord <= 50)
                                        {
                                            Console.WriteLine("Bravo une Combattante vient de naître, comment voulez vous l'appeler ?");
                                            string nom = Console.ReadLine();
                                            Combattante bu = new Combattante(nom, s);
                                            Console.ReadLine();
                                            v.addVillageoise(bu);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Bravo une Cuisinière vient de naître, comment voulez vous l'appeler ?");
                                            string nom = Console.ReadLine();
                                            Cuisiniere bu = new Cuisiniere(nom, s);
                                            Console.ReadLine();
                                            v.addVillageoise(bu);
                                        }
                                        
                                        break;

                                    //Sorcière
                                    case 2:
                                        double probaSor = alea.NextDouble();
                                        Console.ReadLine();
                                        //Détermine aléatoirement quelle sorcière va naître
                                        if (probaSor <= 1 / 3)
                                        {
                                            Console.WriteLine("Bravo une Botaniste vient de naître, comment voulez vous l'appeler ?");
                                            string nom = Console.ReadLine();
                                            Botaniste bu = new Botaniste(nom, s);
                                            Console.ReadLine();
                                            v.addVillageoise(bu);
                                        }
                                        else if (probaSor > 1 / 3 && probaSor <= 2 / 3)
                                        {
                                            Console.WriteLine("Bravo une Guérisseuse vient de naître, comment voulez vous l'appeler ?");
                                            string nom = Console.ReadLine();
                                            Guerisseuse bu = new Guerisseuse(nom, s);
                                            Console.ReadLine();
                                            v.addVillageoise(bu);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Une occultiste vient de naître, et elle est déjà détestable, trouvez lui un nom, vite!");
                                            string nom = Console.ReadLine();
                                            Occultiste bu = new Occultiste(nom, s);
                                            Console.ReadLine();
                                            v.addVillageoise(bu);
                                        }
                                        
                                        break;
                                }
                                grilleJeu.dessineVillage(v);
                                SetActionJournee(1);
                                Action(v, s, st, grilleJeu);
                            }
                            //Cas où il n'y a pas de potion dans le stock
                            else if (possedeRessource == false && possedeBotaniste)
                            {
                                Console.WriteLine("Vous n'avez plus de potions de création, il faut en concocter des nouvelles grâce à la botaniste.");
                                Console.ReadLine();
                            }

                            Console.ReadLine();
                            //Retour au menu
                            Action(v, s, st, grilleJeu);
                            break;

                        //Régénérer une forêt 
                        case 1:
                            possedeRessource = false;
                            possedeBotaniste = false;
                            foreach (Villageoise personne in v._Village)
                                if (personne.getTypeVillageoise() == "Botaniste")
                                    possedeBotaniste = true;

                            //Est-ce que le stock contient une potion d'engrais ?
                            for (int i = 0; i < s.getStockLength(); i++)
                            {
                                if (s.getRessource(i).getNom() == "Potion engrais" && s.getRessource(i).Quantite > 0)
                                {
                                    possedeRessource = true;
                                    s.removeRessource(s.getRessource(i));
                                }

                            }
                            if (possedeRessource)
                            {
                                Console.Clear();
                                Console.WriteLine("Vous avez concocté une potion d'engrais et vous apprêtez à l'utiliser. Une nouvelle forêt va pousser instantanément dans le village !");
                                grilleJeu.CreerForet();
                                grilleJeu.AfficherGrille();
                                SetActionJournee(1);

                            }

                            else if (possedeRessource == false && possedeBotaniste)
                            {
                                Console.WriteLine("Vous n'avez plus de potions d'engrais, il faut en concocter des nouvelles grâce à la botaniste.");
                                Console.ReadLine();
                            }
                            Console.ReadLine();
                            Action(v, s, st, grilleJeu);
                            break;
                        
                        //Purifier une occultiste
                        case 2:
                            bool possedeRessourceBis = false;
                            for (int i = 0; i < s.getStockLength(); i++)
                            {
                                if (s.getRessource(i).getNom() == "Potion purification" && s.getRessource(i).Quantite > 0) //vérifie qu'on possède bien une potion de purification
                                {
                                    possedeRessourceBis = true;
                                    s.removeRessource(s.getRessource(i));
                                }

                            }
                            if(possedeRessourceBis)
                            {
                                Console.Clear();
                                string purif=File.ReadAllText("assets/sprites/purifie.txt");
                                Console.ReadLine();
                                int nbPurifiees = 0;
                                for (int i = 0; i < v.getTailleVillage(); i++)
                                {
                                    if (v.getVillageoise(i).getTypeVillageoise() == "Occultiste" && nbPurifiees == 0)
                                    {
                                        Console.WriteLine(purif);
                                        v.purifier(((Occultiste)v.getVillageoise(i)), s);
                                        nbPurifiees += 1;
                                        SetActionJournee(1);
                                        grilleJeu.dessineVillage(v);
                                    }
                                }
                                if (nbPurifiees == 0)
                                {
                                    Console.WriteLine("Il n'y a pas d'occultiste à purifier, vous voyez un peu le mal partout...");

                                }
                            }
                            else // cas où il n'y pas de potion de purification
                            {
                                Console.WriteLine("Vous n'avez pas de potion de purification, vous essayez de nous la faire à l'envers ?");
                                Console.ReadLine();
                            }
                            
                            Console.ReadLine();
                            Action(v, s, st, grilleJeu);
                            break;
                
                        //Concocter une potion
                        case 3:

                            //Créer une liste qui contient toutes les botanistes du village 
                            List<Botaniste> listeBotaniste = new List<Botaniste>();
                            foreach (Villageoise personne in v._Village)
                                if (personne.getTypeVillageoise() == "Botaniste")
                                    listeBotaniste.Add((Botaniste)personne);
                            int proba = RandomNumber(1, 6);
                            //Index qui choisira une botaniste au hasard dans la liste pour concocter la potion 
                            int indexBotaniste = RandomNumber(0, listeBotaniste.Count());
                            Console.Clear();
                            ConsoleKey keyPressed;
                            //Affichage de l'écran des recettes de potions
                            string potions = File.ReadAllText("assets/sprites/potions.txt");
                            Console.WriteLine(potions);
                            Console.WriteLine("\n\n\n" + s + "\n");
                            Console.WriteLine("\n Choisissez la potion à créer en pressant la lettre correspondante sur votre clavier. Echap pour quitter...");
                            do
                            {
                                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                                keyPressed = keyInfo.Key;
                                //Concoction potion de création
                                if (keyPressed == ConsoleKey.C)
                                {
                                    //S'il y a bien un laboratoire et que le joueur possède les ressources nécessaires de la recette
                                    if (s.isDispoStock(recettePotions[2]) && st.getNbBatType("Laboratoire") > 0)
                                    {
                                        for (int i = 0; i < recettePotions[2].Count; i++)
                                            s.removeRessource(s.getRessource(s.indexRessource(recettePotions[2][i].getNom())));
                                        s.addRessource(s.getRessource(s.indexRessource("Potion création")));
                                        grilleJeu.BotanisteLabo();
                                        string chaudron = File.ReadAllText("assets/sprites/cauldron.txt");
                                        Console.WriteLine(chaudron);
                                        Console.WriteLine("Félicitations, vous avez créé une potion de création !");
                                        grilleJeu.AfficherGrille();
                                        Console.ReadLine();
                                        grilleJeu.rentrerVillageoise("ζ");
                                        listeBotaniste[indexBotaniste].seBlesser(proba);
                                        SetActionJournee(1);
                                    }
                                    //S'il y a bien un laboratoire mais que le joueur ne possède pas les ressources nécessaires de la recette
                                    else if (s.isDispoStock(recettePotions[2]) == false && st.getNbBatType("Laboratoire") > 0)
                                    {
                                        Console.ReadLine();
                                        Console.WriteLine("\nVous n'avez pas tous les ingrédients nécessaires, hop au travail");
                                        Console.ReadLine();

                                    }
                                    //S'il n'y a pas de laboratoire mais que le joueur possède les ressources nécessaires de la recette
                                    else if (s.isDispoStock(recettePotions[2]) && st.getNbBatType("Laboratoire") <= 0)
                                    {
                                        Console.ReadLine();
                                        Console.WriteLine("\nVous n'avez pas de laboratoire, impossible de concocter !");
                                        Console.ReadLine();
                                        
                                    }
                                    //S'il n'y a ni ressources ni laboratoire 
                                    else if (s.isDispoStock(recettePotions[2]) == false && st.getNbBatType("Laboratoire") <= 0)
                                    {
                                        Console.ReadLine();
                                        Console.WriteLine("\nNi labo, ni ingrédients, vous comptez aller où si vous continuez comme ça ?!");
                                        Console.ReadLine();

                                    }

                                }
                                else if (keyPressed == ConsoleKey.S)
                                {
                                    //S'il y a bien un laboratoire et que le joueur possède les ressources nécessaires de la recette
                                    if (s.isDispoStock(recettePotions[0]) && st.getNbBatType("Laboratoire") > 0)
                                    {
                                        for (int i = 0; i < recettePotions[0].Count; i++)
                                            s.removeRessource(s.getRessource(s.indexRessource(recettePotions[0][i].getNom())));
                                        s.addRessource(s.getRessource(s.indexRessource("Potion soin")));
                                        grilleJeu.BotanisteLabo();
                                        string chaudron = File.ReadAllText("assets/sprites/cauldron.txt");
                                        Console.WriteLine(chaudron);
                                        Console.WriteLine("Félicitations, vous avez créé une potion de soin !");
                                        grilleJeu.AfficherGrille();
                                        Console.ReadLine();
                                        grilleJeu.rentrerVillageoise("ζ");
                                        listeBotaniste[indexBotaniste].seBlesser(proba);
                                        SetActionJournee(1);
                                    }
                                    //S'il y a bien un laboratoire mais que le joueur ne possède pas les ressources nécessaires de la recette
                                    else if (s.isDispoStock(recettePotions[0]) == false && st.getNbBatType("Laboratoire") > 0)
                                    {
                                        Console.ReadLine();
                                        Console.WriteLine("\nVous n'avez pas tous les ingrédients nécessaires, hop au travail");
                                        Console.ReadLine();

                                    }
                                    //S'il n'y a pas de laboratoire mais que le joueur possède les ressources nécessaires de la recette
                                    else if (s.isDispoStock(recettePotions[0]) && st.getNbBatType("Laboratoire") <= 0)
                                    {
                                        Console.ReadLine();
                                        Console.WriteLine("\nVous n'avez pas de laboratoire, impossible de concocter !");
                                        Console.ReadLine();

                                    }
                                    //S'il n'y a ni ressources ni laboratoire 
                                    else if (s.isDispoStock(recettePotions[0]) == false && st.getNbBatType("Laboratoire") <= 0)
                                    {
                                        Console.ReadLine();
                                        Console.WriteLine("\nNi labo, ni ingrédients, vous comptez aller où si vous continuez comme ça ?!");
                                        Console.ReadLine();

                                    }

                                }
                                else if (keyPressed == ConsoleKey.P)
                                {
                                    //S'il y a bien un laboratoire et que le joueur possède les ressources nécessaires de la recette
                                    if (s.isDispoStock(recettePotions[1]) && st.getNbBatType("Laboratoire") > 0)
                                    {
                                        for (int i = 0; i < recettePotions[1].Count; i++)
                                            s.removeRessource(s.getRessource(s.indexRessource(recettePotions[1][i].getNom())));
                                        s.addRessource(s.getRessource(s.indexRessource("Potion purification")));
                                        grilleJeu.BotanisteLabo();
                                        string chaudron = File.ReadAllText("assets/sprites/cauldron.txt");
                                        Console.WriteLine(chaudron);
                                        Console.WriteLine("Félicitations, vous avez créé une potion de purification !");
                                        grilleJeu.AfficherGrille();
                                        Console.ReadLine();
                                        grilleJeu.rentrerVillageoise("ζ");
                                        listeBotaniste[indexBotaniste].seBlesser(proba);
                                        SetActionJournee(1);
                                    }
                                    //S'il y a bien un laboratoire mais que le joueur ne possède pas les ressources nécessaires de la recette
                                    else if (s.isDispoStock(recettePotions[1]) == false && st.getNbBatType("Laboratoire") > 0)
                                    {
                                        Console.ReadLine();
                                        Console.WriteLine("\nVous n'avez pas tous les ingrédients nécessaires, hop au travail");
                                        Console.ReadLine();

                                    }
                                    //S'il n'y a pas de laboratoire mais que le joueur possède les ressources nécessaires de la recette
                                    else if (s.isDispoStock(recettePotions[1]) && st.getNbBatType("Laboratoire") <= 0)
                                    {
                                        Console.ReadLine();
                                        Console.WriteLine("\nVous n'avez pas de laboratoire, impossible de concocter !");
                                        Console.ReadLine();

                                    }
                                    //S'il n'y a ni ressources ni laboratoire 
                                    else if (s.isDispoStock(recettePotions[1]) == false && st.getNbBatType("Laboratoire") <= 0)
                                    {
                                        Console.ReadLine();
                                        Console.WriteLine("\nNi labo, ni ingrédients, vous comptez aller où si vous continuez comme ça ?!");
                                        Console.ReadLine();

                                    }
                                }
                                else if (keyPressed == ConsoleKey.E)
                                {
                                    //S'il y a bien un laboratoire et que le joueur possède les ressources nécessaires de la recette
                                    if (s.isDispoStock(recettePotions[3]) && st.getNbBatType("Laboratoire") > 0)
                                    {
                                        for (int i = 0; i < recettePotions[2].Count; i++)
                                            s.removeRessource(s.getRessource(s.indexRessource(recettePotions[3][i].getNom())));
                                        s.addRessource(s.getRessource(s.indexRessource("Potion engrais")));
                                        grilleJeu.BotanisteLabo();
                                        string chaudron = File.ReadAllText("assets/sprites/cauldron.txt");
                                        Console.WriteLine(chaudron);
                                        Console.WriteLine("Félicitations, vous avez créé une potion d'engrais !");
                                        grilleJeu.AfficherGrille();
                                        Console.ReadLine();
                                        grilleJeu.rentrerVillageoise("ζ");
                                        listeBotaniste[indexBotaniste].seBlesser(proba);
                                        SetActionJournee(1);
                                    }
                                    //S'il y a bien un laboratoire mais que le joueur ne possède pas les ressources nécessaires de la recette
                                    else if (s.isDispoStock(recettePotions[3]) == false && st.getNbBatType("Laboratoire") > 0)
                                    {
                                        Console.ReadLine();
                                        Console.WriteLine("\nVous n'avez pas tous les ingrédients nécessaires, hop au travail");
                                        Console.ReadLine();

                                    }
                                    //S'il n'y a pas de laboratoire mais que le joueur possède les ressources nécessaires de la recette
                                    else if (s.isDispoStock(recettePotions[3]) && st.getNbBatType("Laboratoire") <= 0)
                                    {
                                        Console.ReadLine();
                                        Console.WriteLine("\nVous n'avez pas de laboratoire, impossible de concocter !");
                                        Console.ReadLine();

                                    }
                                    //S'il n'y a ni ressources ni laboratoire 
                                    else if (s.isDispoStock(recettePotions[3]) == false && st.getNbBatType("Laboratoire") <= 0)
                                    {
                                        Console.ReadLine();
                                        Console.WriteLine("\nNi labo, ni ingrédients, vous comptez aller où si vous continuez comme ça ?!");
                                        Console.ReadLine();

                                    }
                                }
                                verifierMort(v);
                            }
                            while (keyPressed != ConsoleKey.E && keyPressed != ConsoleKey.P && keyPressed != ConsoleKey.S && keyPressed != ConsoleKey.C && keyPressed!=ConsoleKey.Escape);
                            Action(v, s, st, grilleJeu);
                            break;
                    }


                    break;

                //Construire ou améliorer un bâtiment 
                case 4:

                    Console.Clear();
                    Console.WriteLine(st);
                    ConsoleKey keyPressedBis;
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Voulez vous construire un bâtiment (C) ou améliorer un bâtiment déjà existant (A) ?  Echap pour quitter...");
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                        keyPressedBis = keyInfo.Key;
                        //Cas où le joueur veut construire 
                        if (keyPressedBis == ConsoleKey.C)
                        {
                            int nbLabActuel = st.getNbBatType("Laboratoire");
                            int nbUsineActuel = st.getNbBatType("Usine");
                            //Appel à la méthode de construction
                            st.choixConstruction(s, recettesBat, grilleJeu);
                            //Vérifier que le bâtiment est bien construit pour compter cela comme une action
                            if (st.getNbBatType("Laboratoire") > nbLabActuel || st.getNbBatType("Usine") > nbUsineActuel)
                                SetActionJournee(1);
                        }
                        //Cas où le joueur veut améliorer 
                        else if (keyPressedBis == ConsoleKey.A)
                        {
                            if (st.getStructureLength() > 0)
                            {
                                //Affichage de l'écran d'amélioration des bâtiments
                                string ecranAmelioration = File.ReadAllText("assets/sprites/ameliorebat.txt");
                                Console.WriteLine(ecranAmelioration);
                                Console.WriteLine("\n\n\n" + s + "\n\n");
                                Console.WriteLine( st + "\n\n");
                                Console.ReadLine();
                                Console.Write("\n Entrez le nom du bâtiment à améliorer : ");
                                string batAmeliore = Console.ReadLine();
                                int isInStructure = 0;
                                bool aAmeliore = false;
                                
                                for (int i = 0; i < st.getStructureLength(); i++)
                                {
                                    //Amélioration d'une usine 
                                    if (st.getBatiment(i).getNom() == batAmeliore && st.getBatiment(i).getTypeBat() == "Usine")
                                    {
                                        int nivActuel = st.getBatiment(i).Niveau;
                                        st.getBatiment(i).Ameliorer(s, recettesBat[2]);
                                        if (st.getBatiment(i).Niveau > nivActuel)
                                            aAmeliore = true;
                                        isInStructure += 1;
                                       

                                    }
                                    //Amélioration d'un laboratoire 
                                    else if (st.getBatiment(i).getNom() == batAmeliore && st.getBatiment(i).getTypeBat() == "Laboratoire")
                                    {
                                        int nivActuel = st.getBatiment(i).Niveau;
                                        st.getBatiment(i).Ameliorer(s, recettesBat[3]);
                                        if (st.getBatiment(i).Niveau > nivActuel)
                                            aAmeliore = true;
                                        isInStructure += 1;
                                        

                                    }
                                    //Cas où le joueur rentre un nom qui n'est pas valide 
                                    if (isInStructure == 0)
                                    {
                                        Console.WriteLine("Aucun bâtiment ne porte ce nom, veillez à l'orthographier correctement. Appuyez sur entrée pour continuer...");
                                        Console.ReadLine();
                                    }
                                }
                                if (aAmeliore)
                                    SetActionJournee(1);
                            }
                            //Cas où il n'y a aucun bâtiment dans le village 
                            else
                            {
                                Console.Clear();
                                Console.ReadLine();
                                Console.WriteLine("Vous n'avez aucun bâtiment pour le moment, construisez avant d'améliorer ! Appuyez sur entrée pour continuer...");
                                Console.ReadLine();
                            }
                        }
                    }
                    while (keyPressedBis != ConsoleKey.A && keyPressedBis != ConsoleKey.C && keyPressedBis!=ConsoleKey.Escape);



                    Action(v, s, st, grilleJeu);
                    break;

                //Voir l'état du village
                case 5:
                    Console.Clear();
                    string separation = "===========================================";
                    //Affichage du village, de la structure et du stock 
                    Console.WriteLine(v + "\n" + separation + "\n\n" + st + "\n\n" + s);
                    Console.ReadLine();
                    Console.WriteLine("\nPressez entrée pour revenir aux choix des actions.");
                    Console.ReadLine();
                    //Retour au menu
                    Action(v, s, st, grilleJeu);
                    break;
                    
                //Voir les règles
                case 6:
                    Console.ReadLine();
                    Console.Clear();
                    string regles = File.ReadAllText("assets/sprites/reglesJeu.txt");
                    Console.WriteLine(regles);
                    Console.ReadLine();
                    Action(v, s, st, grilleJeu);
                    break;

                //Quitter 
                case 7:
                    grilleJeu.EffacerGrille();
                    Start();
                    break;
            }
        }

        //Fonction qui appelle tous les évènements de fin de journée 
        public void FinDeJournee(Village v, Stock s, Structure st, Grille grilleJeu)
        {
            //Se lance si toutes les actions de la journée sont épuisées
            if (nbActionsJournee == 0)
            {
                //Le compteur d'action est remis à 5 
                nbActionsJournee = 5;
                this.Banquet(v, s);
                this.attaqueMechenchon(v, s, st, grilleJeu);
                this.Nuit(v);
            }

        }

        //Fonction qui appelle la méthode de maléfice de l'occultiste si elle en trouve une dans le village 
        public void MenaceOccultiste(Village v)
        {
            for (int i = 0; i < v.getTailleVillage(); i++)
            {
                if (v.getVillageoise(i).getTypeVillageoise() == "Occultiste")
                {
                    ((Occultiste)v.getVillageoise(i)).lancerMalefice(v,grilleJeu);
                }
            }
        }

        //Fonction qui gère le repas préparé par les cuisinières que prennent les villageoises en fin de journée
        public void Banquet(Village v, Stock s)
        {
            Console.Clear();
            Console.ReadLine();
            int cptType = 0;
            bool possedeCuistot = false;
            //Vérifie qu'il y a bien au moins une cuisinière dans le village 
            for (int i = 0; i < v.getTailleVillage(); i++)
            {
                if (v.getVillageoise(i).getTypeVillageoise() == "Cuisiniere")
                    possedeCuistot = true;
            }
            //Si il y a bien une cuisinière 
            if (possedeCuistot)
            {
                for (int j = 0; j < s.getStockLength(); j++)
                {
                    //Si la ressource est alimentaire 
                    if (s.getRessource(j).getNom() == "Blé" || s.getRessource(j).getNom() == "Courge" || s.getRessource(j).getNom() == "Poire")
                    {
                        //Et qu'il n'y en a pas deux dans le stock
                        if (s.getRessource(j).Quantite < 2)
                        {
                            Console.WriteLine("Pas assez de " + s.getRessource(j).getNom() + " dans le stock, les troupes seront moins rassasiées...");
                            Console.ReadLine();

                        }//Et qu'il y en a au moins  deux dans le stock
                        else
                        {
                            //Incrémentation du compteur de ressources 
                            cptType += 1;
                            s.getRessource(j).Quantite -= 2;
                        }
                    }
                }
                //Plus on a de ressources différentes à donner à manger, plus les villageoises seront d'aplomb après le repas 
                for (int i = 0; i < v.getTailleVillage(); i++)
                {
                    //Gestion de la fatigue de la cuisinière après la préparation du repas 
                    if (v.getVillageoise(i).getTypeVillageoise() == "Cuisiniere")
                    {
                        v.getVillageoise(i).Moral = 35;
                        v.getVillageoise(i).Pdv = 55;
                        int proba = RandomNumber(0, 10);
                        ((Cuisiniere)v.getVillageoise(i)).seBlesser(proba);


                    }
                    else
                    {
                        //3 ressources en quantité suffisante
                        if (cptType == 3)
                        {
                            if (v.getVillageoise(i).Pdv >= 70)
                                v.getVillageoise(i).Pdv = 100;
                            else
                                v.getVillageoise(i).Pdv += 30;
                        }
                        //2 ressources en quantité suffisante
                        else if (cptType == 2)
                        {
                            if (v.getVillageoise(i).Pdv >= 80)
                                v.getVillageoise(i).Pdv = 100;
                            else
                                v.getVillageoise(i).Pdv += 20;
                        }
                        //1 ressources en quantité suffisante
                        else if (cptType == 1)
                        {
                            if (v.getVillageoise(i).Pdv >= 90)
                                v.getVillageoise(i).Pdv = 100;
                            else
                                v.getVillageoise(i).Pdv += 10;
                        }

                    }

                }
                Console.WriteLine("C'est l'heure du souper ! Les cuisinières mettent la main à la pâte.");
                Console.ReadLine();
                Console.WriteLine("Miam miam miam");
                Console.ReadLine();
                Console.WriteLine("Les troupes sont (plus ou moins) rassasiées ! \n\nAppuyez sur entrée pour continuer...");
                Console.ReadLine();
            }
            //S'il n'y a pas de cuisinière
            else
            {
                Console.WriteLine("Comment nourir des troupes sans cusinières ? Vous vous couchez sans manger. \n\nAppuyez sur entrée pour continuer...");
                Console.ReadLine();
            }

        }

        //Fonction qui affiche l'écran de nuit et détermine quelle villageoise montera la garde et se sera pas reposée le lendemain
        public void Nuit(Village v)
        {
            Console.Clear();
            string nightScreen = File.ReadAllText("assets/sprites/messagenuit.txt");
            string dayScreen = File.ReadAllText("assets/sprites/messagejour.txt");
            Random alea = new Random();
            int indiceGarde = alea.Next(v.getTailleVillage());
            //Le sommeil augmente le moral des villageoise 
            for (int i = 0; i < v.getTailleVillage(); i++)
                if (v.getVillageoise(i).Moral < 70)
                    v.getVillageoise(i).Moral = 70;
            //Sauf pour celle qui monte la garde 
            if (v.getVillageoise(indiceGarde).Moral > 55)
                v.getVillageoise(indiceGarde).Moral = 55;
            //Affichage des écrans de transition
            Console.ReadLine();
            Console.WriteLine(nightScreen);
            Console.WriteLine("\n\n" + v.getVillageoise(indiceGarde).getNom() + " (" + v.getVillageoise(indiceGarde).getTypeVillageoise() + ") va monter la garde cette nuit, son moral va en prendre un coup, mais c'est pour le bien du village !");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine(dayScreen);
            Console.ReadLine();

        }

        //Deux méthodes qui servent à trier efficacement une liste 
        public int Partitionner(List<Villageoise> personnes, int premier, int dernier, int pivot) //Pivot entre premier et dernier
        {
            if (personnes.Count() == 1)
                return 0;
            else
            {
                Villageoise etape = personnes[pivot];
                personnes[pivot] = personnes[dernier];
                personnes[dernier] = etape;
                int j = premier;
                for (int i = j; i < dernier - 1; i++)
                {
                    if (personnes[i].Pdv <= personnes[dernier].Pdv)
                    {
                        etape = personnes[i];
                        personnes[i] = personnes[j];
                        personnes[j] = etape;
                    }
                }
                etape = personnes[dernier];
                personnes[dernier] = personnes[j];
                personnes[j] = etape;
                return j;
            }
        }
        public List<Villageoise> quicksort(List<Villageoise> personnes, int premier, int dernier)
        {
            if (premier < dernier)
            {
                int pivot = RandomNumber(premier, dernier);
                pivot = Partitionner(personnes, premier, dernier, pivot);
                quicksort(personnes, premier, pivot - 1);
                quicksort(personnes, pivot + 1, dernier);
            }
            return personnes;
        } //Algorithme de tri rapide 

    }

}


