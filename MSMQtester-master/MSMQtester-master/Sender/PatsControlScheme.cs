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
        public float timeNeededForChange = 200f;//in milliseconds
        public Node root = null;
        public Dictionary<int, Node> allNodes = new Dictionary<int, Node>();
        //max value is 9 due to indexing implementation
        public int childrenPerNode = 4;//default value

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
                instantiateNewTree(4);
                childrenPerNode = 4;
            }
            else
            {
                if (root.children.Count > 0)
                {
                    childrenPerNode = root.children.Count;
                }
                else
                {
                    childrenPerNode = 4;
                }
            }
        }

        public bool GetDataFromFile()
        {
            if (File.Exists(Globals.TreeSaveLocation))
            {
                string inputData = File.ReadAllText(Globals.TreeSaveLocation);
                try
                {
                    List<Node> retrievedNodes = JsonConvert.DeserializeObject<List<Node>>(inputData);
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
                        }
                    }
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void instantiateNewTree(int positionsPerSplit)
        {
            this.allNodes.Clear();
            this.root = createNewNode(Globals.ROOTNODE, positionsPerSplit, Globals.NULLPARENT);
            this.timeNeededForChange = 200;
            
        }

        public Node createNewNode(int id, int positionsPerSplit, int parentID)
        {
            return new Node("Extended Hand", new SetPoint(), id, new List<int>(positionsPerSplit), parentID);
        }

        private List<Node> iterateThroughTree(Node root)
        {
            List<Node> output = new List<Node>();
            output.Add(root);
            foreach(int n in root.children)
            {
                if (allNodes.ContainsKey(n))
                {
                    output.Concat(iterateThroughTree(allNodes[n]));
                }
            }
            return output;
        }

        public void pushDataToFile()
        {
            List<Node> allNodes = iterateThroughTree(root);
            string output = JsonConvert.SerializeObject(allNodes);
            using (StreamWriter sw = new StreamWriter(Globals.TreeSaveLocation))
            {
                sw.WriteLine(output);
            }
        }
    }
}
