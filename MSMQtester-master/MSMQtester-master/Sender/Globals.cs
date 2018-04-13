﻿using System;
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

        //exits the application
        public static void CloseAllForms(object sender, FormClosingEventArgs e)
        {
            UnityCommunicationHub.PurgeFileSystem();
            Application.Exit();
        }
    }
}
