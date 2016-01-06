using System;
using System.Linq;

namespace Games
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Maze Solver!");
            char[][] mazeData = mazeDefinition
                .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.ToArray())
                .ToArray();
            Maze maze = new Maze(mazeData, Tuple.Create(1, 1), Tuple.Create(13, 4));

            Console.Write(maze);
        }

        private const string mazeDefinition = @"
+++++++++++++++
+ +     + +++ +
+ + + +++     +
+   +   + +++ +
+ +++++     + +
+++++++++++++++";
    }
}
