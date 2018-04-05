using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class RotateHandParts : MonoBehaviour
{

    string directoryPath = @"c:\BCIDataDirectory";
    string filePath = @"c:\BCIDataDirectory\transferInfo.txt";
    string mutexFileTurn = @"c:\BCIDataDirectory\WFATurn.mutex";
    string mutexUnityTurn = @"c:\BCIDataDirectory\UnityTurn.mutex";
    string unityReadyToGo = @"c:\BCIDataDirectory\UnityReady.txt";
    string WFAReadyToGo = @"c:\BCIDataDirectory\WFAReady.txt";

    // Use this for initialization
    void Start()
    {
        //set things up

        using (StreamWriter sw = new StreamWriter(unityReadyToGo))
        {
            sw.WriteLine("G");
        }
        //wait for confirmation of ready to go
        while (!File.Exists(WFAReadyToGo)) { }
        switchToWFA();

    }

    // Update is called once per frame
    void Update()
    {
        if (File.Exists(mutexUnityTurn))
        {
            // Read and show each line from the file.
            string line = "";
            float data = 0;
            using (StreamReader sr = new StreamReader(filePath))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    switch (line.Substring(0, 2)){
                        case "T1":
                            data = (float)System.Convert.ToDouble(line.Substring(2));
                            Globals.T1DesiredPosition = optimizeInputAngles(data);
                            break;
                        case "T2":
                            data = (float)System.Convert.ToDouble(line.Substring(2));

                            Globals.T2DesiredPosition = optimizeInputAngles(data);
                            break;
                        case "A1":
                            data = (float)System.Convert.ToDouble(line.Substring(2));

                            Globals.A1DesiredPosition = optimizeInputAngles(data);
                            break;
                        case "A2":
                            data = (float)System.Convert.ToDouble(line.Substring(2));

                            Globals.A2DesiredPosition = optimizeInputAngles(data);
                            break;
                        case "A3":
                            data = (float)System.Convert.ToDouble(line.Substring(2));

                            Globals.A3DesiredPosition = optimizeInputAngles(data);
                            break;
                        case "B1":
                            data = (float)System.Convert.ToDouble(line.Substring(2));

                            Globals.B1DesiredPosition = optimizeInputAngles(data);
                            break;
                        case "B2":
                            data = (float)System.Convert.ToDouble(line.Substring(2));

                            Globals.B2DesiredPosition = optimizeInputAngles(data);
                            break;
                        case "B3":
                            data = (float)System.Convert.ToDouble(line.Substring(2));

                            Globals.B3DesiredPosition = optimizeInputAngles(data);
                            break;
                        case "C1":
                            data = (float)System.Convert.ToDouble(line.Substring(2));

                            Globals.C1DesiredPosition = optimizeInputAngles(data);
                            break;
                        case "C2":
                            data = (float)System.Convert.ToDouble(line.Substring(2));

                            Globals.C2DesiredPosition = optimizeInputAngles(data);
                            break;
                        case "C3":
                            data = (float)System.Convert.ToDouble(line.Substring(2));

                            Globals.C3DesiredPosition = optimizeInputAngles(data);
                            break;
                        case "D1":
                            data = (float)System.Convert.ToDouble(line.Substring(2));

                            Globals.D1DesiredPosition = optimizeInputAngles(data);
                            break;
                        case "D2":
                            data = (float)System.Convert.ToDouble(line.Substring(2));

                            Globals.D2DesiredPosition = optimizeInputAngles(data);
                            break;
                        case "D3":
                            data = (float)System.Convert.ToDouble(line.Substring(2));
                            Globals.D3DesiredPosition = optimizeInputAngles(data);
                            break;
                    }

                }
            }
            switchToWFA();
        }
        else
        {
            //Debug.Log("Waiting for control command");
        }
    }

    private float optimizeInputAngles(float input)
    {
        float data = input;
        while (data > 360)
        {
            data -= 360;
        }
        while (data < 0)
        {
            data += 360;
        }
        return data;
    }

    private void switchToWFA()
    {
        
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            sw.WriteLine("T1" + Globals.T1ActualPosition);
            sw.WriteLine("T2" + Globals.T2ActualPosition);
            sw.WriteLine("A1" + Globals.A1ActualPosition);
            sw.WriteLine("A2" + Globals.A2ActualPosition);
            sw.WriteLine("A3" + Globals.A3ActualPosition);
            sw.WriteLine("B1" + Globals.B1ActualPosition);
            sw.WriteLine("B2" + Globals.B2ActualPosition);
            sw.WriteLine("B3" + Globals.B3ActualPosition);
            sw.WriteLine("C1" + Globals.C1ActualPosition);
            sw.WriteLine("C2" + Globals.C2ActualPosition);
            sw.WriteLine("C3" + Globals.C3ActualPosition);
            //sw.WriteLine("D1" + Globals.D1ActualPosition);
            sw.WriteLine("D2" + Globals.D2ActualPosition);
            sw.WriteLine("D3" + Globals.D3ActualPosition);
            sw.WriteLine("From unity");
        }
        
        File.Delete(mutexUnityTurn);
        using (StreamWriter sw = new StreamWriter(mutexFileTurn))
        {
            sw.WriteLine("G");
        }
    }

}
