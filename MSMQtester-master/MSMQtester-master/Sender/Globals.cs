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
        //frequency of transmission from BCI chip to this program
        public static float transmissionRate;

        //form constants for efficient loading
        public static Form welcomeScreen = null;
        public static Form treeScreen = null;
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

        //exits the application
        public static void CloseAllForms(object sender, FormClosingEventArgs e)
        {
            UnityCommunicationHub.PurgeFileSystem();
            Application.Exit();
        }
    }
}
