using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C2Behavior : MonoBehaviour {
    float lastError = 0.0f;
    float errorAccumulation = 0.0f;
    float lastDesiredPosition = float.MaxValue;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //get current position and desired position
        float desiredPosition = Globals.C2DesiredPosition;
        float currentPosition = transform.localEulerAngles.z;
        //calculate error
        float error = desiredPosition - currentPosition;
        //make sure to reset error acumulation if target changes suddenly
        if (Mathf.Abs((lastDesiredPosition - desiredPosition)) > Globals.errorAccumulationClear)
        {
            errorAccumulation = 0;
            lastDesiredPosition = desiredPosition;
        }
        //make sure we're moving optimally
        while (error > 180 || error < -180)
        {
            if (error > 180)
            {
                error = (error - 180) * -1;
            }
            else
            {
                error = (error + 180) * -1;
            }
        }
        //add to error accumulation for PID
        errorAccumulation += error;
        //calculate PID
        float changeInPosition = Globals.PID_Calculation(error, lastError, errorAccumulation);
        //make needed change
        transform.localEulerAngles = new Vector3(0, 0, currentPosition + changeInPosition);
        //update variables for next loop
        lastError = error;
        Globals.C2ActualPosition = transform.localEulerAngles.z;
    }
}
