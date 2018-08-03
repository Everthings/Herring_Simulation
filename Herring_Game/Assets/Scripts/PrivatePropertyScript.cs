using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivatePropertyScript : MonoBehaviour {

    public int minX;
    public int maxX;
    public int minZ;
    public int maxZ;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // Casts the ray and get the first game object hit
        Physics.Raycast(ray, out hit);

        Vector3 p = hit.point;

        if (p.x > minX && p.x < maxX && p.z > minZ && p.z < maxZ)
            GetComponent<MeshRenderer>().enabled = true;
        else
            GetComponent<MeshRenderer>().enabled = false;
    }
}
