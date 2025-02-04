using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace colony_game
{
    class Menu
    {
        //Attributs de la classe Menu
        private int Index; //Choix du joueur
        private string[] Possibilities; //Options du menu
        private string Display;   //Affichage de l'en-tête du menu

        //Constructeur de la classe Menu
        public Menu (string dis, string[] poss)
        {
            Possibilities = poss;
            Display = dis;
            Index = 0;
        }
       

        //Methode permettant de mettre en valeur la séléction actuelle du joueur 
        private void DisplayOptions()
        {
            Console.WriteLine(Display);
            for (int i=0;i<Possibilities.Length;i++)
            {
                string currentOption = Possibilities[i];
                string prefix;

                if (i == Index)
                    prefix = "-->";
                else
                    prefix = "";
                Console.WriteLine($"{prefix} ■■ {currentOption}");

            }
        }

        //Methode faisant tourner le menu jusqu'à ce que le joueur choisisse une option en appuyant sur entrée. Renvoie l'index de ce choix.
        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                DisplayOptions();
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;
                if (keyPressed==ConsoleKey.UpArrow)
                {
                    if (Index == 0)
                        Index = Possibilities.Length;
                    Index--;
                }
                else if (keyPressed==ConsoleKey.DownArrow)
                {
                    if (Index == Possibilities.Length-1)
                        Index = -1;
                    Index++;
                }
            } while (keyPressed != ConsoleKey.Enter);
            return Index;
        }

       

        
    }
}
