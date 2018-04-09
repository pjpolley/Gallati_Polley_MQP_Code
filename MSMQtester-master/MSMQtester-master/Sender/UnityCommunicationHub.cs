using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sender
{
    public static class UnityCommunicationHub
    {
        private static string directoryPath = @"c:\BCIDataDirectory";
        private static string filePath = @"c:\BCIDataDirectory\transferInfo.txt";
        private static string mutexFileTurn = @"c:\BCIDataDirectory\WFATurn.mutex";
        private static string mutexUnityTurn = @"c:\BCIDataDirectory\UnityTurn.mutex";
        private static string unityReadyToGo = @"c:\BCIDataDirectory\UnityReady.txt";
        private static string WFAReadyToGo = @"c:\BCIDataDirectory\WFAReady.txt";

        private static bool initialized = false;

        public static bool connected = false;

        //initializes file communication system with Unity. Should ONLY be called once at program start.
        public static bool InitializeUnityCommunication()
        {
            if (initialized)
            {
                Console.WriteLine("ERROR: TRYING TO INITIALIZE AFTER INITIALIZATION SUCCESSFUL");
                return true;
            }
            // Determine whether the directory exists.
            if (Directory.Exists(directoryPath))
            {
                Console.WriteLine("Directory path exists already. Proceeding as normal.");
            }
            else
            {
                // Try to create the directory.
                Console.WriteLine("Creating directory path.");
                DirectoryInfo di = Directory.CreateDirectory(directoryPath);
            }

            //make sure to clean up any excess data from last run
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            if (File.Exists(mutexFileTurn))
            {
                File.Delete(mutexFileTurn);
            }
            if (File.Exists(mutexUnityTurn))
            {
                File.Delete(mutexUnityTurn);
            }
            using (StreamWriter sw = new StreamWriter(mutexFileTurn))
            {
                sw.WriteLine("g");
            }
            using (StreamWriter sw = new StreamWriter(WFAReadyToGo))
            {
                sw.WriteLine("g");
            }
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.WriteLine("START");
            }
            //wait for confirmation step
            Stopwatch watch = Stopwatch.StartNew();
            watch.Start();
            while (!File.Exists(unityReadyToGo))
            {
                if(watch.ElapsedMilliseconds > Globals.TimeToConnectToUnity)
                {
                    Console.WriteLine("Unity connection attempt stopped");
                    return false;
                }
            }
            initialized = true;
            connected = true;
            return true;
        }

        //performs both the read and write 
        public static bool TwoWayTransmission()
        {
            if (!ReadData(false))
            {
                Console.WriteLine("ERROR IN READING FROM UNITY");
                return false;
            }
            if (!WriteData(false))
            {
                Console.WriteLine("ERROR IN WRITING TO FILE TO TRANSMIT TO UNITY");
                return false;
            }
            switchToUnity();
            return true;
        }

        //returns true if it was able to aquire the file to read and then write to the file, false otherwise
        //input true unless using as an intermediate step
        public static bool ReadData(bool turnOverToUnityAfter)
        {
            if (File.Exists(mutexFileTurn))
            {
                //first get the position from the hand
                string line = "";
                using (StreamReader sr = new StreamReader(filePath))
                {
                    if((line = sr.ReadLine()) != null){
                        //make sure we're the recipient
                        if(line.Equals("TO WFA"))
                        {
                            //get data if we're the recipient
                            while ((line = sr.ReadLine()) != null)
                            {
                                switch (line.Substring(0, 2))
                                {
                                    case "T1":
                                        Globals.T1ActualPosition = (float)System.Convert.ToDouble(line.Substring(2));
                                        break;
                                    case "T2":
                                        Globals.T2ActualPosition = (float)System.Convert.ToDouble(line.Substring(2));
                                        break;
                                    case "A1":
                                        Globals.A1ActualPosition = (float)System.Convert.ToDouble(line.Substring(2));
                                        break;
                                    case "A2":
                                        Globals.A2ActualPosition = (float)System.Convert.ToDouble(line.Substring(2));
                                        break;
                                    case "A3":
                                        Globals.A3ActualPosition = (float)System.Convert.ToDouble(line.Substring(2));
                                        break;
                                    case "B1":
                                        Globals.B1ActualPosition = (float)System.Convert.ToDouble(line.Substring(2));
                                        break;
                                    case "B2":
                                        Globals.B2ActualPosition = (float)System.Convert.ToDouble(line.Substring(2));
                                        break;
                                    case "B3":
                                        Globals.B3ActualPosition = (float)System.Convert.ToDouble(line.Substring(2));
                                        break;
                                    case "C1":
                                        Globals.C1ActualPosition = (float)System.Convert.ToDouble(line.Substring(2));
                                        break;
                                    case "C2":
                                        Globals.C2ActualPosition = (float)System.Convert.ToDouble(line.Substring(2));
                                        break;
                                    case "C3":
                                        Globals.C3ActualPosition = (float)System.Convert.ToDouble(line.Substring(2));
                                        break;
                                    case "D1":
                                        Globals.D1ActualPosition = (float)System.Convert.ToDouble(line.Substring(2));
                                        break;
                                    case "D2":
                                        Globals.D2ActualPosition = (float)System.Convert.ToDouble(line.Substring(2));
                                        break;
                                    case "D3":
                                        Globals.D3ActualPosition = (float)System.Convert.ToDouble(line.Substring(2));
                                        break;
                                }

                            }
                        }
                    }

                }

                if (turnOverToUnityAfter)
                {
                    switchToUnity();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        //write the global variables to the file to transmit to unity. automatically switches to unity reading after
        public static bool WriteData(bool turnOverToUnityAfter)
        {
            //get the most recent data
            switchToUnity();
            Stopwatch timer = new Stopwatch();
            timer.Start();
            while (!File.Exists(mutexFileTurn))
            {
                if(timer.ElapsedMilliseconds > 5000)
                {
                    Console.WriteLine("Could not read from unity");
                    throw new Exception();
                }
            }
            //and now write your own data to the file
            File.Delete(filePath);
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.WriteLine("TO UNITY");
                sw.WriteLine("T1" + Globals.T1DesiredPosition);
                sw.WriteLine("T2" + Globals.T2DesiredPosition);
                sw.WriteLine("A1" + Globals.A1DesiredPosition);
                sw.WriteLine("A2" + Globals.A2DesiredPosition);
                sw.WriteLine("A3" + Globals.A3DesiredPosition);
                sw.WriteLine("B1" + Globals.B1DesiredPosition);
                sw.WriteLine("B2" + Globals.B2DesiredPosition);
                sw.WriteLine("B3" + Globals.B3DesiredPosition);
                sw.WriteLine("C1" + Globals.C1DesiredPosition);
                sw.WriteLine("C2" + Globals.C2DesiredPosition);
                sw.WriteLine("C3" + Globals.C3DesiredPosition);
                //sw.WriteLine("D1" + Globals.D1DesiredPosition);
                sw.WriteLine("D2" + Globals.D2DesiredPosition);
                sw.WriteLine("D3" + Globals.D3DesiredPosition);
                sw.WriteLine("From sender");
            }
            if (turnOverToUnityAfter)
            {
                switchToUnity();
            }
            return true;
        }

        public static void switchToUnity()
        {
            File.Delete(mutexFileTurn);
            using (StreamWriter sw = new StreamWriter(mutexUnityTurn))
            {
                sw.WriteLine("G");
            }
        }

        private static bool isMyTurn()
        {
            if (File.Exists(mutexFileTurn))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //deletes all files from transmission directory. Should ONLY be called upon program exit.
        public static void PurgeFileSystem()
        {
            File.Delete(unityReadyToGo);
            File.Delete(WFAReadyToGo);
            File.Delete(filePath);
            File.Delete(mutexFileTurn);
            File.Delete(mutexUnityTurn);
            connected = false;
            initialized = false;
        }

    }
}
