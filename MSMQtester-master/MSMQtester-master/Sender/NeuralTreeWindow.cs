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
    public partial class NeuralTreeWindow : Form
    {
        TreeNode activeNode = null;

        public NeuralTreeWindow()
        {
            this.FormClosing += Globals.CloseAllForms;

            InitializeComponent();

            TreeNode testNode = new TreeNode("Test");
            testNode.Nodes.Add("Test child");
            NeuronTreeView.Nodes.Add(testNode);
        }

        private void NeuronTreeView_NodeClicked(object sender, TreeNodeMouseClickEventArgs e)
        {
            activeNode = e.Node;
            Console.WriteLine(e.Node.Text + " Clicked");
        }

        private void setHandPositionButton_Click(object sender, EventArgs e)
        {

        }

        private void AddAnotherLayerButton_Click(object sender, EventArgs e)
        {

        }

        private void removeLayerButton_Click(object sender, EventArgs e)
        {

        }

        private void changeNameButton_Click(object sender, EventArgs e)
        {

        }

        private void setNumberOfPositionsPerLayerButton_Click(object sender, EventArgs e)
        {

        }

        private void handDelayButton_Click(object sender, EventArgs e)
        {

        }
    }
}
