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
    public partial class NeuralNetForm : Form
    {
        private NeuralNet net;
        private SerialReader serial;
        private SetPoint currentHandPosition = new SetPoint();
        private List<double[]> inputTrainingData;
        private List<double[]> outputTrainingData;
        private object dataLock = new object();

        public NeuralNetForm()
        {
            InitializeComponent();

            net = new NeuralNet(8, 10);

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

            NodeSavingReading setpointLoader = new NodeSavingReading();
            List<SetPoint> setPointList = setpointLoader.GetSetPointDataFromFile(Globals.DefaultHandPositionsLocation);
            foreach (var setPoint in setPointList)
            {
                DefaultPositionsBox.Items.Add(setPoint);
            }
        }

        private void NeuralNetForm_Load(object sender, EventArgs e)
        {
            
        }

        private void Run()
        {
            lock (dataLock)
            {
                var input = serial.GetData();
                net.Think(input);
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


                net.Train(networkTrainingInput, networkTrainingOutput);
            }
        }

        private void DefaultPositionsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentHandPosition = (SetPoint)sender;
        }



        private void SaveButton_Click(object sender, EventArgs e)
        {
            net.Save();
        }

        private void logButton_Click(object sender, EventArgs e)
        {
            inputTrainingData.Add(serial.GetData());
            outputTrainingData.Add(currentHandPosition.ConvertToDoubles());
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
            CertaintyPrompt prompt = new CertaintyPrompt();

            if (prompt.ShowDialog() == DialogResult.OK && prompt.Continue)
            {
                net.Validate(8, 10);
            }
        }
    }
}
