using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeShrubCollectionScript : MonoBehaviour
{

    List<List<Vector3>> sectionPositions = new List<List<Vector3>>();

    // Use this for initialization
    void Start () {
        sectionPositions.Add(transform.Find("Section1").GetComponent<TreeGeneratorScript>().getTreePositions());
        sectionPositions.Add(transform.Find("Section2").GetComponent<TreeGeneratorScript>().getTreePositions());
        sectionPositions.Add(transform.Find("Section3").GetComponent<TreeGeneratorScript>().getTreePositions());
        sectionPositions.Add(transform.Find("Section4").GetComponent<TreeGeneratorScript>().getTreePositions());
        sectionPositions.Add(transform.Find("Section5").GetComponent<TreeGeneratorScript>().getTreePositions());
        sectionPositions.Add(transform.Find("Section6").GetComponent<TreeGeneratorScript>().getTreePositions());
        sectionPositions.Add(transform.Find("Section7").GetComponent<TreeGeneratorScript>().getTreePositions());
        sectionPositions.Add(transform.Find("Section8").GetComponent<TreeGeneratorScript>().getTreePositions());
        sectionPositions.Add(transform.Find("Section9").GetComponent<TreeGeneratorScript>().getTreePositions());
        sectionPositions.Add(transform.Find("Section10").GetComponent<TreeGeneratorScript>().getTreePositions());
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
