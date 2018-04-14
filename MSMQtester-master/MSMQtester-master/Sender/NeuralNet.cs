using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Neuro;
using Accord.Neuro.Learning;


namespace Sender
{
    class NeuralNet
    {
        private ActivationNetwork network;
        private int outputSize;

        public NeuralNet(int inputs, int outputs, int layers)
        {
            outputSize = outputs;
            network = new ActivationNetwork(new SigmoidFunction(), inputs, 5, 5, 5, outputs);
            network.Randomize();


        }

        public double Train(double[][] input, double[][] output, int iterations)
        {
            var teacher = new ResilientBackpropagationLearning(network);

            double[][] returnVal = new double[input.GetLength(0)][];
            double error = 0;
            for (int iteration = 0; iteration < iterations; iteration++)
            {
                error = teacher.RunEpoch(input, output);
            }

            return error;
        }

        public double[] Think(double[] input)
        {
            return (network.Compute(input));
        }
    }
}