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

namespace Sender
{
    public partial class Form2 : Form
    {
        MessageQueue readerThingy;
        String textOut;

        public Form2()
        {
            InitializeComponent();
            readerThingy = new MessageQueue(".\\Private$\\OhHaiMark");
            textOut = "Waiting for Message";
            textBox1.Text = textOut;
            

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Messaging.Message inMessage = readerThingy.Receive();
            inMessage.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });
            textOut = (String)inMessage.Body;
            textBox1.Text = textOut;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
