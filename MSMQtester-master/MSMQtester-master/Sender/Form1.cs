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
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.WriteLine("START");
            }
            //wait for confirmation step
            while (!File.Exists(unityReadyToGo))
            {

            }

            this.FormClosing += Form1_FormClosing;

            //Initializes read buffer
            inData = new Byte[32];

            //Opens serial port for communication
            serialPort1 = new SerialPort("COM5", 115200);
            serialPort1.Open();
            serialPort1.Write("s");

            InitializeComponent();
            form = new Form2();
            form.Show();

            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            toSend = textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Globals.T1DesiredPosition = (float)System.Convert.ToDouble(toSend);
            ReadData();
        }

        //returns true if it was able to aquire the file to read and then write to the file, false otherwise
        private bool ReadData()
        {
            if (File.Exists(mutexFileTurn))
            {
                //first get the position from the hand
                string line = "";
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        switch (line.Substring(0, 2))
                        {
                            case "T1":
                                Globals.T1ActualPosition = (float)System.Convert.ToDouble(line.Substring(2));
                                break;
                            case "T2":
                                Globals.T2ActualPosition = (float)System.Convert.ToDouble(line.Substring(2));
                                break;
                            case "A1":
                                Globals.A1ActualPosition = (float)System.Convert.ToDouble(line.Substring(2));
                                break;
                            case "A2":
                                Globals.A2ActualPosition = (float)System.Convert.ToDouble(line.Substring(2));
                                break;
                            case "A3":
                                Globals.A3ActualPosition = (float)System.Convert.ToDouble(line.Substring(2));
                                break;
                            case "B1":
                                Globals.B1ActualPosition = (float)System.Convert.ToDouble(line.Substring(2));
                                break;
                            case "B2":
                                Globals.B2ActualPosition = (float)System.Convert.ToDouble(line.Substring(2));
                                break;
                            case "B3":
                                Globals.B3ActualPosition = (float)System.Convert.ToDouble(line.Substring(2));
                                break;
                            case "C1":
                                Globals.C1ActualPosition = (float)System.Convert.ToDouble(line.Substring(2));
                                break;
                            case "C2":
                                Globals.C2ActualPosition = (float)System.Convert.ToDouble(line.Substring(2));
                                break;
                            case "C3":
                                Globals.C3ActualPosition = (float)System.Convert.ToDouble(line.Substring(2));
                                break;
                            case "D1":
                                Globals.D1ActualPosition = (float)System.Convert.ToDouble(line.Substring(2));
                                break;
                            case "D2":
                                Globals.D2ActualPosition = (float)System.Convert.ToDouble(line.Substring(2));
                                break;
                            case "D3":
                                Globals.D3ActualPosition = (float)System.Convert.ToDouble(line.Substring(2));
                                break;
                        }

                    }
                }

                //and now write your own data to the file
                File.Delete(filePath);
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.WriteLine("T1" + Globals.T1DesiredPosition);
                    sw.WriteLine("T2" + Globals.T2DesiredPosition);
                    sw.WriteLine("A1" + Globals.A1DesiredPosition);
                    sw.WriteLine("A2" + Globals.A2DesiredPosition);
                    sw.WriteLine("A3" + Globals.A3DesiredPosition);
                    sw.WriteLine("B1" + Globals.B1DesiredPosition);
                    sw.WriteLine("B2" + Globals.B2DesiredPosition);
                    sw.WriteLine("B3" + Globals.B3DesiredPosition);
                    sw.WriteLine("C1" + Globals.C1DesiredPosition);
                    sw.WriteLine("C2" + Globals.C2DesiredPosition);
                    sw.WriteLine("C3" + Globals.C3DesiredPosition);
                    //sw.WriteLine("D1" + Globals.D1DesiredPosition);
                    sw.WriteLine("D2" + Globals.D2DesiredPosition);
                    sw.WriteLine("D3" + Globals.D3DesiredPosition);
                    sw.WriteLine("From sender");
                }

                switchToUnity();
                return true;
            }
            else
            {
                return false;
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
            //Opens the floodgates
            serialPort1.Write("b");

            while (true)
            {
                //Checks for start of packet; if it finds it, verifies it's a packet, then processes data
                if (serialPort1.ReadByte() == 0xA0)
                {
                    serialPort1.Read(inData, 0, 32);
                    if (inData[31] > 0xBF && inData[31] < 0xD0 && inData[0] == 0)
                    {
                        int outVal = interpret24bitAsInt32(inData[1], inData[2], inData[3]);
                        double returnVal = outVal * 0.02235;
                        textBox1.Text = returnVal.ToString();
                    }
                }

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Really close this form?", string.Empty, MessageBoxButtons.YesNo);
            File.Delete(unityReadyToGo);
            File.Delete(WFAReadyToGo);
            File.Delete(filePath);
            File.Delete(mutexFileTurn);
            File.Delete(mutexUnityTurn);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        //Converts 3 bytes to signed Int32 (from OpenBCI)
        int interpret24bitAsInt32(byte byte1, byte byte2, byte byte3)
            {
                int newInt = (
                    ((0xFF & byte1) << 16) |
                    ((0xFF & byte2) << 8) |
                    (0xFF & byte3)
                  );
                if ((newInt & 0x00800000) > 0)
                {
                    newInt = (int)((uint)newInt | (uint)0xFF000000);
                }
                else
                {
                    newInt = (int)((uint)newInt & (uint)0x00FFFFFF);
                }
                return (newInt);
            }
        }

}
