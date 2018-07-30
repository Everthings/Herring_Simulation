using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void clicked()
    {
        //TODO: make num actually control herring num

        GameObject.Find("Coonamessett").GetComponent<HerringGeneratorScript>().spawnHerring(0);
    }
}
