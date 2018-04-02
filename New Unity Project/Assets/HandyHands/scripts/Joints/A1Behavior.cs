using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A1Behavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.localEulerAngles = new Vector3(0, 0, Globals.A1Pos);
    }
}
