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

    public bool removeCulvert()
    {
        if(transform.Find("Culvert").transform.position.y != -100)
        {
            transform.Find("Culvert").transform.position = new Vector3(10, -100, 10);

            return true;
        }

        return false;
        
    }
}
