using numl.AI;

namespace Games
{
    public class TicTacToeMove : ISuccessor
    {
        public TicTacToeMove(IState state, string action)
        {
            State = state;
            Action = action;
        }

        public double Cost
        {
            get { return 1; }
        }

        public string Action { get; private set; }

        public IState State { get; private set; }

    }
}
