using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Converters;
using Accord.Math;
using Accord.Neuro.Learning;

namespace Sender
{
    public partial class UnsureNetworkForm : Form
    {
        private NeuralNet net;
        private SerialReader serial;
        private int currentHandPosition;
        private List<double[]> inputTrainingData;
        private List<double[]> outputTrainingData;
        private object dataLock = new object();
        private Dictionary<string, int> indexList;
        private Dictionary<int, string> reverseIndexList;
        private Dictionary<int, SetPoint> setPointList;

        Stopwatch timer = new Stopwatch();
        private List<Threshhold> ranges;
        PatsControlScheme controls;
        int rate;

        private string ANNfilename = Globals.NeuralNetSaveLocation;
        private string KFoldFilename = Globals.KFoldDataSaveLocation;

        public UnsureNetworkForm()
        {
            InitializeComponent();

            NodeSavingReading reader = new NodeSavingReading();

            net = new NeuralNet(7, 7, ANNfilename, KFoldFilename);
            inputTrainingData = reader.GetStoredDataFromFile(Globals.inputDataStorage);
            outputTrainingData = reader.GetStoredDataFromFile(Globals.outputDataStorage);

            //inputTrainingData = new List<double[]>();
            //outputTrainingData = new List<double[]>();

            controls = new PatsControlScheme();
            controls.Initialize();
            controls.timeNeededForChange = 5000;

            serial = new SerialReader();
            serial.Read();

            UnityCommunicationHub.InitializeUnityCommunication();
            UnityCommunicationHub.TwoWayTransmission();


            indexList = Globals.GetBasicValues();
            reverseIndexList = Globals.GetBasicValuesReversed();
            setPointList = Globals.GetBasicPositions();

            foreach (KeyValuePair<string, int> position in indexList)
            {
                DefaultPositionsBox.Items.Add(position.Key);
            }



        }


        private void Run()
        {
            //while (true)
            {
                lock (dataLock)
                {

                    var input = serial.GetData();
                    double[] inputData = new double[7];
                    for (int j = 0; j < 8; j++)
                    {
                        if (j < 1) inputData[j] = input[j];
                        else if (j > 1) inputData[j - 1] = input[j];
                    }
                    var percievedPositionArray = net.Think(inputData);

                    double bestVal = 0;
                    SetPoint bestSetPoint = new SetPoint();
                    bool goodToMove = false;

                    while (!goodToMove)
                    {
                        var index = 0;
                        for (int i = 0; i < percievedPositionArray.Length; i++)
                        {
                            if (percievedPositionArray[i] > bestVal)
                            {
                                bestVal = percievedPositionArray[i];
                                index = i;
                                bestSetPoint = setPointList[i];
                            }
                        }

                        if (bestVal > 0.95)
                        {
                            goodToMove = true;
                        }
                        else
                        {
                            PositionTextBox.Text = reverseIndexList[index];
                            goodToMove = RunFocus();
                            if (!goodToMove)
                            {
                                percievedPositionArray[index] = 0;
                            }
                        }
                    }

                    var percievedPosition = bestSetPoint;

                    Globals.T1DesiredPosition = percievedPosition.T1Position;
                    Globals.T2DesiredPosition = percievedPosition.T2Position;
                    Globals.A1DesiredPosition = percievedPosition.A1Position;
                    Globals.A2DesiredPosition = percievedPosition.A2Position;
                    Globals.A3DesiredPosition = percievedPosition.A3Position;
                    Globals.B1DesiredPosition = percievedPosition.B1Position;
                    Globals.B2DesiredPosition = percievedPosition.B2Position;
                    Globals.B3DesiredPosition = percievedPosition.B3Position;
                    Globals.C1DesiredPosition = percievedPosition.C1Position;
                    Globals.C2DesiredPosition = percievedPosition.C2Position;
                    Globals.C3DesiredPosition = percievedPosition.C3Position;
                    Globals.D1DesiredPosition = percievedPosition.D1Position;
                    Globals.D2DesiredPosition = percievedPosition.D2Position;
                    Globals.D3DesiredPosition = percievedPosition.D3Position;

                    UnityCommunicationHub.WriteData(true);
                }

            }
        }

        private void Train()
        {
            lock (dataLock)
            {
                var networkTrainingInput = new double[inputTrainingData.Count][];
                var networkTrainingOutput = new double[outputTrainingData.Count][];
                for (int i = 0; i < inputTrainingData.Count; i++)
                {
                    networkTrainingInput[i] = inputTrainingData[i];
                    networkTrainingOutput[i] = outputTrainingData[i];
                }


                net.Train(networkTrainingInput, networkTrainingOutput, 100, .10f);
            }
        }

        private void DefaultPositionsBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            var inputItemName = (System.Windows.Forms.ListBox)sender;
            currentHandPosition = indexList[(string)inputItemName.SelectedItem];
        }



