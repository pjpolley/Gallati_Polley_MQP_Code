using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thumbRotationTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log(transform.ToString());
        Debug.Log(transform.localEulerAngles.ToString());
        transform.localEulerAngles = new Vector3(0, 0, 90);
        Debug.Log(transform.localEulerAngles.ToString());

    }

    // Update is called once per frame
    void Update () {
	}
}
