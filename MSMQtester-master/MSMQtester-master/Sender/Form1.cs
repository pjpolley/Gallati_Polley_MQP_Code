using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Messaging;
using System.IO;
using System.IO.Pipes;
using System.Security.Principal;
using System.Diagnostics;
using System.Threading;
using System.IO.Ports;

namespace Sender
{
    public partial class Form1 : Form
    {
        String toSend;
        Form2 form;
        Byte[] inData;

        string dataPath = @"c:\Program Files\OpenBCI_GUI\SavedData\OpenBCI-RAW-readThis.txt";
        string directoryPath = @"c:\BCIDataDirectory";
        string filePath = @"c:\BCIDataDirectory\transferInfo.txt";
        string mutexFileTurn = @"c:\BCIDataDirectory\WFATurn.mutex";
        string mutexUnityTurn = @"c:\BCIDataDirectory\UnityTurn.mutex";
        string unityReadyToGo = @"c:\BCIDataDirectory\UnityReady.txt";
        string WFAReadyToGo = @"c:\BCIDataDirectory\WFAReady.txt";

        public Form1()
        {
            // Determine whether the directory exists.
            if (Directory.Exists(directoryPath))
            {
                Console.WriteLine("Directory path exists already. Proceeding as normal.");
            }
            else
            {
                // Try to create the directory.
                Console.WriteLine("Creating directory path.");
                DirectoryInfo di = Directory.CreateDirectory(directoryPath);
            }

            //make sure to clean up any excess data from last run
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            if (File.Exists(mutexFileTurn))
            {
                File.Delete(mutexFileTurn);
            }
            if (File.Exists(mutexUnityTurn))
            {
                File.Delete(mutexUnityTurn);
            }
            using (StreamWriter sw = new StreamWriter(mutexFileTurn))
            {
                sw.WriteLine("g");
            }
            using (StreamWriter sw = new StreamWriter(WFAReadyToGo))
            {
                sw.WriteLine("g");
            }
            //wait for confirmation step
            while (!File.Exists(unityReadyToGo))
            {

            }

            this.FormClosing += Form1_FormClosing;

            //Initializes read buffer
            inData = new Byte[25];

            //Opens serial port for communication
            serialPort1 = new SerialPort("COM5", 115200);
            serialPort1.Open();

            InitializeComponent();
            form = new Form2();
            form.Show();

            //Opens the floodgates
            serialPort1.Write("b");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            toSend = textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists(mutexFileTurn))
            {
                File.Delete(filePath);
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.WriteLine("T1" + toSend);
                }

                switchToUnity();
            }
        }

        private void switchToUnity()
        {
            File.Delete(mutexFileTurn);
            using (StreamWriter sw = new StreamWriter(mutexUnityTurn))
            {
                sw.WriteLine("G");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            while (true)
            {
                //If serial port reads the beginning of a packet, makes the data packet and prints the 1st node info
                if (serialPort1.ReadByte() == 0xA0)
                {
                    serialPort1.Read(inData, 0, 25);
                    Byte[] node1 = new ArraySegment<Byte>(inData, 0, 4).ToArray<Byte>();
                    node1[0] = 0;
                    node1 = node1.Reverse<Byte>().ToArray<Byte>();
                    textBox1.Text = BitConverter.ToInt32(inData, 1).ToString();
                }
                //Else it just keeps iterating
                else
                {
                    serialPort1.ReadByte();
                }
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Really close this form?", string.Empty, MessageBoxButtons.YesNo);
            File.Delete(unityReadyToGo);
            File.Delete(WFAReadyToGo);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }

}
