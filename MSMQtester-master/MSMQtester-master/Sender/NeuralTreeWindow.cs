using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Sender
{
    public partial class NeuralTreeWindow : Form
    {
        TreeNode activeNode = null;

        Node rootNode;

        PatsControlScheme controls;

        public NeuralTreeWindow()
        {
            this.FormClosing += Globals.CloseAllForms;
            InitializeComponent();

            controls = new PatsControlScheme();
            controls.GetDataFromFile();

            TreeNode testNode = new TreeNode("Test");
            Node testOld = new Node(Globals.ROOTNODE, new List<int>(), Globals.NULLPARENT);
            testOld.setHandPosition(new SetPoint());
            testNode.Tag = testOld;
            Node test = (Node)testNode.Tag;
            controls.allNodes.Add(0, test);
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
            UnityCommunicationHub.ReadData(true);
            SetPoint newPoint = new SetPoint
            {
                A1Position = Globals.A1ActualPosition,
                A2Position = Globals.A2ActualPosition,
                A3Position = Globals.A3ActualPosition,
                B1Position = Globals.B1ActualPosition,
                B2Position = Globals.B2ActualPosition,
                B3Position = Globals.B3ActualPosition,
                C1Position = Globals.C1ActualPosition,
                C2Position = Globals.C2ActualPosition,
                C3Position = Globals.C3ActualPosition,
                D1Position = Globals.D1ActualPosition,
                D2Position = Globals.D2ActualPosition,
                D3Position = Globals.D3ActualPosition,
                T1Position = Globals.T1ActualPosition,
                T2Position = Globals.T2ActualPosition
            };
            //activeNode.
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
            controls.timeNeededForChange = System.Convert.ToInt32(handDelayBox);
        }

        private void saveCommandStructure_Click(object sender, EventArgs e)
        {
            controls.pushDataToFile();
        }

        private void hardResetButton_Click(object sender, EventArgs e)
        {
            //purge all nodes
            NeuronTreeView.Nodes.Clear();

            //
            controls.instantiateNewTree(System.Convert.ToInt32(positionsPerLayerBox.Text));
            TreeNode newDisplayNode = new TreeNode()
            {
                Name = controls.root.name,
                Tag = controls.root.id,
            };
            NeuronTreeView.Nodes.Add(newDisplayNode);
        }
    }
}