        private void SaveButton_Click(object sender, EventArgs e)
        {
            NodeSavingReading reader = new NodeSavingReading();
            net.Save();
            reader.pushDataToFile(Globals.inputDataStorage, inputTrainingData);
            reader.pushDataToFile(Globals.outputDataStorage, outputTrainingData);
        }

        private void logButton_Click(object sender, EventArgs e)
        {
            Thread.Sleep(200);
            for (int i = 0; i < 50; i++)
            {
                double[] inData = serial.GetData();
                double[] inputData = new double[7];
                for (int j = 0; j < 8; j++)
                {
                    if (j < 1) inputData[j] = inData[j];
                    else if (j > 1) inputData[j - 1] = inData[j]; 
                }
                inputTrainingData.Add(inputData);
                double[] outputData = new double[7];
                outputData[currentHandPosition] = 1;
                outputTrainingData.Add(outputData);
                Thread.Sleep(1);
            }
        }

        private void TrainButton_Click(object sender, EventArgs e)
        {
            Thread trainingThread = new Thread(Train);
            trainingThread.Start();
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            Thread.Sleep(200);
            //Thread testThread = new Thread(Run);
            //testThread.Start();
            Run();
        }


        private double[] ScaleOutputStorageData(double[] inputData)
        {
            var returnData = new double[inputData.Length];
            for (int i = 0; i < inputData.Length; i++)
            {
                returnData[i] = inputData[i] / 90;
            }

            return returnData;
        }

        private float[] ScaleOutputData(double[] inputData)
        {
            var returnData = new float[inputData.Length];
            for (int i = 0; i < inputData.Length; i++)
            {
                returnData[i] = (float)inputData[i] * (90);
            }

            return returnData;
        }

        private void FocusButton_Click(object sender, EventArgs e)
        {
                int desiredMillisecondDelay = controls.timeNeededForChange;
                int arraySize = (desiredMillisecondDelay / 1000) * rate;
                serial.Read();
                rate = serial.getRate();

                double lowConcentration = 0;
                double highConcentration = 0;

                //first get threshholds
                bool done = false;
                while (!done)
                {

                    int reads = 0;
                    decimal allReads = 0;
                    MessageBox.Show("First try to let your mind wander until the next popup appears. Hit OK when ready.", string.Empty, MessageBoxButtons.OK);
                    timer.Start();
                    while (timer.ElapsedMilliseconds < Globals.threshholdAquisitionTime)
                    {
                        decimal currentIn = (decimal)Math.Abs(serial.GetData()[Globals.inputNode]);
                        allReads += currentIn;
                        reads++;
                    }

                    timer.Reset();
                    lowConcentration = (double)(allReads / reads);
                    Console.WriteLine("Low concentration was: " + lowConcentration);

                    reads = 0;
                    allReads = 0;

                    MessageBox.Show("Next try to focus as hard as possible something. Hit OK when ready.", string.Empty, MessageBoxButtons.OK);

                    timer.Start();
                    while (timer.ElapsedMilliseconds < Globals.threshholdAquisitionTime)
                    {
                        decimal currentIn = (decimal)Math.Abs(serial.GetData()[Globals.inputNode]);
                        allReads += currentIn;
                        reads++;
                    }
                    timer.Reset();

                    highConcentration = (double)(allReads / reads);

                    Console.WriteLine("High concentration was: " + highConcentration);

                    if (highConcentration > lowConcentration)
                    {
                        done = true;
                    }
                }

                double differenceInConcentrations = highConcentration - lowConcentration;
                double deltaBetweenThreshholds = differenceInConcentrations / 2;

                //make ranges for this run
                ranges = new List<Threshhold>(2);


                        //make sure all reads work for it
                        ranges.Add(new Threshhold(Double.MinValue, lowConcentration + (deltaBetweenThreshholds)));

                        ranges.Add(new Threshhold(lowConcentration + (deltaBetweenThreshholds), Double.MaxValue));


                MessageBox.Show("Ready to control hand. Press OK when ready.", string.Empty, MessageBoxButtons.OK);
        }

        private bool RunFocus()
        {
            
            int desiredMillisecondDelay = controls.timeNeededForChange;
            timer = new Stopwatch();

                //get the inputs and average them for the desired output
                double averageInput = 0;
                Random rand = new Random();
                var numInputs = 0;
                decimal accruedValues = 0;
                timer.Start();
                decimal currentIn;
                while (timer.ElapsedMilliseconds < desiredMillisecondDelay)
                {
                    currentIn = (decimal)Math.Abs(serial.GetData()[Globals.inputNode]);
                    accruedValues += currentIn;
                    numInputs++;
                    double currentVal = (double)(accruedValues / numInputs);
                    if (currentVal > ranges[0].max)
                    {
                        FocusTextBox.Text = "Yes, " + currentVal;
                    }
                    else
                    {
                        FocusTextBox.Text = "No, " + currentVal;
                    }
                }
                averageInput = (double)(accruedValues / numInputs);

                if (averageInput > ranges[0].max)
                {
                    return true;
                }
                else
                {
                    return false;
                }

                
            
        }

        private void FocusTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
