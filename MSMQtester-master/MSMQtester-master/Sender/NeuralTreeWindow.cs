using System;
using System.Collections.Generic;
using System.Linq;
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

            //initialize tree structure
            controls = new PatsControlScheme();
            controls.Initialize();

            List<Node> newList = controls.allNodes.Values.ToList<Node>();
            //order the list from lowest to highest to ensure all nodes are populated in order
            newList.OrderBy(o => o.id);

            //make sure the list of nodes is clear
            NeuronTreeView.Nodes.Clear();

            //sequentially add all the nodes to the tree
            foreach (Node n in newList)
            {
                //figure out what the parent id is
                int parentID = n.parent;
                //if it's the root node, just add it to the tree
                if(parentID == Globals.NULLPARENT)
                {
                    TreeNode newNode = new TreeNode(n.name);
                    newNode.Tag = n.id;
                    NeuronTreeView.Nodes.Add(newNode);
                    activeNode = newNode;
                }
                //in the case that it's a child node, figure out what display node to add it to
                else
                {
                    int i = 0;
                    bool foundParent = false;
                    //iterate through until you find the parent
                    while (!foundParent)
                    {
                        if ((int)NeuronTreeView.Nodes[i].Tag == parentID)
                        {
                            foundParent = true;
                        }
                        else {
                            i++;
                        }
                    }
                    //once we find the parent, add it to the parent's children
                    TreeNode newNode = new TreeNode(n.name);
                    newNode.Tag = n.id;
                    NeuronTreeView.Nodes[i].Nodes.Add(newNode);
                }
            }
        }

        private void NeuronTreeView_NodeClicked(object sender, TreeNodeMouseClickEventArgs e)
        {
            activeNode = e.Node;
            Console.WriteLine(e.Node.Text + " Clicked");
            Console.WriteLine("Tag is: " + e.Node.Tag);
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
            controls.allNodes[(int)activeNode.Tag].setHandPosition(newPoint);
        }

        private void AddAnotherLayerButton_Click(object sender, EventArgs e)
        {
            Node thisNode = controls.allNodes[(int)activeNode.Tag];
            for(int i = 0; i < controls.childrenPerNode; i++)
            {
                //calculate the id for the new node
                int newID = (thisNode.id * 10) + i + 1;
                //create a new node in the backend
                controls.createNewNode(newID, controls.childrenPerNode, thisNode.id);
                //and then add it to the frontend
                TreeNode newDisplayNode = new TreeNode(controls.allNodes[newID].name);
                newDisplayNode.Tag = controls.allNodes[newID].id;
                activeNode.Nodes.Add(newDisplayNode);
            }
        }

        private void removeLayerButton_Click(object sender, EventArgs e)
        {
            Node thisNode = controls.allNodes[(int)activeNode.Tag];
            List<int> children = thisNode.children;
            foreach(int i in children)
            {
                controls.allNodes.Remove(i);
            }
            activeNode.Nodes.Clear();
        }

        private void changeNameButton_Click(object sender, EventArgs e)
        {
            activeNode.Name = desiredNameBox.Text;
            controls.allNodes[(int)activeNode.Tag].name = desiredNameBox.Text;
        }

        private void setNumberOfPositionsPerLayerButton_Click(object sender, EventArgs e)
        {
            int newChildrenPerNode = System.Convert.ToInt32(positionsPerLayerBox.Text);
            if(newChildrenPerNode == controls.childrenPerNode)
            {
                //nothing required
                return;
            }
            else if(newChildrenPerNode <= 1)
            {
                //shouldn't be able to have only one child per node. if so, the control system doesn't do anything
                Console.WriteLine("ERROR. INPUT CANNOT BE LESS THAN 2");
                return;
            }
            else if(!(newChildrenPerNode <= 9))
            {
                //shouldn't be able to have only one child per node. if so, the control system doesn't do anything
                Console.WriteLine("ERROR. INPUT CANNOT BE GREATER THAN 9");
                return;
            }
            else if (newChildrenPerNode > controls.childrenPerNode)
            {
                foreach(TreeNode n in NeuronTreeView.Nodes)
                {
                    //calculate the id for the new node
                    int newID = ((int)n.Tag * 10) + newChildrenPerNode;
                    //create a new node in the backend
                    controls.createNewNode(newID, controls.childrenPerNode, (int)n.Tag);
                    //and then add it to the frontend
                    TreeNode newDisplayNode = new TreeNode()
                    {
                        Name = controls.allNodes[newID].name,
                        Tag = controls.allNodes[newID].id,
                    };
                    n.Nodes.Add(newDisplayNode);
                }
            }
            else
            {
                //set up the list to populate with all display nodes
                List<TreeNode> allFrontendNodes = new List<TreeNode>();

                //add nodes to list
                foreach (Node n in controls.allNodes.Values)
                {
                    TreeNode newDisplayNode = new TreeNode()
                    {
                        Name = n.name,
                        Tag = n.id,
                    };
                    allFrontendNodes.Add(newDisplayNode);
                }

                //order by index from highest to lowest. indexes are calculated so that lower nodes are higher in the tree
                List<TreeNode> newList = allFrontendNodes.OrderByDescending(o => (int)o.Tag).ToList();

                //sequentially add all the nodes to the tree
                foreach (TreeNode n in newList)
                {
                    //figure out what the id is
                    int ID = (int)n.Tag;

                    //and delete nodes as its children until it fits
                    while(n.Nodes.Count > newChildrenPerNode)
                    {
                        controls.allNodes.Remove((int)n.Nodes[n.Nodes.Count].Tag);
                        n.Nodes.RemoveAt(n.Nodes.Count);
                    }
                    
                }
            }
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
            Console.WriteLine("Test");
            TreeNode newNode = new TreeNode(controls.root.name);
            newNode.Tag = controls.root.id;
            NeuronTreeView.Nodes.Add(newNode);
        }
    }
}
