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

namespace Sender
{
    public partial class NeuralNetForm : Form
    {
        private NeuralNet net;
        private SerialReader serial;
        private SetPoint currentHandPosition = new SetPoint();
        private List<double[]> inputTrainingData;
        private List<double[]> outputTrainingData;
        private object dataLock = new object();
        private Dictionary<string, SetPoint> setPointList;

        public NeuralNetForm()
        {
            InitializeComponent();

            net = new NeuralNet(8, 14);
            inputTrainingData = new List<double[]>();
            outputTrainingData = new List<double[]>();
            serial = new SerialReader();

            UnityCommunicationHub.InitializeUnityCommunication();
            UnityCommunicationHub.TwoWayTransmission();

            

            currentHandPosition.A1Position = Globals.A1ActualPosition;
            currentHandPosition.A2Position = Globals.A2ActualPosition;
            currentHandPosition.A3Position = Globals.A3ActualPosition;
            currentHandPosition.B1Position = Globals.B1ActualPosition;
            currentHandPosition.B2Position = Globals.B2ActualPosition;
            currentHandPosition.B3Position = Globals.B3ActualPosition;
            currentHandPosition.C1Position = Globals.C1ActualPosition;
            currentHandPosition.C2Position = Globals.C2ActualPosition;
            currentHandPosition.C3Position = Globals.C3ActualPosition;
            currentHandPosition.D1Position = Globals.D1ActualPosition;
            currentHandPosition.D2Position = Globals.D2ActualPosition;
            currentHandPosition.D3Position = Globals.D3ActualPosition;
            currentHandPosition.T1Position = Globals.T1ActualPosition;
            currentHandPosition.T2Position = Globals.T2ActualPosition;

            setPointList = Globals.GetBasicPositions();
            foreach (KeyValuePair<string, SetPoint> position in setPointList)
            {
                DefaultPositionsBox.Items.Add(position.Key);
            }

            serial.Read();

        }

        private void NeuralNetForm_Load(object sender, EventArgs e)
        {
            
        }

        private void Run()
        {
            lock (dataLock)
            {
                
                var input = serial.GetData();
                var percievedPosition = ScaleOutputData(net.Think(input));
                
                Globals.T1DesiredPosition = percievedPosition[0];
                Globals.T2DesiredPosition = percievedPosition[1];
                Globals.A1DesiredPosition = percievedPosition[2];
                Globals.A2DesiredPosition = percievedPosition[3];
                Globals.A3DesiredPosition = percievedPosition[4];
                Globals.B1DesiredPosition = percievedPosition[5];
                Globals.B2DesiredPosition = percievedPosition[6];
                Globals.B3DesiredPosition = percievedPosition[7];
                Globals.C1DesiredPosition = percievedPosition[8];
                Globals.C2DesiredPosition = percievedPosition[9];
                Globals.C3DesiredPosition = percievedPosition[10];
                Globals.D1DesiredPosition = percievedPosition[11];
                Globals.D2DesiredPosition = percievedPosition[12];
                Globals.D3DesiredPosition = percievedPosition[13];

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
                    networkTrainingOutput[i] = ScaleOutputStorageData(outputTrainingData[i]);
                }


                net.Train(networkTrainingInput, networkTrainingOutput);
            }
        }

        private void DefaultPositionsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var inputItemName = (System.Windows.Forms.ListBox) sender;
            currentHandPosition = setPointList[(string)inputItemName.SelectedItem];
        }



        private void SaveButton_Click(object sender, EventArgs e)
        {
            net.Save();
        }

        private void logButton_Click(object sender, EventArgs e)
        {
            inputTrainingData.Add(serial.GetData());
            outputTrainingData.Add(ScaleOutputStorageData(currentHandPosition.ConvertToDoubles()));
        }

        private void TrainButton_Click(object sender, EventArgs e)
        {
            Thread trainingThread = new Thread(Train);
            trainingThread.Start();
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
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
                outDataArray[i] = ScaleOutputStorageData(outputTrainingData[i]);
            }

            net.dataset_in = inDataArray;
            net.dataset_out = outDataArray;

            CertaintyPrompt prompt = new CertaintyPrompt();

            if (prompt.ShowDialog() == DialogResult.OK && prompt.Continue)
            {
                net.Validate(8, 14);
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
