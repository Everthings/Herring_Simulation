using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButtonScript : MonoBehaviour {

    public int spawnCount;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void clicked()
    {
        GameObject.Find("Coonamessett").GetComponent<HerringGeneratorScript>().spawnHerring(spawnCount);
        GameObject.Find("Sections").GetComponent<MainScript>().disableRestorationOptions();
    }
}
