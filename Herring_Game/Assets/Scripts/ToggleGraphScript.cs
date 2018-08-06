using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGraphScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void toggleGraph(bool b)
    {
        if(b == true)
        {
            transform.Find("PieUI").transform.position = new Vector3(Screen.width/2, Screen.height/2, 0);
            transform.Find("GameUI").transform.position = new Vector3(0, -10000, 0);
        }
        else
        {
            transform.Find("PieUI").transform.position = new Vector3(0, -10000, 0);
            transform.Find("GameUI").transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        }
    }
}
