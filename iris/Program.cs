using System;
using numl.Model;
using numl.Supervised.DecisionTree;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Iris Machine Learning Example");
            
            // our data
            var data = Iris.Load();
            
            // descriptor
            var description = Descriptor.Create<Iris>();
            Console.WriteLine(description);
            
            // generator makes model
            var generator = new DecisionTreeGenerator();
            var model = generator.Generate(description, data);
            Console.WriteLine("Generated model:");
            Console.WriteLine(model);
            
            // use model
            // model.Predict(...)
        }
    }
}
