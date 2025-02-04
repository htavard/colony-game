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
    class Program
    {
        //Le Main Program ne sert qu'à faire appel à la classe Game et à sa méthode de démarrage 
        static void Main(string[] args)

        {
            Game myGame = new Game();
            myGame.Start();
        }

    }
}