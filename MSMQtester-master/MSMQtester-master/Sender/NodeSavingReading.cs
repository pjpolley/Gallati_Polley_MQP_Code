using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sender
{
    public class NodeSavingReading
    {

        public void pushDataToFile(String fileLocation, List<Node> nodesToSave)
        {
            List<Node> listOfNodes = nodesToSave.ToList<Node>();
            string output = JsonConvert.SerializeObject(listOfNodes);
            using (StreamWriter sw = new StreamWriter(Globals.TreeSaveLocation))
            {
                sw.WriteLine(output);
            }
        }

        public void pushDataToFile(String fileLocation, List<SetPoint> pointsToSave)
        {
            List<SetPoint> listofSetPoints = pointsToSave.ToList<SetPoint>();
            string output = JsonConvert.SerializeObject(listofSetPoints);
            using (StreamWriter sw = new StreamWriter(Globals.TreeSaveLocation))
            {
                sw.WriteLine(output);
            }
        }

        public void pushDataToFile(String fileLocation, KFoldData dataPoint)
        {
            string output = JsonConvert.SerializeObject(dataPoint);
            using (StreamWriter sw = new StreamWriter(Globals.TreeSaveLocation))
            {
                sw.WriteLine(output);
            }
        }

        public List<Node> GetDataFromFile(String fileLocation)
        {
            if (File.Exists(fileLocation))
            {
                string inputData = File.ReadAllText(Globals.TreeSaveLocation);
                try
                {
                    return JsonConvert.DeserializeObject<List<Node>>(inputData);
                }
                catch (Exception e)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public List<SetPoint> GetSetPointDataFromFile(String fileLocation)
        {
            if (File.Exists(fileLocation))
            {
                string inputData = File.ReadAllText(Globals.TreeSaveLocation);
                try
                {
                    return JsonConvert.DeserializeObject<List<SetPoint>>(inputData);
                }
                catch (Exception e)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public KFoldData GetKFoldDataFromFile(String fileLocation)
        {
            if (File.Exists(fileLocation))
            {
                string inputData = File.ReadAllText(Globals.TreeSaveLocation);
                try
                {
                    return JsonConvert.DeserializeObject<KFoldData>(inputData);
                }
                catch (Exception e)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
