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
    public partial class Form1 : Form
    {
        String toSend;
        Form2 form;

        public Form1()
        {
            InitializeComponent();
            if (!MessageQueue.Exists(".\\Private$\\OhHaiMark")) MessageQueue.Create(".\\Private$\\OhHaiMark");
            form = new Form2();
            form.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            toSend = textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageQueue q = new MessageQueue(".\\Private$\\OhHaiMark");
            q.Send(toSend);
            textBox1.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageQueue.Delete(".\\Private$\\OhHaiMark");
            textBox1.Text = "Deleted";
        }

    }
}
