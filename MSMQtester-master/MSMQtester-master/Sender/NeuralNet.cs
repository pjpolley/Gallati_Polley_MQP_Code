using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sender
{
    class NeuralNet
    {
        public static int inputSize;
        public static int outputSize;
        private volatile List<double[,]> weights;
        private Random weightSeed;
        public Mutex weightsMutex;
        private double[,] input;
        private double[,] output;
        private int iterations;

        public NeuralNet(int desiredInputSize, int desiredOutputSize, int breadth = 10, int depth = 2)
        {
            weightsMutex = new Mutex();
            weightSeed = new Random();
            inputSize = desiredInputSize;
            outputSize = desiredOutputSize;

            if (breadth != 0 && depth != 0)
            {
                weights = new List<double[,]>();
                for (int i = 0; i < depth; i++)
                {
                    if (i == 0) weights.Add(generateWeights(desiredInputSize, breadth));
                    else if (i != (depth - 1)) weights.Add(generateWeights(breadth, breadth));
                    else weights.Add(generateWeights(breadth, desiredOutputSize));
                }
            }
        }

        //Trains Neural Net
        public void Train(double[,] inputArray, double[,] outputArray, int iterations = 10000)
        {
            if (inputArray.GetUpperBound(1) + 1 != inputSize || outputArray.GetUpperBound(1) + 1 != outputSize) throw new Exception();
            this.iterations = iterations;
            input = new double[1, inputArray.GetLength(1)];
            output = new double[1, outputArray.GetLength(1)];


            for (int i = 0; i < inputArray.GetLength(0); i++)
            {
                var threaddy = new Thread(new ThreadStart(trainArray));
                for (int j = 0; j < inputArray.GetLength(1); j++)
                {
                    input[0, j] = inputArray[i, j];
                }
                for (int j = 0; j < outputArray.GetLength(1); j++)
                {
                    output[0, j] = outputArray[i, j];
                }
                threaddy.Start();
            }
        }

        //Predicts Data
        public double[,] Predict(double[,] inputArray)
        {
            weightsMutex.WaitOne();
            if (inputArray.GetUpperBound(1) + 1 != inputSize) throw new Exception();

            List<double[,]> sums = new List<double[,]>();
            for (int i = 0; i < weights.Count; i++)
            {
                if (i == 0) sums.Add(multiplyArrays(inputArray, weights[i]));
                else sums.Add(multiplyArrays(sigmoid(sums[i - 1]), weights[i]));
            }
            weightsMutex.ReleaseMutex();
            return (sigmoid(sums.Last()));
        }


        //Generates random weights between 0 and 1 in array of size x by y
        private double[,] generateWeights(int x, int y)
        {
            var returnArray = new double[x, y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    returnArray[i, j] = weightSeed.NextDouble();
                }
            }
            return (returnArray);
        }

        //Multiplies two 2d double arrays
        private double[,] multiplyArrays(double[,] a, double[,] b)
        {
            var returnArray = new double[(a.GetUpperBound(0) + 1), (b.GetUpperBound(1) + 1)];
            if (a.GetUpperBound(1) == b.GetUpperBound(0))
            {
                for (int i = 0; i <= a.GetUpperBound(0); i++)
                {
                    for (int j = 0; j <= b.GetUpperBound(1); j++)
                    {
                        for (int k = 0; k <= a.GetUpperBound(1); k++)
                        {
                            returnArray[i, j] = returnArray[i, j] + a[i, k] * b[k, j];
                        }
                    }
                }
            }
            else
            {
                throw new IndexOutOfRangeException();
            }

            return (returnArray);
        }

        //Like Multiply but not
        private double[,] divideArrays(double[,] a, double[,] b)
        {
            var returnArray = new double[(a.GetUpperBound(0) + 1), (b.GetUpperBound(1) + 1)];
            if (a.GetUpperBound(1) == b.GetUpperBound(0))
            {
                for (int i = 0; i <= a.GetUpperBound(0); i++)
                {
                    for (int j = 0; j <= b.GetUpperBound(1); j++)
                    {
                        for (int k = 0; k <= a.GetUpperBound(1); k++)
                        {
                            if (b[k, j] != 0) returnArray[i, j] = returnArray[i, j] + a[i, k] / b[k, j];
                            else returnArray[i, j] = returnArray[i, j] + a[i, k] / 0.00000001;
                        }
                    }
                }
            }
            else
            {
                throw new IndexOutOfRangeException();
            }

            return (returnArray);
        }

        //Subtracts 2d array b from same-size 2d array b
        //TODO: Error checking to make sure that a and b are same size
        private double[,] subtractArrays(double[,] a, double[,] b)
        {
            var returnArray = new double[(a.GetUpperBound(0) + 1), (b.GetUpperBound(1) + 1)];
            for (int i = 0; i <= a.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= a.GetUpperBound(1); j++)
                {
                    returnArray[i, j] = a[i, j] - b[i, j];
                    //if (returnArray[i, j] < 0) returnArray[i, j] = 0;
                }
            }

            return (returnArray);
        }

        //Runs every line in a 2d array through a sigmoid function (or derivative of one)
        private double[,] sigmoid(double[,] inArray, bool isDerivative = false)
        {
            double[,] returnArray = new double[(inArray.GetUpperBound(0) + 1), (inArray.GetUpperBound(1) + 1)];

            for (int i = 0; i <= inArray.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= inArray.GetUpperBound(1); j++)
                {
                    returnArray[i, j] = (1 / (1 + Math.Exp(inArray[i, j])));
                    if (isDerivative) returnArray[i, j] = (returnArray[i, j] * (1 - returnArray[i, j]));
                }
            }

            return (returnArray);

        }

        //Multiplies individual values in 2d array a with corresponding entries in same-size 2d array b
        //TODO: Error checking to make sure that a and b are same size
        private double[,] multiplyIndividualArrayEntries(double[,] a, double[,] b)
        {
            var returnArray = new double[(a.GetUpperBound(0) + 1), (b.GetUpperBound(1) + 1)];
            for (int i = 0; i <= a.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= a.GetUpperBound(1); j++)
                {
                    returnArray[i, j] = a[i, j] * b[i, j];
                }
            }

            return (returnArray);
        }

        private double[,] transpose(double[,] inputArray)
        {
            var returnArray = new double[(inputArray.GetUpperBound(1) + 1), (inputArray.GetUpperBound(0) + 1)];
            for (int i = 0; i <= inputArray.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= inputArray.GetUpperBound(1); j++)
                {
                    returnArray[j, i] = inputArray[i, j];
                }
            }
            return (returnArray);
        }

        private double[,] divide(double[,] inputArray, double[,] divideVal)
        {
            var returnArray = new double[(inputArray.GetUpperBound(0) + 1), (inputArray.GetUpperBound(1) + 1)];
            for (int i = 0; i <= inputArray.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= inputArray.GetUpperBound(1); j++)
                {
                    returnArray[i, j] = inputArray[i, j] / divideVal[0, 0];
                }
            }
            return (returnArray);
        }

        private void trainArray()
        {
            var inputArray = input;
            var outputArray = output;

            while (true)
            {
                weightsMutex.WaitOne();
                List<double[,]> sums = new List<double[,]>();
                for (int i = 0; i < weights.Count; i++)
                {
                    if (i == 0) sums.Add(multiplyArrays(inputArray, weights[i]));
                    else sums.Add(multiplyArrays(sigmoid(sums[i - 1]), weights[i]));
                }

                var ogWeights = weights;
                var errorArray = new List<double[,]>();
                for (int i = (weights.Count - 1); i >= 0; i--)
                {
                    var error = new double[(sums[i].GetUpperBound(0) + 1), (sums[i].GetUpperBound(1) + 1)];
                    var weightCorrections = new double[weights[i].GetLength(0), weights[i].GetLength(1)];

                    if (i == (weights.Count - 1))
                    {
                        error = subtractArrays(outputArray, sigmoid(sums[i]));
                        errorArray.Add(error);
                        var errorProportion = multiplyIndividualArrayEntries((sigmoid(sums[i], true)), error);
                        for (int k = 0; k <= errorProportion.GetUpperBound(1); k++)
                        {
                            for (int l = 0; l <= weightCorrections.GetUpperBound(0); l++)
                            {
                                weightCorrections[l, k] = (errorProportion[0, k] / sums[i][0, k]);
                            }
                        }

                    }
                    else
                    {
                        error = multiplyIndividualArrayEntries(divideArrays(errorArray.Last(), transpose(weights[i + 1])), sigmoid(sums[i], true));
                        errorArray.Add(error);

                        if (i != 0) weightCorrections = divideArrays(transpose(error), (sigmoid(sums[i - 1])));
                        else weightCorrections = transpose(divideArrays(transpose(error), (inputArray)));

                    }

                    weights[i] = subtractArrays(weights[i], weightCorrections);



                    /*
                    var weightCorrections = multiply(sigmoid(weights[i], true), correction);
                    weights[i] = subtractArrays(weights[i], weightCorrections);
                    */
                }
                weightsMutex.ReleaseMutex();
            }
        }
    }
}
