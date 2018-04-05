using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sender
{
    public partial class WelcomeScreen : Form
    {
        public WelcomeScreen()
        {
            this.FormClosing += Globals.CloseAllForms;

            InitializeComponent();
        }


        private void BasicFunctionalityButton_Click(object sender, EventArgs e)
        {
            if(Globals.form1 == null)
            {
                Globals.form1 = new Form1();
            }
            Globals.form1.Show();
            this.Hide();
        }

        private void NeuralTreeButton_Click(object sender, EventArgs e)
        {
            if (Globals.treeScreen == null)
            {
                Globals.treeScreen = new NeuralTreeWindow();
            }
            Globals.treeScreen.Show();
            this.Hide();
        }
    }
}
