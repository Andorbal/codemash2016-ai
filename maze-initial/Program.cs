using System;
using System.Linq;
using numl.AI;

namespace Games
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Maze Solver!");

            Maze maze = new Maze(GetMazeData(mazeDefinition), Tuple.Create(1, 1), Tuple.Create(13, 4));
            Maze maze2 = new Maze(GetMazeData(mazeDefinition2), Tuple.Create(1, 1), Tuple.Create(35, 21));
            AStarSearch strategy = new AStarSearch();
            strategy.Heuristic = s => s.Heuristic();
            var search = new SimpleSearch(strategy);
            search.Find(maze2);

            Console.WriteLine("Initial");
            Console.WriteLine(maze2);

            int moves = 0;
            foreach(var s in search.Solution)
            {
                Console.WriteLine($"{s.Action} ({++moves})");
                Console.WriteLine(s.State);
            }

            Console.WriteLine($"Solved in {moves} moves");
        }

        private static char[,] GetMazeData(string def) {
            char[][] mazeData = def
                .Split(new[] { '\n' })
                .Skip(1)
                .Select(x => x.ToArray())
                .ToArray();

            char[,] actualMazeData = new char[mazeData[0].Length, mazeData.Length];
            for (int i = 0; i < mazeData.Length; i++) {
                for (int j = 0; j < mazeData[i].Length; j++) {
                    actualMazeData[j, i] = mazeData[i][j];
                }
            }

            return actualMazeData;
        }

        private const string mazeDefinition = @"
+++++++++++++++
+ +     + +++ +
+ + + +++     +
+   +   + +++ +
+ +++++     + +
+++++++++++++++";

        private const string mazeDefinition2 = @"
+++++++++++++++++++++++++++++++++++++
+ +     +     +     +       +   +   +
+ + +++ + +++ +++++ +++ +++ +++ +   +
+     +   + +     +     + + +   +   +
+++++ + +++++ +++++ +++ + + + +++++ +
+   + +     +     + + + + +     +   +
+ + + +++++ + + +++++ +++ + +++++ + +
+ +     + + +   +     +     +   +   +
+ +++++ +++ +++ + +++ +++++ + +++   +
+     +   + +   + +   +     + +     +
+ +++ +++ + +++ + +++++ + + + +++++++
+   +   + + +   +   +   + + +   +   +
+++++++ + + + +++++ + +++ + +++ +++ +
+     + +     +   + +   + +   +     +
+ +++ + +++++ +++ + +++ +++ +++++++ +
+ +   +     +     +   + +       +   +
+ + +++++ + +++ +++++ + + +++++ +   +
+ +     + + +   +           +   +   +
+ +++++ + + + +++ +++++ +++ + +++++++
+ +   + + +     +     + +   +       +
+ + +++ +++ +++ +++++ +++ + +++++ + +
+ +       +           +     +       +
+++++++++++++++++++++++++++++++++++++";
    }
}
