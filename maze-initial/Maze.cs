using numl.AI;
using System.Collections.Generic;

namespace Games 
{
    public class Maze : IState 
    {
        public string Id { get; set; }
        
        public bool IsTerminal { get; private set; }
        
        public double Heuristic() 
        {
            return 0;
        }
        public IEnumerable<ISuccessor> GetSuccessors() 
        {
            yield return null;
        }
        public bool IsEqualTo(IState maze) 
        {
            return false;
        }
    }
}