using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OKButton : MonoBehaviour {

    public string name;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void clicked(){
        GameObject.Find("Canvas").transform.Find(name).gameObject.SetActive(false);
    }

}
