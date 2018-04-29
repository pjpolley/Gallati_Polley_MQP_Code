using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sender
{
    class PatsControlScheme : ControlSystemInterface
    {
        public int timeNeededForChange = 10000;//in milliseconds
        public Node root = null;
        public Dictionary<int, Node> allNodes = new Dictionary<int, Node>();
        //max value is 9 due to indexing implementation
        public int childrenPerNode = 1;//default value

        public void DetermineSetpointsFromInputs()
        {

        }

        public void PushInformationToHand()
        {

        }

        public void Initialize()
        {
            if (!GetDataFromFile())
            {
                instantiateNewTree(1, 10000);
                childrenPerNode = 1;
            }
            else
            {
                if (root.children.Count > 0)
                {
                    childrenPerNode = root.children.Count - 1;
                }
                else
                {
                    childrenPerNode = 1;
                }
            }
        }

        public bool GetDataFromFile()
        {
            if (File.Exists(Globals.TreeSaveLocation))
            {
                string inputData = File.ReadAllText(Globals.TreeSaveLocation);
                List<Node> retrievedNodes = null;
                try
                {
                    retrievedNodes = JsonConvert.DeserializeObject<List<Node>>(inputData);
                }
                catch (Exception e)
                {
                    return false;
                }

                if (retrievedNodes == null)
                {
                    return false;
                }
                for(int i = 0; i < retrievedNodes.Count; i++)
                {
                    if(retrievedNodes[i].name == null || ( (retrievedNodes[i].id < 0 || retrievedNodes[i].getHandPosition() == null) && retrievedNodes[i].id != Globals.CONTROLNODE))
                    {
                        //check each node to make sure they saved correctly
                        return false;
                    }
                }
                if (retrievedNodes != null)
                {
                    bool foundRoot = false;
                    foreach (Node n in retrievedNodes)
                    {
                        allNodes.Add(n.id, n);
                        if (!foundRoot && n.parent == Globals.NULLPARENT)
                        {
                            root = n;
                            foundRoot = true;
                        }
                        if (n.id != Globals.CONTROLNODE && !n.children.Contains(Globals.CONTROLNODE) && n.children.Count > 0)
                        {
                            n.children.Add(Globals.CONTROLNODE);
                        }
                    }
                    foreach(Node n in allNodes.Values)
                    {
                        if (n.id != Globals.CONTROLNODE && n.children.Contains(Globals.CONTROLNODE))
                        {
                            n.children.Remove(Globals.CONTROLNODE);
                            n.children.Add(Globals.CONTROLNODE);
                        }
                    }
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public void instantiateNewTree(int positionsPerSplit, int delay)
        {
            this.allNodes.Clear();
            this.root = createNewNode(Globals.ROOTNODE, positionsPerSplit, Globals.NULLPARENT);
            this.childrenPerNode = positionsPerSplit;
            
            this.timeNeededForChange = delay;
        }

        public Node createNewNode(int id, int positionsPerSplit, int parentID)
        {
            Node newNode = new Node("Extended Hand", new SetPoint(), id, new List<int>(positionsPerSplit), parentID);
            if (id != Globals.CONTROLNODE)
            {
                this.allNodes.Add(newNode.id, newNode);
            }
            if(parentID != Globals.NULLPARENT && id != Globals.CONTROLNODE)
            {
                allNodes[parentID].children.Add(id);
            }
            return newNode;
        }

        public void pushDataToFile()
        {
            List<Node> listOfNodes = allNodes.Values.ToList<Node>();
            string output = JsonConvert.SerializeObject(listOfNodes);
            using (StreamWriter sw = new StreamWriter(Globals.TreeSaveLocation))
            {
                sw.WriteLine(output);
            }
        }

        public void cleanupReferences()
        {
            List<Node> everyNode = allNodes.Values.ToList().OrderBy(k => k.id).ToList();
            foreach (Node n in allNodes.Values)
            {
                if(!everyNode.Exists(node => node.parent == n.parent)){
                    allNodes.Remove(n.id);
                }
            }
        }
    }
}
