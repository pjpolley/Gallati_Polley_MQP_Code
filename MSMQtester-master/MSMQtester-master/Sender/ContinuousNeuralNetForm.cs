using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sender
{
    public partial class ContinuousNeuralNetForm : Form
    {

        private NeuralNet net;
        private SerialReader serial;
        private int currentHandPosition;
        private List<double[]> inputTrainingData;
        private List<double[]> outputTrainingData;
        private object dataLock = new object();

        private int expirationTimer = 10;


        private string ANNfilename = Globals.NeuralNetSaveLocation;
        private string KFoldFilename = Globals.KFoldDataSaveLocation;


        public ContinuousNeuralNetForm()
        {
            InitializeComponent();

            net = new NeuralNet(8, 14, ANNfilename, KFoldFilename);
            inputTrainingData = new List<double[]>();
            outputTrainingData = new List<double[]>();

            if (inputTrainingData.Count > expirationTimer)
            {
                inputTrainingData.RemoveAt(0);
                outputTrainingData.RemoveAt(0);
            }

            serial = new SerialReader();
            serial.Read();

            UnityCommunicationHub.InitializeUnityCommunication();
            UnityCommunicationHub.TwoWayTransmission();

            Random rand = new Random(23);


            Globals.T1DesiredPosition = (float)rand.Next(0, 90);
            Globals.T2DesiredPosition = (float)rand.Next(0, 90);
            Globals.A1DesiredPosition = (float)rand.Next(0, 90);
            Globals.A2DesiredPosition = (float)rand.Next(0, 90);
            Globals.A3DesiredPosition = (float)rand.Next(0, 90);
            Globals.B1DesiredPosition = (float)rand.Next(0, 90);
            Globals.B2DesiredPosition = (float)rand.Next(0, 90);
            Globals.B3DesiredPosition = (float)rand.Next(0, 90);
            Globals.C1DesiredPosition = (float)rand.Next(0, 90);
            Globals.C2DesiredPosition = (float)rand.Next(0, 90);
            Globals.C3DesiredPosition = (float)rand.Next(0, 90);
            Globals.D1DesiredPosition = (float)rand.Next(0, 90);
            Globals.D2DesiredPosition = (float)rand.Next(0, 90);
            Globals.D3DesiredPosition = (float)rand.Next(0, 90);

            UnityCommunicationHub.WriteData(true);
        }

        private void Test()
        {
            lock (dataLock)
            {

                var input = serial.GetData();
                UnityCommunicationHub.ReadData();
                var percievedPositionArray = net.Think(input);

                

                inputTrainingData.Add(input);
                outputTrainingData.Add(Globals.GetDoubles());

                for (int i = 0; i < percievedPositionArray.Length; i++)
                {
                    percievedPositionArray[i] = percievedPositionArray[i] * 90;
                }



                Globals.T1DesiredPosition = (float)percievedPositionArray[0];
                Globals.T2DesiredPosition = (float)percievedPositionArray[1];
                Globals.A1DesiredPosition = (float)percievedPositionArray[2];
                Globals.A2DesiredPosition = (float)percievedPositionArray[3];
                Globals.A3DesiredPosition = (float)percievedPositionArray[4];
                Globals.B1DesiredPosition = (float)percievedPositionArray[5];
                Globals.B2DesiredPosition = (float)percievedPositionArray[6];
                Globals.B3DesiredPosition = (float)percievedPositionArray[7];
                Globals.C1DesiredPosition = (float)percievedPositionArray[8];
                Globals.C2DesiredPosition = (float)percievedPositionArray[9];
                Globals.C3DesiredPosition = (float)percievedPositionArray[10];
                Globals.D1DesiredPosition = (float)percievedPositionArray[11];
                Globals.D2DesiredPosition = (float)percievedPositionArray[12];
                Globals.D3DesiredPosition = (float)percievedPositionArray[13];

                UnityCommunicationHub.WriteData(true);

                
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


                net.Train(networkTrainingInput, networkTrainingOutput, 10000, .97f);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            net.Save();
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            Thread testThread = new Thread(Test);
            testThread.Start();
            Thread trainingThread = new Thread(Train);
            trainingThread.Start();
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
                net.Validate(8, 6);
            }
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

        private void Run()
        {
            lock (dataLock)
            {

                var input = serial.GetData();
                var percievedPositionArray = net.Think(input);

                if (input.Contains(double.NaN))
                {
                    while (input.Contains(double.NaN))
                    {
                        input = serial.GetData();
                    }
                }

                for (int i = 0; i < percievedPositionArray.Length; i++)
                {
                    percievedPositionArray[i] = percievedPositionArray[i] * 90;
                }



                Globals.T1DesiredPosition = (float)percievedPositionArray[0];
                Globals.T2DesiredPosition = (float)percievedPositionArray[1];
                Globals.A1DesiredPosition = (float)percievedPositionArray[2];
                Globals.A2DesiredPosition = (float)percievedPositionArray[3];
                Globals.A3DesiredPosition = (float)percievedPositionArray[4];
                Globals.B1DesiredPosition = (float)percievedPositionArray[5];
                Globals.B2DesiredPosition = (float)percievedPositionArray[6];
                Globals.B3DesiredPosition = (float)percievedPositionArray[7];
                Globals.C1DesiredPosition = (float)percievedPositionArray[8];
                Globals.C2DesiredPosition = (float)percievedPositionArray[9];
                Globals.C3DesiredPosition = (float)percievedPositionArray[10];
                Globals.D1DesiredPosition = (float)percievedPositionArray[11];
                Globals.D2DesiredPosition = (float)percievedPositionArray[12];
                Globals.D3DesiredPosition = (float)percievedPositionArray[13];

                UnityCommunicationHub.WriteData(true);


            }
        }

        private void Reader_Click(object sender, EventArgs e)
        {
            Thread testThread = new Thread(Run);
            testThread.Start();
        }
    }
}
