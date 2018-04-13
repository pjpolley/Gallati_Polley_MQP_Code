using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Accord.Math;
using Accord.Neuro;
using Accord.Neuro.Learning;


namespace Sender
{
    class NeuralNet
    {
        double[][] dataset_in = new double[8][] { new double[3] { 0, 0, 0 }, new double[3] { 0, 0, 1 }, new double[3] { 0, 1, 0 }, new double[3] { 0, 1, 1 }, new double[3] { 1, 0, 0 }, new double[3] { 1, 0, 1 }, new double[3] { 1, 1, 0 }, new double[3] { 1, 1, 1 } };
        double[][] dataset_out = new double[8][] { new double[1] { 0 }, new double[1] { 0 }, new double[1] { 1 }, new double[1] { 1 }, new double[1] { 1 }, new double[1] { 1 }, new double[1] { 0 }, new double[1] { 0 } };

        private ActivationNetwork network;
        private KFoldData topResults;

        private string ANNfilename = Globals.NeuralNetSaveLocation;
        private string KFoldFilename = Globals.KFoldDataSaveLocation;


        //private CrossValidation<ActivationNetwork, double, double> validator;

        public NeuralNet(int inputs, int outputs)
        {
            NodeSavingReading reader = new NodeSavingReading();
            try
            {
                network = (ActivationNetwork)Network.Load(ANNfilename);
                topResults = reader.GetKFoldDataFromFile(KFoldFilename);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Could not find file, generating new one");
                Validate(inputs, outputs);
            }

            //new ActivationNetwork(new SigmoidFunction(), inputs, returnArray);//
            //



        }

        public double Train(double[][] input, double[][] outputs)
        {
            var teacher = new ResilientBackpropagationLearning(network);
            teacher.LearningRate = topResults.TrainingSpeed;

            double error = 0;
            for (int iteration = 0; iteration < topResults.Iterations; iteration++)
            {
                error = teacher.RunEpoch(input, outputs);
            }

            return error;
        }

        public double[] Think(double[] input)
        {
            return (network.Compute(input));
        }

        public void Save()
        {
            NodeSavingReading reader = new NodeSavingReading();
            network.Save(ANNfilename);
            reader.pushDataToFile(KFoldFilename,topResults);
        }

        public async void Validate(int inputSize, int outputSize)
        {
            List<KFoldData> inputsList = new List<KFoldData>();
            for (double trainingweights = 0.01; trainingweights <= 1; trainingweights += 0.01)
            {
                for (int breadth = 1; breadth <= 5; breadth++)
                {
                    for (int depth = 1; depth < 5; depth++)
                    {
                        inputsList.Add(new KFoldData(breadth, depth, trainingweights, 0, 0));
                    }
                }
            }

            var kFoldList = await Task.WhenAll(inputsList.Select(i => kfold(inputSize, outputSize, i.Breadth, i.Depth, i.TrainingSpeed)));

            KFoldData returnedKFoldData = new KFoldData(0, 0, 0, 0, double.MaxValue);

            KFoldData[] KFoldArray = kFoldList;
            foreach (var tempKFoldData in KFoldArray)
            {
                if (tempKFoldData.K < returnedKFoldData.K)
                {
                    returnedKFoldData = tempKFoldData;
                }
            }

            Console.WriteLine("Best value is:");
            Console.WriteLine("Depth: " + returnedKFoldData.Depth);
            Console.WriteLine("Breadth: " + returnedKFoldData.Breadth);
            Console.WriteLine("Training Speed: " + returnedKFoldData.TrainingSpeed);
            Console.WriteLine("Avg K Value: " + returnedKFoldData.K);

            int[] returnArray = new int[returnedKFoldData.Depth];
            for (int fillVal = 0; fillVal < returnedKFoldData.Depth; fillVal++)
            {
                if (fillVal == returnedKFoldData.Depth - 1)
                {
                    returnArray[fillVal] = outputSize;
                }
                else
                {
                    returnArray[fillVal] = returnedKFoldData.Breadth;
                }
            }

            topResults = returnedKFoldData;
            network = (new ActivationNetwork(new SigmoidFunction(), inputSize, returnArray));
            network.Randomize();
            Save();
            Console.WriteLine("Done!");
        }


        async Task<KFoldData> kfold(int inputSize, int outputSize, int breadth, int depth, double trainingweights)
        {
            await Task.Delay(1).ConfigureAwait(false);
            double bestKVal = double.MaxValue;
            KFoldData bestVal = new KFoldData(0, 0, 0, 0, 0);
            for (int iterations = 10; iterations < 10000; iterations = iterations * 10)
            {

                int[] nodeArray = new int[depth + 1];
                for (int fillVal = 0; fillVal < depth; fillVal++)
                {
                    if (fillVal == 0) // depth - 1)
                    {
                        nodeArray[0] = outputSize;
                    }
                    else
                    {
                        nodeArray[fillVal] = breadth;
                    }
                }



                double kSumAvg = 0;
                for (int i = 0; i < dataset_in.GetLength(0); i++)
                {
                    var testNet = new ActivationNetwork(new SigmoidFunction(), inputSize, nodeArray);
                    var testLearner = new ResilientBackpropagationLearning(testNet);
                    testLearner.LearningRate = trainingweights;

                    var tempDataIn = dataset_in;
                    var tempDataOut = dataset_out;
                    double[][] trainingArrayIn = tempDataIn.RemoveAt(i);
                    double[] testingArrayIn = dataset_in[i];
                    double[][] trainingArrayOut = tempDataOut.RemoveAt(i);
                    double[] testingArrayOut = dataset_out[i];

                    for (int iteration = 0; iteration < iterations; iteration++)
                    {
                        testLearner.RunEpoch(trainingArrayIn, trainingArrayOut);
                    }



                    double kSum = 0;
                    var testResults = testNet.Compute(testingArrayIn);
                    for (int j = 0; j < testResults.Length; j++)
                    {
                        kSum += Math.Abs(testResults[j] - testingArrayOut[j]);
                    }

                    kSumAvg += kSum;
                }

                kSumAvg = kSumAvg / dataset_in.GetLength(0);
                if (kSumAvg == 0)
                {
                    bestKVal = kSumAvg;
                    bestVal = new KFoldData(breadth, depth, trainingweights, iterations, bestKVal);
                    Console.WriteLine("Thread Complete");
                    return bestVal;

                }

                if (kSumAvg < bestKVal)
                {
                    bestKVal = kSumAvg;
                    bestVal = new KFoldData(breadth, depth, trainingweights, iterations, bestKVal);

                }
            }
            Console.WriteLine("Thread Complete");
            return bestVal;
            //return new Task<KFoldData>(() => helperFunction(inputSize, outputSize, breadth,depth,trainingweights));

        }

        //KFoldData helperFunction(int inputSize, int outputSize, int breadth, int depth, double trainingweights)
        //{

        //}
    }
}
