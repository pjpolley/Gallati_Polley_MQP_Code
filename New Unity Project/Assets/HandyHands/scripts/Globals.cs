using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals{
    //arbitrary statring value is 5.0. This should be overwriten by startup functionality
    //max degrees any joint can turn per frame. Should be low () for realistic behavior
    private static float maxSpeed = 5.0f;

    //constants needed for PID control
    private static float Kp = 0.5f;
    private static float Ki = 0;//0.001f;
    private static float Kd = 0;//0.1f;

    //constant threshhold needed to reset PID control. Set to a 2 degree threshhold as default
    public static float errorAccumulationClear = 2.0f;

    //global Desired Position of the joints
    public static volatile float T1DesiredPosition = 0;
    public static volatile float T2DesiredPosition = 0;
    public static volatile float A1DesiredPosition = 0;
    public static volatile float A2DesiredPosition = 0;
    public static volatile float A3DesiredPosition = 0;
    public static volatile float B1DesiredPosition = 0;
    public static volatile float B2DesiredPosition = 0;
    public static volatile float B3DesiredPosition = 0;
    public static volatile float C1DesiredPosition = 0;
    public static volatile float C2DesiredPosition = 0;
    public static volatile float C3DesiredPosition = 0;
    public static volatile float D1DesiredPosition = 0;
    public static volatile float D2DesiredPosition = 0;
    public static volatile float D3DesiredPosition = 0;

    //global actual Position of the joints
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

    public static void setParametersForPID(float KP, float KI, float KD, float desiredMaxSpeed)
    {
        Kp = KP;
        Ki = KI;
        Kd = KD;
        maxSpeed = desiredMaxSpeed;
    }

    public static float PID_Calculation(float error, float lastError, float accumulatedError)
    {
        float returnVal = (error * Kp) + (accumulatedError * Ki) + (((error - lastError) / Time.deltaTime) * Kd);
        //return returnVal;
        if(Math.Abs(returnVal) > maxSpeed)
        {
            return maxSpeed * Math.Sign(returnVal);
        }
        else
        {
            return returnVal;
        }
    }
}
