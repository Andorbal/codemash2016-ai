using numl.AI;

namespace Games
{
    public class SquareMove : ISuccessor
    {
        public SquareMove(IState state, string action)
        {
            State = state;
            Action = action;
        }

        public double Cost { get { return 1; } }
        public string Action { get; private set; }
        public IState State { get; private set; }
    }
}
