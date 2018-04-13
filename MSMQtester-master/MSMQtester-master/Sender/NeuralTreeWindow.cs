using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Sender
{
    public partial class NeuralTreeWindow : Form
    {
        TreeNode activeNode = null;

        PatsControlScheme controls;

        private int fingerSelected = Globals.THUMB;
        private int jointSelected = Globals.OUTERJOINT;

        public NeuralTreeWindow()
        {
            this.FormClosing += Globals.CloseAllForms;
            InitializeComponent();

            updateFingerDisplay();

            UnityCommunicationHub.InitializeUnityCommunication();

            //initialize tree structure
            controls = new PatsControlScheme();
            controls.Initialize();

            loadPositions(controls.root);

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
                    //iterate through until you find the parent
                    TreeNode parentNode = findNodeInTree(parentID);
                    //once we find the parent, add it to the parent's children
                    TreeNode newNode = new TreeNode(n.name);
                    newNode.Tag = n.id;
                    parentNode.Nodes.Add(newNode);
                }
            }
        }

        private void NeuronTreeView_NodeClicked(object sender, TreeNodeMouseClickEventArgs e)
        {
            activeNode = e.Node;
            Console.WriteLine(e.Node.Text + " Clicked");
            Console.WriteLine("Tag is: " + e.Node.Tag);
            Node thisNode = controls.allNodes[(int)e.Node.Tag];
            loadPositions(thisNode);
        }

        private void loadPositions(Node thisNode)
        {
            Globals.A1DesiredPosition = thisNode.A1Position;
            Globals.A2DesiredPosition = thisNode.A2Position;
            Globals.A3DesiredPosition = thisNode.A3Position;
            Globals.B1DesiredPosition = thisNode.B1Position;
            Globals.B2DesiredPosition = thisNode.B2Position;
            Globals.B3DesiredPosition = thisNode.B3Position;
            Globals.C1DesiredPosition = thisNode.C1Position;
            Globals.C2DesiredPosition = thisNode.C2Position;
            Globals.C3DesiredPosition = thisNode.C3Position;
            Globals.D1DesiredPosition = thisNode.D1Position;
            Globals.D2DesiredPosition = thisNode.D2Position;
            Globals.D3DesiredPosition = thisNode.D3Position;
            Globals.T1DesiredPosition = thisNode.T1Position;
            Globals.T2DesiredPosition = thisNode.T2Position;
            UnityCommunicationHub.TwoWayTransmission();
        }

        private void setHandPositionButton_Click(object sender, EventArgs e)
        {
            UnityCommunicationHub.ReadData(true);
            SetPoint newPoint = new SetPoint
            {
                A1Position = Globals.A1DesiredPosition,
                A2Position = Globals.A2DesiredPosition,
                A3Position = Globals.A3DesiredPosition,
                B1Position = Globals.B1DesiredPosition,
                B2Position = Globals.B2DesiredPosition,
                B3Position = Globals.B3DesiredPosition,
                C1Position = Globals.C1DesiredPosition,
                C2Position = Globals.C2DesiredPosition,
                C3Position = Globals.C3DesiredPosition,
                D1Position = Globals.D1DesiredPosition,
                D2Position = Globals.D2DesiredPosition,
                D3Position = Globals.D3DesiredPosition,
                T1Position = Globals.T1DesiredPosition,
                T2Position = Globals.T2DesiredPosition
            };
            controls.allNodes[(int)activeNode.Tag].setHandPosition(newPoint);
        }

        private void AddAnotherLayerButton_Click(object sender, EventArgs e)
        {
            Node thisNode = controls.allNodes[(int)activeNode.Tag];
            if(thisNode.children.Count != 0)
            {
                return;//node already has children. do nothing
            }
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
            activeNode.Text = desiredNameBox.Text;
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
                    while (n.Nodes.Count < newChildrenPerNode)
                    {
                        //calculate the id for the new node
                        int newID = ((int)n.Tag * 10) + n.Nodes.Count + 1;
                        //create a new node in the backend
                        controls.createNewNode(newID, controls.childrenPerNode, (int)n.Tag);
                        //and then add it to the frontend
                        TreeNode newDisplayNode = new TreeNode(controls.allNodes[newID].name);
                        newDisplayNode.Tag = controls.allNodes[newID].id;
                        n.Nodes.Add(newDisplayNode);
                    }
                }
                controls.childrenPerNode = newChildrenPerNode;
            }
            else
            {
                //set up the list to populate with all display nodes
                //order by index from highest to lowest. indexes are calculated so that lower nodes are higher in the tree
                List<Node> newList = controls.allNodes.Values.OrderByDescending(o => o.id).ToList();

                foreach (Node n in newList)
                {
                    //figure out what the id is
                    int ID = n.id;

                    //and delete nodes as its children until it fits
                    while(n.children.Count > newChildrenPerNode)
                    {
                        int idOfChildToDestroy = n.children.Last<int>();
                        TreeNode frontEndNode = findNodeInTree(idOfChildToDestroy);
                        frontEndNode.Remove();
                        int idOfParent = controls.allNodes[idOfChildToDestroy].parent;
                        controls.allNodes[idOfParent].children.Remove(idOfChildToDestroy);
                        controls.allNodes.Remove(idOfChildToDestroy);

                    }
                    
                }
                controls.childrenPerNode = newChildrenPerNode;
            }
        }

        private TreeNode findNodeInTree(int id)
        {
            TreeNode root = NeuronTreeView.Nodes[0];
            if((int)root.Tag == id)
            {
                return root;
            }
            else
            {
                foreach(TreeNode n in root.Nodes)
                {
                    TreeNode checkedNode = recursiveFindNode(n, id);
                    if(checkedNode != null)
                    {
                        return checkedNode;
                    }
                }
                return null;
            }
        }

        private TreeNode recursiveFindNode(TreeNode n, int id)
        {
            if ((int)n.Tag == id)
            {
                return n;
            }
            else
            {
                TreeNode foundNode;
                foreach (TreeNode child in n.Nodes) {
                    foundNode = recursiveFindNode(child, id);
                    if(foundNode != null)
                    {
                        return child;
                    }
                }
                return null;
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

        private void ThumbSelectButton_Click(object sender, EventArgs e)
        {
            fingerSelected = Globals.THUMB;
            InnerJointButton.Hide();
            if(jointSelected == Globals.INNERJOINT)
            {
                jointSelected = Globals.MIDDLEJOINT;
            }
            updateFingerDisplay();
        }

        private void IndexSelectButton_Click(object sender, EventArgs e)
        {
            fingerSelected = Globals.POINTER;
            updateFingerDisplay();
            InnerJointButton.Show();
        }

        private void MiddleSelectButton_Click(object sender, EventArgs e)
        {
            fingerSelected = Globals.MIDDLE;
            updateFingerDisplay();
            InnerJointButton.Show();
        }

        private void RingSelectButton_Click(object sender, EventArgs e)
        {
            fingerSelected = Globals.RING;
            updateFingerDisplay();
            InnerJointButton.Show();
        }

        private void PinkySelectButton_Click(object sender, EventArgs e)
        {
            fingerSelected = Globals.PINKY;
            InnerJointButton.Hide();
            if (jointSelected == Globals.INNERJOINT)
            {
                jointSelected = Globals.MIDDLEJOINT;
            }
            updateFingerDisplay();
        }

        private void OuterJointButton_Click(object sender, EventArgs e)
        {
            jointSelected = Globals.OUTERJOINT;
            updateFingerDisplay();
        }

        private void MiddleJointButton_Click(object sender, EventArgs e)
        {
            jointSelected = Globals.MIDDLEJOINT;
            updateFingerDisplay();
        }

        private void InnerJointButton_Click(object sender, EventArgs e)
        {
            jointSelected = Globals.INNERJOINT;
            updateFingerDisplay();
        }

        private void updateFingerDisplay()
        {
            currentlyModifyingBox.Text = "Modifying " + Globals.valuesToStrings[fingerSelected] + " " + Globals.valuesToStrings[jointSelected];

            if (fingerSelected == Globals.THUMB)
            {
                if (jointSelected == Globals.OUTERJOINT)
                {
                    DesiredAngleInput.Text = Globals.T2DesiredPosition.ToString();
                }
                else if (jointSelected == Globals.MIDDLEJOINT)
                {
                    DesiredAngleInput.Text = Globals.T1DesiredPosition.ToString();
                }
            }
            else if (fingerSelected == Globals.POINTER)
            {
                if (jointSelected == Globals.OUTERJOINT)
                {
                    DesiredAngleInput.Text = Globals.A3DesiredPosition.ToString();
                }
                else if (jointSelected == Globals.MIDDLEJOINT)
                {
                    DesiredAngleInput.Text = Globals.A2DesiredPosition.ToString();
                }
                else if (jointSelected == Globals.INNERJOINT)
                {
                    DesiredAngleInput.Text = Globals.A1DesiredPosition.ToString();
                }
            }
            else if (fingerSelected == Globals.MIDDLE)
            {
                if (jointSelected == Globals.OUTERJOINT)
                {
                    DesiredAngleInput.Text = Globals.B3DesiredPosition.ToString();
                }
                else if (jointSelected == Globals.MIDDLEJOINT)
                {
                    DesiredAngleInput.Text = Globals.B2DesiredPosition.ToString();
                }
                else if (jointSelected == Globals.INNERJOINT)
                {
                    DesiredAngleInput.Text = Globals.B1DesiredPosition.ToString();
                }
            }
            else if (fingerSelected == Globals.RING)
            {
                if (jointSelected == Globals.OUTERJOINT)
                {
                    DesiredAngleInput.Text = Globals.C3DesiredPosition.ToString();
                }
                else if (jointSelected == Globals.MIDDLEJOINT)
                {
                    DesiredAngleInput.Text = Globals.C2DesiredPosition.ToString();
                }
                else if (jointSelected == Globals.INNERJOINT)
                {
                    DesiredAngleInput.Text = Globals.C1DesiredPosition.ToString();
                }
            }
            else if (fingerSelected == Globals.PINKY)
            {
                if (jointSelected == Globals.OUTERJOINT)
                {
                    DesiredAngleInput.Text = Globals.D2DesiredPosition.ToString();
                }
                else if (jointSelected == Globals.MIDDLEJOINT)
                {
                    DesiredAngleInput.Text = Globals.D1DesiredPosition.ToString();
                }
            }
        }

        private void setDesiredAngle()
        {
            float desiredAngle = (float)System.Convert.ToDouble(DesiredAngleInput.Text);
            if (fingerSelected == Globals.THUMB)
            {
                if (jointSelected == Globals.OUTERJOINT)
                {
                    Globals.T2DesiredPosition = desiredAngle;
                }
                else if (jointSelected == Globals.MIDDLEJOINT)
                {
                    Globals.T1DesiredPosition = desiredAngle;
                }
            }
            else if (fingerSelected == Globals.POINTER)
            {
                if (jointSelected == Globals.OUTERJOINT)
                {
                    Globals.A3DesiredPosition = desiredAngle;
                }
                else if (jointSelected == Globals.MIDDLEJOINT)
                {
                    Globals.A2DesiredPosition = desiredAngle;
                }
                else if (jointSelected == Globals.INNERJOINT)
                {
                    Globals.A1DesiredPosition = desiredAngle;
                }
            }
            else if (fingerSelected == Globals.MIDDLE)
            {
                if (jointSelected == Globals.OUTERJOINT)
                {
                    Globals.B3DesiredPosition = desiredAngle;
                }
                else if (jointSelected == Globals.MIDDLEJOINT)
                {
                    Globals.B2DesiredPosition = desiredAngle;
                }
                else if (jointSelected == Globals.INNERJOINT)
                {
                    Globals.B1DesiredPosition = desiredAngle;
                }
            }
            else if (fingerSelected == Globals.RING)
            {
                if (jointSelected == Globals.OUTERJOINT)
                {
                    Globals.C3DesiredPosition = desiredAngle;
                }
                else if (jointSelected == Globals.MIDDLEJOINT)
                {
                    Globals.C2DesiredPosition = desiredAngle;
                }
                else if (jointSelected == Globals.INNERJOINT)
                {
                    Globals.C1DesiredPosition = desiredAngle;
                }
            }
            else if (fingerSelected == Globals.PINKY)
            {
                if (jointSelected == Globals.OUTERJOINT)
                {
                    Globals.D2DesiredPosition = desiredAngle;
                }
                else if (jointSelected == Globals.MIDDLEJOINT)
                {
                    Globals.D1DesiredPosition = desiredAngle;
                }
            }
        }

        private void IncreaseAngleButton_Click(object sender, EventArgs e)
        {
            float desiredAngle = (float)System.Convert.ToDouble(DesiredAngleInput.Text) + 1.0f;
            DesiredAngleInput.Text = desiredAngle.ToString();
            setDesiredAngle();
            UnityCommunicationHub.TwoWayTransmission();
        }

        private void DecreaseAngleButton_Click(object sender, EventArgs e)
        {
            float desiredAngle = (float)System.Convert.ToDouble(DesiredAngleInput.Text) - 1.0f;
            DesiredAngleInput.Text = desiredAngle.ToString();
            setDesiredAngle();
            UnityCommunicationHub.TwoWayTransmission();
        }

        private void DesiredAngleInput_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                setDesiredAngle();
                UnityCommunicationHub.TwoWayTransmission();
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        volatile bool continueControlling = true;

        private void beginControllingHandButton_Click(object sender, EventArgs e)
        {
            SerialReader reader = new SerialReader();
            continueControlling = true;
            Node currentNode = controls.root;
            int rate = reader.getRate();
            int desiredMillisecondDelay = controls.timeNeededForChange;
            //int arraySize = (desiredMillisecondDelay / 1000) * rate;
            reader.Read();

            double lowConcentration;
            double highConcentration;

            //first get threshholds
            Stopwatch timer = new Stopwatch();
            int reads = 0;
            decimal allReads = 0;
            MessageBox.Show("First try to let your mind wander until the next popup appears. Hit OK when ready.", string.Empty, MessageBoxButtons.OK);
            timer.Start();
            while (timer.ElapsedMilliseconds < Globals.threshholdAquisitionTime)
            {
                allReads += (decimal)reader.GetData()[Globals.inputNode];
                reads++;
            }
            timer.Reset();
            lowConcentration = (double)(allReads / reads);

            reads = 0;
            allReads = 0;

            MessageBox.Show("Good. Next try to focus as hard as possible something. Hit OK when ready.", string.Empty, MessageBoxButtons.OK);
            timer.Start();
            while (timer.ElapsedMilliseconds < Globals.threshholdAquisitionTime)
            {
                allReads += (decimal)reader.GetData()[Globals.inputNode];
                reads++;
            }
            timer.Reset();
            highConcentration = (double)(allReads / reads);

            double differenceInConcentrations = highConcentration - lowConcentration;
            double deltaBetweenThreshholds = differenceInConcentrations / controls.childrenPerNode;

            //make ranges for this run
            List<Threshhold> ranges = new List<Threshhold>(controls.childrenPerNode);

            for(int i = 0; i < controls.childrenPerNode; i++)
            {
                if(i == 0)
                {
                    //make sure all reads work for it
                    ranges[i] = new Threshhold(Double.MinValue, lowConcentration + ((i + 1) * deltaBetweenThreshholds));
                }
                else if (i == controls.childrenPerNode - 1)
                {
                    ranges[i] = new Threshhold(lowConcentration + (i * deltaBetweenThreshholds), Double.MaxValue);
                }
                else
                {
                    ranges[i] = new Threshhold(lowConcentration + (i * deltaBetweenThreshholds), lowConcentration + ((i + 1) * deltaBetweenThreshholds));
                }
            }

            decimal accruedValues = 0;
            long numInputs = 0;
            bool foundNextPosition = false;
            Node desiredNode = controls.root;

            MessageBox.Show("Ready to control hand. Press OK when ready.", string.Empty, MessageBoxButtons.OK);

            while (continueControlling)
            {
                //get the inputs and average them for the desired output
                timer.Start();
                while (timer.ElapsedMilliseconds < desiredMillisecondDelay)
                {
                    accruedValues += (decimal)reader.GetData()[Globals.inputNode];
                    numInputs++;

                }
                double averageInput = (double)(accruedValues / numInputs);

                //now get the next node to go to
                for (int i = 0; i < controls.childrenPerNode && !foundNextPosition; i++)
                {
                    if (ranges[i].Contains(averageInput))
                    {
                        foundNextPosition = true;
                        desiredNode = controls.allNodes[currentNode.children[i]];
                    }
                }

                currentNode = desiredNode;

                loadPositions(currentNode);

                timer.Reset();
                numInputs = 0;
                accruedValues = 0;
            }
        }
    }
}
