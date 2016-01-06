using System;
using numl.AI;

namespace Games
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var initial = new Square(new[] { 3, 4, 2, 1, 5, 7, 6, 0, 8 });
			AStarSearch strategy = new AStarSearch();
			strategy.Heuristic = s => s.Heuristic();
			var search = new SimpleSearch(strategy);
            search.Find(initial);
            
            Console.WriteLine("Initial");
            Console.WriteLine(initial);
            
            int moves = 0;
            foreach(var s in search.Solution)
            {
                Console.WriteLine($"{s.Action} ({++moves})");
                Console.WriteLine(s.State);
            }
            
            Console.WriteLine($"Solved in {moves} moves");
        }
    }
}
