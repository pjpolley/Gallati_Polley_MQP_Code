﻿using System;
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
            //if(Globals.form1 == null)
            //{
            //    Globals.form1 = new Form1();
            //}
            //Globals.form1.Show();
            //this.Hide();
            if (Globals.ContinuousNeuralNetForm == null)
            {
                Globals.ContinuousNeuralNetForm = new ContinuousNeuralNetForm();
            }
            Globals.ContinuousNeuralNetForm.Show();
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

        private void NeuralNetButton_Click(object sender, EventArgs e)
        {
            if (Globals.netScreen == null)
            {
                Globals.netScreen = new NeuralNetForm();
            }
            Globals.netScreen.Show();
            this.Hide();
        }

        private void UnsureNetworkButton_Click(object sender, EventArgs e)
        {
            if (Globals.UnsureNetworkForm == null)
            {
                Globals.UnsureNetworkForm = new UnsureNetworkForm();
            }
            Globals.UnsureNetworkForm.Show();
            this.Hide();
        }
    }
}
