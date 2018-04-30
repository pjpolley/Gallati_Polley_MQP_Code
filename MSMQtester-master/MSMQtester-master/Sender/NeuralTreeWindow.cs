using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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

        volatile bool continueControlling = true;
        long numInputs = 0;
        TreeNode lastNode = null;
        Stopwatch timer = new Stopwatch();
        Node currentNode;
        SerialReader reader = new SerialReader();
        int rate;

        public NeuralTreeWindow()
        {
            this.Size = new System.Drawing.Size(1710, 1301);
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
                if (n.id != Globals.CONTROLNODE)
                {
                    //figure out what the parent id is
                    int parentID = n.parent;
                    //if it's the root node, just add it to the tree
                    if (parentID == Globals.NULLPARENT)
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

            foreach (Node n in newList)
            {
                if (n.id != Globals.CONTROLNODE && n.children.Contains(Globals.CONTROLNODE))
                {
                    TreeNode tn = findNodeInTree(n.id);
                    bool done = false;
                    foreach (TreeNode tnn in tn.Nodes)
                    {
                        if (!done && (int)tnn.Tag == Globals.CONTROLNODE)
                        {
                            tnn.Remove();
                            done = true;
                        }
                    }
                    TreeNode newControlNode = new TreeNode("Controls");
                    newControlNode.Tag = Globals.CONTROLNODE;
                    tn.Nodes.Add(newControlNode);
                }
            }
        }

        private void NeuronTreeView_NodeClicked(object sender, TreeNodeMouseClickEventArgs e)
        {
            Console.WriteLine(e.Node.Text + " Clicked");
            Console.WriteLine("Tag is: " + e.Node.Tag);
            if ((int)e.Node.Tag != Globals.CONTROLNODE)
            {
                activeNode = e.Node;
                Node thisNode = controls.allNodes[(int)e.Node.Tag];
                loadPositions(thisNode);
                Console.WriteLine("Not a control node");
            }
            else
            {
                Console.WriteLine("A control node");
            }
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
            controls.allNodes[(int)activeNode.Tag].setHandPosition(newPoint);       }

        private void AddAnotherLayerButton_Click(object sender, EventArgs e)
        {
            Node thisNode = controls.allNodes[(int)activeNode.Tag];
            if (thisNode.children.Count != 0)
            {
                return;//node already has children. do nothing
            }
            for (int i = 0; i < controls.childrenPerNode; i++)
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
            controls.allNodes[thisNode.id].children.Add(Globals.CONTROLNODE);
            TreeNode newControlNode = new TreeNode("Controls");
            newControlNode.Tag = Globals.CONTROLNODE;
            activeNode.Nodes.Add(newControlNode);
        }

        private void removeLayerButton_Click(object sender, EventArgs e)
        {
            Node thisNode = controls.allNodes[(int)activeNode.Tag];
            List<int> children = thisNode.children;
            foreach (int i in children)
            {
                if (i != Globals.CONTROLNODE)
                {
                    controls.allNodes.Remove(i);
                }
            }
            activeNode.Nodes.Clear();
            thisNode.children.Clear();
        }

        private void changeNameButton_Click(object sender, EventArgs e)
        {
            activeNode.Text = desiredNameBox.Text;
            controls.allNodes[(int)activeNode.Tag].name = desiredNameBox.Text;
        }

        private void purgeChildren(TreeNode node)
        {
            if (node != null && (int)node.Tag != Globals.CONTROLNODE)
            {
                foreach (TreeNode n in node.Nodes)
                {
                    purgeChildren(n);
                }
                node.Remove();
                controls.allNodes.Remove((int)node.Tag);
            }
            else
            {
                //the controls option
                if (node != null)
                {
                    node.Remove();
                }
            }
        }

        private TreeNode findNodeInTree(int id)
        {
            if (id == Globals.CONTROLNODE)
            {
                return null;
            }
            TreeNode root = NeuronTreeView.Nodes[0];
            if ((int)root.Tag == id)
            {
                return root;
            }
            else
            {
                foreach (TreeNode n in root.Nodes)
                {
                    TreeNode checkedNode = recursiveFindNode(n, id);
                    if (checkedNode != null)
                    {
                        return checkedNode;
                    }
                }
                return null;
            }
        }

        private TreeNode recursiveFindNode(TreeNode n, int id)
        {
            if (id == Globals.CONTROLNODE)
            {
                return null;
            }
            if ((int)n.Tag == id)
            {
                return n;
            }
            else
            {
                TreeNode foundNode;
                foreach (TreeNode child in n.Nodes)
                {
                    foundNode = recursiveFindNode(child, id);
                    if (foundNode != null)
                    {
                        return foundNode;
                    }
                }
                return null;
            }
        }

        private void handDelayButton_Click(object sender, EventArgs e)
        {
            controls.timeNeededForChange = System.Convert.ToInt32(handDelayBox.Text);
        }

        private void saveCommandStructure_Click(object sender, EventArgs e)
        {
            controls.pushDataToFile();
        }

        private void hardResetButton_Click(object sender, EventArgs e)
        {
            //purge all nodes
            NeuronTreeView.Nodes.Clear();

            //and reset the tree
            controls.instantiateNewTree(1, System.Convert.ToInt32(handDelayBox.Text));
            TreeNode newNode = new TreeNode(controls.root.name);
            newNode.Tag = controls.root.id;
            NeuronTreeView.Nodes.Add(newNode);
            NeuronTreeView.ExpandAll();
        }

        private void ThumbSelectButton_Click(object sender, EventArgs e)
        {
            fingerSelected = Globals.THUMB;
            InnerJointButton.Hide();
            if (jointSelected == Globals.INNERJOINT)
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

        private double configuredMidPoint;

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
            if (e.KeyCode == Keys.Enter)
            {
                setDesiredAngle();
                UnityCommunicationHub.TwoWayTransmission();
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void beginControllingHandButton_Click(object sender, EventArgs e)
        {
            int desiredMillisecondDelay = controls.timeNeededForChange;
            int arraySize = (desiredMillisecondDelay / 1000) * rate;
            reader.Read();
            rate = reader.getRate();

            double lowConcentration = 0;
            double highConcentration = 0;

            //first get threshholds
            bool done = false;
            while (!done)
            {

                int reads = 0;
                decimal allReads = 0;
                MessageBox.Show("First try to let your mind wander until the next popup appears. Hit OK when ready.", string.Empty, MessageBoxButtons.OK);
                timer.Start();
                while (timer.ElapsedMilliseconds < Globals.threshholdAquisitionTime)
                {
                    decimal currentIn = (decimal)Math.Abs(reader.GetData()[Globals.inputNode]);
                    allReads += currentIn;
                    reads++;
                    currentAverageBox.Text = (allReads / reads).ToString();
                    currentInputBox.Text = currentIn.ToString();
                }

                timer.Reset();
                lowConcentration = (double)(allReads / reads);
                midpointTextBox.Text = lowConcentration.ToString();
                Console.WriteLine("Low concentration was: " + lowConcentration);

                reads = 0;
                allReads = 0;

                MessageBox.Show("Next try to focus as hard as possible something. Hit OK when ready.", string.Empty, MessageBoxButtons.OK);

                timer.Start();
                while (timer.ElapsedMilliseconds < Globals.threshholdAquisitionTime)
                {
                    decimal currentIn = (decimal)Math.Abs(reader.GetData()[Globals.inputNode]);
                    allReads += currentIn;
                    reads++;
                    currentAverageBox.Text = (allReads / reads).ToString();
                    currentInputBox.Text = currentIn.ToString();
                }
                timer.Reset();

                highConcentration = (double)(allReads / reads);
                configuredMidPoint = ((highConcentration + lowConcentration) / 2);
                midpointTextBox.Text = configuredMidPoint.ToString();

                Console.WriteLine("High concentration was: " + highConcentration);

                if (highConcentration > lowConcentration)
                {
                    done = true;
                }
            }

            MessageBox.Show("Ready to control hand. Press OK when ready.", string.Empty, MessageBoxButtons.OK);
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            continueControlling = false;
        }

        private void iterateButton_Click(object sender, EventArgs e)
        {
            runTree();
        }

        private void runTree()
        {
            continueControlling = true;
            currentNode = controls.root;
            int desiredMillisecondDelay = controls.timeNeededForChange;

            TreeNode n = findNodeInTree(currentNode.id);
            n.BackColor = Color.Yellow;
            lastNode = n;

            timer = new Stopwatch();
            while (continueControlling)
            {
                //get the inputs and average them for the desired output
                double averageInput = 0;

                if (currentNode.children.Count < 1)
                {
                    continueControlling = false;
                    loadPositions(currentNode);
                    break;
                }
                Random rand = new Random();
                numInputs = 0;
                decimal accruedValues = 0;
                timer.Reset();
                timer.Start();
                decimal currentIn;
                while (timer.ElapsedMilliseconds < desiredMillisecondDelay)
                {
                    currentIn = (decimal)Math.Abs(reader.GetData()[Globals.inputNode]);
                    accruedValues += currentIn;
                    numInputs++;
                    currentAverageBox.Text = (accruedValues / numInputs).ToString();
                    timeLeftBox.Text = (desiredMillisecondDelay - timer.ElapsedMilliseconds).ToString();
                    currentInputBox.Text = currentIn.ToString();
                }

                averageInput = (double)(accruedValues / numInputs);
                currentAverageBox.Text = averageInput.ToString();

                if (averageInput >= configuredMidPoint)
                {
                    //set current hand position as the one to move to
                    continueControlling = false;
                    timeLeftBox.Text = "Done";
                }
                else
                {
                    continueControlling = true;
                    currentNode = controls.allNodes[currentNode.children.First()];
                }

                n = findNodeInTree(currentNode.id);
                n.BackColor = Color.Yellow;

                lastNode.BackColor = Color.White;
                lastNode = n;

                Console.WriteLine(averageInput);

                numInputs = 0;
                accruedValues = 0;

                Console.WriteLine("Desired was " + currentNode.name);

                if (currentNode.id == Globals.CONTROLNODE)
                {
                    continueControlling = false;
                    loadPositions(currentNode);
                }
                else
                {
                    if (!continueControlling)
                    {
                        loadPositions(currentNode);
                    }
                }
            }
        }

        private void handDelayBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                controls.timeNeededForChange = System.Convert.ToInt32(handDelayBox);
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void desiredNameBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                activeNode.Text = desiredNameBox.Text;
                controls.allNodes[(int)activeNode.Tag].name = desiredNameBox.Text;
                e.Handled = e.SuppressKeyPress = true;
            }
        }
    }
}
