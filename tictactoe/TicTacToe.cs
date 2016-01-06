using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using numl.AI;

namespace Games
{
    public class TicTacToe : IAdversarialState
    {
        public string Id { get; private set; }
        public bool IsTerminal { get; private set; }
        public double Utility { get; private set; }
        public bool Player { get; private set; }

        private readonly int[] _board = new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public TicTacToe(bool player)
        {
            Id = Guid.NewGuid().ToString();
            IsTerminal = false;
            Player = player;
        }

        public TicTacToe(bool player, int[] board)
            : this(player)
        {
            Id = Guid.NewGuid().ToString();
            _board = board;
            Calculate(_board);
        }

        public IEnumerable<ISuccessor> GetSuccessors()
        {
            for (int i = 0; i < _board.Length; i++)
            {
                if (_board[i] == 0)
                {
                    var play = Player ? 1 : -1;
                    var newBoard = (int[])_board.Clone();
                    newBoard[i] = play;
                    yield return new TicTacToeMove(new TicTacToe(!Player, newBoard), i.ToString());
                }
            }
        }

        public IAdversarialState Reset()
        {
            return new TicTacToe(Player);
        }

        // helpers
        private void Calculate(int[] board)
        {
            Utility = 0;
            IsTerminal = false;

            var wins = new[]
            {
                new[] {0, 1, 2},
                new[] {3, 4, 5},
                new[] {6, 7, 8},
                new[] {0, 3, 6},
                new[] {1, 4, 7},
                new[] {2, 5, 8},
                new[] {0, 4, 8},
                new[] {2, 4, 6},
            };

            // check win
            foreach (var w in wins)
            {
                var u = Calculate(board, w);
                if (u != 0)
                {
                    Utility = u;
                    IsTerminal = u != 0;
                    return;
                }
            }

            // check draw
            if (_board.Where(i => i == 0).Count() == 0)
            {
                IsTerminal = true;
                Utility = 0;
            }
        }

        private static int Calculate(int[] board, int[] win)
        {
            if (win.Length != 3) throw new InvalidOperationException("Needs to be three!");
            // if abs sum == 3, there's a win in this configuration
            if (Math.Abs(board[win[0]] + board[win[1]] + board[win[2]]) == 3)
                return board[win[0]] < 0 ? -1 : 1;
            else
                return 0;
        }
        
        public bool CanPlay(int pos)
        {
            return pos > -1 && pos < 9 && _board[pos] == 0;
        }
        
        public IAdversarialState Play(int pos)
        {
            if(_board[pos] != 0)
                return this;
            else 
            {
                var b = (int[])_board.Clone();
                var play = Player ? 1 : -1;
                b[pos] = play;
                return new TicTacToe(!Player, b);
            }
        }

        public bool IsEqualTo(IState state)
        {
            if (state == null) return false;
            if (!(state is TicTacToe)) return false;

            TicTacToe tictactoe = (TicTacToe)state;
            for (int i = 0; i < _board.Length; i++)
                if (_board[i] != tictactoe._board[i])
                    return false;

            return true;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Func<int, string> f = i => 
            {
                if (_board[i] < 0)
                    return "x";
                else if(_board[i] > 0)
                    return "o";
                else
                    return i.ToString();
            };
            
            for (int i = 0; i < _board.Length; i+=3)
            {
                if(i > 0) sb.Append("---+---+---\n");
                sb.Append(string.Format("{0,2} ", f(i)));
                sb.Append(string.Format("|{0,2} ", f(i+1)));
                sb.Append(string.Format("|{0,2} \n", f(i+2)));
                
            }

            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            return IsEqualTo(obj as IState);
        }

        public override int GetHashCode()
        {
            return _board.GetHashCode();
        }

        public double Heuristic()
        {
            return 1;
        }
    }
}
