using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickedScript: MonoBehaviour {

    public string next;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void Clicked()
    {
        GameObject.Find("Fade").GetComponent<FadeScript>().fadeOutScene(next);
    }
}
