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
        SerialReader serialReader;
        Form2 form;
        String toSend;


        public Form1()
        {

            this.FormClosing += Globals.CloseAllForms;

            //Initializes Serial Reader
            serialReader = new SerialReader();

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

                try
                {
                    Globals.T1DesiredPosition = float.Parse(toSend);
                }
                catch(Exception error)
                {
                    Console.WriteLine("ERROR WHEN PARSING FLOAT: " + error.ToString());
                    Globals.T1DesiredPosition = 0;
                }

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
            //Starts reading information from serial
            serialReader.Read();
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
