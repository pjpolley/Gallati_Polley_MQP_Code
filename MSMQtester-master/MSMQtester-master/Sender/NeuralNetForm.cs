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
        private SetPoint currentHandPosition;
        private List<double[]> inputTrainingData;
        private List<double[]> outputTrainingData;
        private object dataLock = new object();

        public NeuralNetForm()
        {
            InitializeComponent();
        }

        private void NeuralNetForm_Load(object sender, EventArgs e)
        {
            net = new NeuralNet(8, 10, 5);
            serial = new SerialReader();

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


                net.Train(networkTrainingInput, networkTrainingOutput, 1);
            }
        }

        private void DefaultPositionsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentHandPosition = (SetPoint)sender;
        }



        private void SaveButton_Click(object sender, EventArgs e)
        {
            //TODO: SAVE FUNCTION
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
    }
}
