using numl.AI;
using System.Collections.Generic;
using System;
using System.Text;

namespace Games
{
    public class Maze : IState
    {
        readonly string[] moves = new[] { "Left", "Right", "Up", "Down" };
        readonly Func<Tuple<int, int>>[] moveActions;

        readonly char[,] data;
        readonly Tuple<int, int> position;
        readonly Tuple<int, int> end;

        public Maze(char[,] data, Tuple<int, int> position, Tuple<int, int> end)
        {
            this.data = data;
            this.position = position;
            this.end = end;

            IsTerminal = AreEqual(position, end);

            moveActions = new Func<Tuple<int, int>>[] {
               () => GetHorizontal(-1),
               () => GetHorizontal(1),
               () => GetVertical(-1),
               () => GetVertical(1)
           };
        }

        public string Id { get; set; }

        public bool IsTerminal { get; private set; }

        public double Heuristic() =>
            Math.Abs(position.Item1 - end.Item1) +
                Math.Abs(position.Item2 - end.Item2);

        public IEnumerable<ISuccessor> GetSuccessors()
        {
            for (int i = 0; i < 4; i++)
            {
                var newPosition = moveActions[i]();
                if (CharacterAt(newPosition) == ' ') {
                    yield return new MazeMove(new Maze(data, newPosition, end), moves[i]);
                }
            }
        }

        public bool IsEqualTo(IState maze) =>
            AreEqual(position, ((Maze) maze).position);

        public override string ToString()
        {
            var buffer = new StringBuilder();

            for (int i = 0; i < data.GetLength(1); i++)
            {
                for (int j = 0; j < data.GetLength(0); j++)
                {
                    var location = Tuple.Create(j, i);

                    if (AreEqual(location, position)) {
                        buffer.Append('O');
                    }
                    else if (AreEqual(location, end)) {
                        buffer.Append('X');
                    }
                    else {
                        buffer.Append(CharacterAt(location));
                    }
                }
                buffer.Append(Environment.NewLine);
            }

            return buffer.ToString();
        }

        private Tuple<int, int> GetHorizontal(int change) =>
            Tuple.Create(position.Item1 + change, position.Item2);

        private Tuple<int, int> GetVertical(int change) =>
            Tuple.Create(position.Item1, position.Item2 + change);

        private char CharacterAt(Tuple<int, int> location) =>
            data[location.Item1, location.Item2];

        private bool AreEqual(Tuple<int, int> x, Tuple<int, int> y) =>
            x.Item1 == y.Item1 && x.Item2 == y.Item2;
    }
}
