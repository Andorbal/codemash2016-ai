using numl;
using System;
using numl.Model;
using numl.Supervised;
using numl.Math.LinearAlgebra;
using numl.Supervised.DecisionTree;

namespace Example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Step 1: Data
            Console.WriteLine("Step 1: Data");
            Tennis[] tennis = Tennis.GetData();
            var descriptor = Descriptor.Create<Tennis>();
            
            // Step 2: Maths
            Console.WriteLine("\nStep 2: Maths");
            var data = descriptor.ToExamples(tennis);
            var x = data.Item1;
            var y = data.Item2;
            Console.WriteLine($"Mathematical feature representation:{x}");
            Console.WriteLine($"\nMathematical label representation:{y.T}");
            
            // Step 3: Generate Model
            Console.WriteLine("\nStep 3: Generate Model");
            var generator = new DecisionTreeGenerator();
            var model = generator.Generate<Tennis>(descriptor, tennis);
            Console.WriteLine(model);
            
            // Step 4: Prediction
            Console.WriteLine("\nStep 4: Prediction");
            Tennis t = new Tennis
            {
                Outlook = Outlook.Sunny,
                Temperature = Temperature.Hot,
                Windy = false
            };
            Tennis p = model.Predict(t);
            Console.WriteLine($"Play: {p.Play}");
            
            
            // Step 5: Testing Accuracy
            Console.WriteLine("\nStep 5: Testing Accuracy");
            ((DecisionTreeGenerator)generator).SetHint(0);

            var learned = Learner.Learn(tennis, 0.80, 100, generator);
            var m = learned.Model;
            double accuracy = learned.Accuracy;
            Console.WriteLine($"Learned {m.GetType().Name} model with {accuracy*100}% accuracy");
            Console.WriteLine(m);

            Tennis h = new Tennis
            {
                Outlook = Outlook.Sunny,
                Temperature = Temperature.Hot,
                Windy = false
            };

            Tennis o = m.Predict(h);
            
            Console.WriteLine($"Play: {o.Play}");
        }
    }
}
