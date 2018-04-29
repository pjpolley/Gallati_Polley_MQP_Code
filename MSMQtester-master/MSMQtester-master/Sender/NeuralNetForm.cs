using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Converters;
using Accord.Neuro.Learning;

namespace Sender
{
    public partial class NeuralNetForm : Form
    {
        private NeuralNet net;
        private SerialReader serial;
        private int currentHandPosition;
        private List<double[]> inputTrainingData;
        private List<double[]> outputTrainingData;
        private object dataLock = new object();
        private Dictionary<string, int> indexList;
        private Dictionary<int, SetPoint> setPointList;

        private string ANNfilename = Globals.NeuralNetSaveLocation;
        private string KFoldFilename = Globals.KFoldDataSaveLocation;

        public NeuralNetForm()
        {
            InitializeComponent();

            NodeSavingReading reader = new NodeSavingReading();

            net = new NeuralNet(8, 7, ANNfilename, KFoldFilename);
            inputTrainingData = reader.GetStoredDataFromFile(Globals.inputDataStorage);
            outputTrainingData = reader.GetStoredDataFromFile(Globals.outputDataStorage);

            //inputTrainingData = new List<double[]>();
            //outputTrainingData = new List<double[]>();



            serial = new SerialReader();
            serial.Read();

            UnityCommunicationHub.InitializeUnityCommunication();
            UnityCommunicationHub.TwoWayTransmission();


            indexList = Globals.GetBasicValues();
            setPointList = Globals.GetBasicPositions();

            foreach (KeyValuePair<string, int> position in indexList)
            {
                DefaultPositionsBox.Items.Add(position.Key);
            }

            

        }

        private void NeuralNetForm_Load(object sender, EventArgs e)
        {
            
        }

        private void Run()
        {
            //while (true)
            {
                lock (dataLock)
                {

                    var input = serial.GetData();
                    double[] inputData = new double[8];
                    for (int j = 0; j < 8; j++)
                    {
                        inputData[j] = input[j];
                    }
                    var percievedPositionArray = net.Think(inputData);

                    double bestVal = 0;
                    SetPoint bestSetPoint = new SetPoint();


                    for (int i = 0; i < percievedPositionArray.Length; i++)
                    {
                        if (percievedPositionArray[i] > bestVal)
                        {
                            bestVal = percievedPositionArray[i];
                            bestSetPoint = setPointList[i];
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


                net.Train(networkTrainingInput, networkTrainingOutput, 100, 0.1f);
            }
        }

        private void DefaultPositionsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            var inputItemName = (System.Windows.Forms.ListBox) sender;
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
                double[] inputData = new double[8];
                for (int j = 0; j < 8; j++)
                {
                    inputData[j] = inData[j];
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
            Thread testThread = new Thread(Run);
            testThread.Start();
        }

        private void ReconfigureButton_Click(object sender, EventArgs e)
        {
            var inDataArray = new double[inputTrainingData.Count][];
            var outDataArray = new double[outputTrainingData.Count][];

            for (int i = 0; i < inputTrainingData.Count; i++)
            {
                inDataArray[i] = inputTrainingData[i];
                outDataArray[i] = (outputTrainingData[i]);
            }

            net.dataset_in = inDataArray;
            net.dataset_out = outDataArray;

            CertaintyPrompt prompt = new CertaintyPrompt();

            if (prompt.ShowDialog() == DialogResult.OK && prompt.Continue)
            {
                net.Validate(8, 7);
            }
        }

        private double[] ScaleOutputStorageData(double[] inputData)
        {
            var returnData = new double[inputData.Length];
            for (int i = 0; i < inputData.Length; i++)
            {
                returnData[i] = inputData[i] /90;
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
    }
}
