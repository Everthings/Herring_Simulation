using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextYearScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void clicked()
    {
        StartCoroutine(GameObject.Find("Sections").GetComponent<MainScript>().incrementYear(true));
    }
}
