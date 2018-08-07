using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Dropdown>().value = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void valueChanged(int i)
    {
        DifficultyData.difficulty = i;
    }
}
