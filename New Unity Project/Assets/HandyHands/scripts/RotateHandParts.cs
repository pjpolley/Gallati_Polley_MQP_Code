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
    }

    // Update is called once per frame
    void Update()
    {
        if (File.Exists(mutexUnityTurn))
        {
            // Read and show each line from the file.
            string line = "";
            using (StreamReader sr = new StreamReader(filePath))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    switch (line.Substring(0, 2)){
                        case "T1":
                            Globals.T1Pos = (float)System.Convert.ToDouble(line.Substring(2));
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

    private void switchToWFA()
    {
        File.Delete(mutexUnityTurn);
        using (StreamWriter sw = new StreamWriter(mutexFileTurn))
        {
            sw.WriteLine("G");
        }
    }

}
