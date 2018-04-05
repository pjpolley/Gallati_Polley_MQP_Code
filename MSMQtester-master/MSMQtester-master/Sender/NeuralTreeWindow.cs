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
        public NeuralTreeWindow()
        {
            this.FormClosing += Globals.CloseAllForms;

            InitializeComponent();

            TreeNode testNode = new TreeNode("Test");
            testNode.Nodes.Add("Test child");
            NeuronTreeView.Nodes.Add(testNode);
        }

        private void NeuronTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void NeuronTreeView_NodeClicked(object sender, TreeNodeMouseClickEventArgs e)
        {
            Console.WriteLine(e.Node.Text + " Clicked");
        }
    }
}
