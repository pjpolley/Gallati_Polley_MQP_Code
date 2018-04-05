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

        public Form1()
        {

            this.FormClosing += Globals.CloseAllForms;

            //Initializes read buffer
            inData = new Byte[32];

            //Opens serial port for communication
            //serialPort1 = new SerialPort("COM5", 115200);
            //serialPort1.Open();
            //serialPort1.Write("s");

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
            if (UnityCommunicationHub.connected)
            {
                Globals.T1DesiredPosition = (float)System.Convert.ToDouble(toSend);
                UnityCommunicationHub.TwoWayTransmission();
            }
            else
            {
                Console.WriteLine("ERROR: CONNECT TO UNITY FIRST");
                textBox1.Text = "ERROR: CONNECT TO UNITY FIRST";
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

        private void button3_Click(object sender, EventArgs e)
        {
            UnityCommunicationHub.InitializeUnityCommunication();

        }
    }

}
