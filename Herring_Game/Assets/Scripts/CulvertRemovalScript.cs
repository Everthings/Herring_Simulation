using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CulvertRemovalScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void removeCulvert()
    {
        transform.Find("Culvert").transform.position = new Vector3(10, -100, 10);
    }
}
