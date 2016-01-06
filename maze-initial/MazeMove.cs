using numl.AI;

namespace Games 
{
    public class MazeMove : ISuccessor 
    {
        public string Action { get; set; }
        
        public double Cost { get; set; }
        
        public IState State { get; set; }
                
        public Maze GetState() 
        {
            return State as Maze;
        }
    }
}