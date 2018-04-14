using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sender
{
    public static class Globals
    {
        //Place to save tree data
        public static string TreeSaveLocation = @"c:\BCIDataDirectory\NeuralTree.txt";
        public static string DefaultHandPositionsLocation = @"c:\BCIDataDirectory\DefaultHandPositions.txt";
        public static string NeuralNetSaveLocation = @"c:\BCIDataDirectory\NeuralNet.ann";
        public static string KFoldDataSaveLocation = @"c:\BCIDataDirectory\KFoldData.kfld";


        //frequency of transmission from BCI chip to this program
        public static float transmissionRate;

        //form constants for efficient loading
        public static Form welcomeScreen = null;
        public static Form treeScreen = null;
        public static Form netScreen = null;
        public static Form form1 = null;
        public static Form form2 = null;

        //time allocated to attempt to connect to unity
        public static int TimeToConnectToUnity = 10000;//10 seconds

        //data read in from the BCI board
        public static float input1 = 0;
        public static float input2 = 0;
        public static float input3 = 0;
        public static float input4 = 0;
        public static float input5 = 0;
        public static float input6 = 0;
        public static float input7 = 0;
        public static float input8 = 0;

        //feedback data from the hand
        public static float T1DesiredPosition = 0;
        public static float T2DesiredPosition = 0;
        public static float A1DesiredPosition = 0;
        public static float A2DesiredPosition = 0;
        public static float A3DesiredPosition = 0;
        public static float B1DesiredPosition = 0;
        public static float B2DesiredPosition = 0;
        public static float B3DesiredPosition = 0;
        public static float C1DesiredPosition = 0;
        public static float C2DesiredPosition = 0;
        public static float C3DesiredPosition = 0;
        public static float D1DesiredPosition = 0;
        public static float D2DesiredPosition = 0;
        public static float D3DesiredPosition = 0;

        //feedback data from the hand
        public static float T1ActualPosition = 0;
        public static float T2ActualPosition = 0;
        public static float A1ActualPosition = 0;
        public static float A2ActualPosition = 0;
        public static float A3ActualPosition = 0;
        public static float B1ActualPosition = 0;
        public static float B2ActualPosition = 0;
        public static float B3ActualPosition = 0;
        public static float C1ActualPosition = 0;
        public static float C2ActualPosition = 0;
        public static float C3ActualPosition = 0;
        public static float D1ActualPosition = 0;
        public static float D2ActualPosition = 0;
        public static float D3ActualPosition = 0;

        //defines for the neural tree
        public static int ROOTNODE = 0;
        public static int NULLPARENT = -1;
        public static int CONTROLNODE = -10;

        //defines for the neural tree
        public static int THUMB = 0;
        public static int POINTER = 1;
        public static int MIDDLE = 2;
        public static int RING = 3;
        public static int PINKY = 4;
        public static int OUTERJOINT = 10;
        public static int MIDDLEJOINT = 11;
        public static int INNERJOINT = 12;
        public static Dictionary<int, string> valuesToStrings = new Dictionary<int, string>()
        {
            {THUMB, "Thumb"},
            {POINTER, "Index Finger"},
            {MIDDLE, "Middle Finger"},
            {RING, "Ring Finger"},
            {PINKY, "Pinky"},
            {OUTERJOINT, "outer joint"},
            {INNERJOINT, "inner joint"},
            {MIDDLEJOINT, "middle joint"},
        };

        //Six Basic Hand Positions
        public static SetPoint HOOK_GRIP = new SetPoint(90,90,90,90,90,90,90,90,90,90,90,90,90,90);
        public static SetPoint OPEN_HAND = new SetPoint(0,0,0,0,0,0,0,0,0,0,0,0,0,0);
        public static SetPoint PEACE_SIGN = new SetPoint(90,90,0,0,0,0,0,0,90,90,90,90,90,90);
        public static SetPoint PINCH_GRIP = new SetPoint(20,90, 20, 90, 45, 0,0,0,0,0,0,0,0,0);
        public static SetPoint THUMBS_UP = new SetPoint(0,0,90,90,90,90,90,90,90,90,90,90,90,90);
        public static SetPoint FINGER_GUNS = new SetPoint(0,0,0,0,0,90,90,90,90,90,90,90,90,90);

        public static Dictionary<string, double[]> GetBasicValues()
        {
            var returnList = new Dictionary<string, double[]>
            {
                { "Hook Grip", new double[6]{1,0,0,0,0,0} },
                { "Open Hand", new double[6]{0,1,0,0,0,0} },
                { "Peace Sign", new double[6]{0,0,1,0,0,0} },
                { "Pinch Grip", new double[6]{0,0,0,1,0,0} },
                { "Thumbs Up", new double[6]{0,0,0,0,1,0} },
                { "Finger Guns", new double[6]{0,0,0,0,0,1} }
            };
            return returnList;
        }

        public static Dictionary<double[], SetPoint> GetBasicPositions()
        {
            var returnList = new Dictionary<double[], SetPoint>
            {
                { new double[6]{1,0,0,0,0,0}, HOOK_GRIP },
                { new double[6]{0,1,0,0,0,0}, OPEN_HAND },
                { new double[6]{0,0,1,0,0,0}, PEACE_SIGN },
                { new double[6]{0,0,0,1,0,0}, PINCH_GRIP },
                { new double[6]{0,0,0,0,1,0}, THUMBS_UP },
                { new double[6]{0,0,0,0,0,1}, FINGER_GUNS }
            };
            return returnList;
        }

        //constants for the command tree
        public static int threshholdAquisitionTime = 5000;//5 seconds to get the limits
        public static int inputNode = 2;

        //exits the application
        public static void CloseAllForms(object sender, FormClosingEventArgs e)
        {
            UnityCommunicationHub.PurgeFileSystem();
            Application.Exit();
        }
    }
}
