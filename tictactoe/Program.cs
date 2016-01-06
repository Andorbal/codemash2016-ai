using System;
using numl.AI;


namespace Games
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var tictactoe = new TicTacToe(false);
            Minimax m = new Minimax();
            m.Depth = 2;
            
            while(!tictactoe.IsTerminal)
            {
                Console.Write('\n');
                Console.WriteLine(tictactoe);
                int pos = -1;
                do
                {
                    ConsoleKeyInfo cki;
                    do 
                    {
                        Console.WriteLine("\nEnter slot: ");
                        cki = Console.ReadKey();
                    }
                    while (!char.IsNumber(cki.KeyChar));
                    
                    pos = int.Parse(cki.KeyChar.ToString());

                } while (!tictactoe.CanPlay(pos));
                
                Console.Write('\n');
                tictactoe = (TicTacToe)tictactoe.Play(pos);
                if(!tictactoe.IsTerminal)
                    tictactoe = (TicTacToe)m.Find(tictactoe).State;
            }
            
            Console.Write('\n');
            Console.WriteLine(tictactoe);
            if(tictactoe.Utility == 0)
                Console.Write($"Game ends in a draw\n");
            else
            {
                var winner = tictactoe.Utility == -1 ? 'x' : 'o';
                Console.Write($"{winner} wins!\n");
            }
        }
    }
}
